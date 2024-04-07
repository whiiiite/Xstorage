using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xstorage.Extentions;
using System.Collections.Specialized;
using System.Diagnostics;
using Xstorage.Data;
using Xstorage.Entities.Models;
using Xstorage.Entities.ViewModels;
using Xstorage.Shared;
using Xstorage.Repositories;
using Xstorage.Services;
using Xstorage.Shared.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Xstorage.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class StoragesController : Controller
    {
        private readonly XstorageDbContext _context;
        private readonly StorageService storageService;
        private readonly UserRepository userRepository;
        private readonly StorageRepository storageRepository;
        private readonly SubscriptionRepository subscriptionRepository;

        private static int currentPage = 0;
        private static int currentPageSize = 0;

        private readonly ILogger<StoragesController> logger;

        public StoragesController(XstorageDbContext context, 
            ILogger<StoragesController> logger,
            UserRepository userRepository,
            StorageRepository storageRepository,
            SubscriptionRepository subscriptionRepository,
            StorageService storageService)
        {
            _context = context;
            this.userRepository = userRepository;
            this.logger = logger;
            this.storageRepository = storageRepository;
            this.storageService = storageService;
            this.subscriptionRepository = subscriptionRepository;
        }

        // GET: Storages
        public async Task<IActionResult> Index(int page = 1, int pageSize = 25)
        {
            try
            {
                string userId = await userRepository.GetUserIdAsync(User.Identity!);
                ViewBag.PageSize = currentPageSize = pageSize;
                ViewBag.CurrentPage = currentPage = page;
                ViewBag.UserStoragesCount = await storageRepository.GetCountOfUserStoragesAsync(userId);

                User? user = await userRepository.GetUserAsync(User.Identity);
                if (user == null)
                {
                    return NotFound("Not found_1");
                }

                Subscription? userSub = await subscriptionRepository.GetUserSubscriptionAsync(user);
                if (userSub == null)
                {
                    await subscriptionRepository.AddSubscriptionAsync(
                        SubscriptionRepository.InitSubscription(user.Id));
                }
                userSub = await subscriptionRepository.GetUserSubscriptionAsync(user);

                long userBytesTakes = await userRepository.CountMemoryUserTakesInServerAsync(User.Identity!);
                long available = SubscriptionRepository.GetAvailableBytesForLevel(userSub!.Level);
                ViewBag.MemUserTakes = userBytesTakes.BytesLengthToString();
                ViewBag.MemAvailableForUser = available.BytesLengthToString();
                ViewBag.PercentTaken = Math.Ceiling((double)userBytesTakes / available * 100d);

                return _context.Storage != null ?
                            View(await _context.Storage
                                               .OrderBy(p => p.Name)
                                               .Where(x => x.HostId == userId && !x.IsDeleted)
                                               .Skip((page - 1) * pageSize)
                                               .Take(pageSize).ToListAsync()) :
                            Problem("Entity set 'XstorageDbContext.Storage'  is null.");
            }
            catch (Exception ex)
            {
                return View("Message", ex.Message);
            }
        }

        // GET: Storages/Details/5
        public async Task<IActionResult> Details(string id, string path)
        {
            try
            {
                if (User.Identity == null) throw new Exception("User is not logged");
                DetailStorageViewModel? storageView = await storageService.DetailsAsync(id, path, User.Identity);

                if (storageView == null)
                {
                    throw new Exception("Storage is null");
                }

                return View(storageView);
            }
            catch(Exception ex)
            {
                return View("Message", ex.Message);
            }
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] StorageViewModel storageViewModel)
        {
            try
            {
                if (await storageService.CreateStorageAsync(storageViewModel, ModelState, User.Identity))
                {
                    return RedirectToAction(nameof(Index));
                }
                logger.LogInformation("{email} created storage {Name}",
                    User.Identity.Name, storageViewModel.Name);
                return View(storageViewModel);
            }
            catch (Exception e)
            {
                return View("Message", e.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateDirectory([FromForm] UploadFolderViewModel folderViewModel)
        {
            try
            {
                string path = folderViewModel.Path;
                string dirName = folderViewModel.DirName;
                string storageId = folderViewModel.StorageId;
                await storageService.CreateDirectoryAsync(storageId, path, dirName);
                logger.LogInformation("{email} created directory in storage {id}", User.Identity.Name, storageId);
                return RedirectToAction(nameof(Details), new { id = storageId, path });
            }
            catch (Exception e)
            {
                return View("Message", e.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateFile([FromForm] UploadFileViewModel fileViewModel)
        {
            try
            {
                CreationFilesResult result = await storageService.CreateFilesAsync(fileViewModel, User.Identity);
                if(!result.IsSuccess)
                {
                    return View("Message", result.Message);
                }
                logger.LogInformation("{email} created directory in storage {id}", User.Identity.Name, fileViewModel.StorageId);
                return RedirectToAction(nameof(Details),
                    new { id = fileViewModel.StorageId, fileViewModel.Path });
            }
            catch (Exception e)
            {
                return View("Message", e.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetFile(string storageId, string name)
        {
            try
            {
                if (User.Identity == null) return NotFound();

                string path = Path.Combine((await storageRepository.GetStorage(storageId)).Path, name);

                if (!System.IO.File.Exists(path))
                {
                    return NotFound("File does not exists");
                }

                byte[] contents = await System.IO.File.ReadAllBytesAsync(path);

                return File(contents, "*/*", path.Split('\\').TakeLast(1).First());
            }
            catch (Exception e)
            {
                return View("Message", e.Message);
            }
        }


        [HttpGet]
        public IActionResult DeleteItem(string id, string currentPath, string pathOfDelete, bool isDirectory)
        {
            try
            {
                if (isDirectory)
                {
                    Directory.Delete(pathOfDelete, recursive: true);
                }
                else
                {
                    System.IO.File.Delete(pathOfDelete);
                }
                logger.LogInformation("{email} deleted {path} in storage {id} isDir: {isdir}",
                    User.Identity.Name, pathOfDelete, id, isDirectory);
                return RedirectToAction(nameof(Details),
                    new { id, path = currentPath });
            }
            catch (Exception e)
            {
                return View("Message", e.Message);
            }
        }


        /// <summary>
        /// Opens the file for detailed view
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> FileViewer(string path)
        {
            try
            {
                await Task.Delay(1);
                return View();
            }
            catch (Exception e)
            {
                return View("Message", e.Message);
            }
        } 

        // GET: Storages/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (id == null || _context.Storage == null)
                {
                    return NotFound();
                }

                var storage = await _context.Storage
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (storage == null)
                {
                    return NotFound();
                }

                return View(storage);
            }
            catch (Exception e)
            {
                return View("Message", e.Message);
            }
        }

        // POST: Storages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                if (_context.Storage == null)
                {
                    return Problem("Entity set 'XstorageDbContext.Storage'  is null.");
                }
                bool success = await storageService.DeleteAsync(id);
                if (!success)
                {
                    return Problem("Something went wrong while deleting");
                }

                logger.LogInformation("{email} have deleted storage", User.Identity.Name);
                return RedirectToAction(nameof(Index), new { page = currentPage, pageSize = currentPageSize });
            }
            catch (Exception e)
            {
                return View("Message", e.Message);
            }
        }


        public async Task<IActionResult> CreateTestStorages(int count = 50)
        {
            for (int i = 0; i < count; i++)
            {
                StorageViewModel svm = new StorageViewModel()
                {
                    IsPrivate = false,
                    Name = Generator.RandomASCIIString(32),
                };
                await storageService.CreateStorageAsync(svm, ModelState, User.Identity);
            }

            return RedirectToAction(nameof(Index)); 
        }
    }
}
