﻿using Microsoft.AspNetCore.Authorization;
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
                    return NotFound("Not found_2. Refresh the page please.");
                }

                long userBytesTakes = await userRepository.CountMemoryUserTakesInServerAsync(User.Identity!);
                long available = SubscriptionRepository.GetAvailableBytesForLevel(userSub.Level);
                ViewBag.MemUserTakes = userBytesTakes.BytesLengthToString();
                ViewBag.MemAvailableForUser = available.BytesLengthToString();
                ViewBag.PercentTaken = Math.Round(userBytesTakes / available * 100d, 2);

                return _context.Storage != null ?
                            View(await _context.Storage
                                                        .OrderBy(p => p.Name)
                                                        .Where(x => x.HostId == userId && !x.IsDeleted)
                                                        .Skip((page - 1) * pageSize)
                                                        .Take(pageSize).ToListAsync()) :
                            Problem("Entity set 'XstorageDbContext.Storage'  is null.");
            }
            catch(Exception e )
            {
                return NotFound($"Page was not found {e}");
            }
        }

        // GET: Storages/Details/5
        public async Task<IActionResult> Details(string id, string path)
        {

                if (User.Identity == null) return NotFound("Not found_1");
                DetailStorageViewModel? storageView = await storageService.DetailsAsync(id, path, User.Identity);

                if (storageView == null)
                {
                    return NotFound("Not found_2");
                }

                return View(storageView);

        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] StorageViewModel storageViewModel)
        {
            if (await storageService.CreateStorageAsync(storageViewModel, ModelState, User.Identity))
            {
                return RedirectToAction(nameof(Index));
            }
            logger.LogInformation("{email} created storage {Name}",
                User.Identity.Name, storageViewModel.Name);
            return View(storageViewModel);
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
            catch (Exception)
            {
                return NotFound("Page was not found");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateFile([FromForm] UploadFileViewModel fileViewModel)
        {
            try
            {
                await storageService.CreateFilesAsync(fileViewModel);
                logger.LogInformation("{email} created directory in storage {id}", User.Identity.Name, fileViewModel.StorageId);
                return RedirectToAction(nameof(Details),
                    new { id = fileViewModel.StorageId, fileViewModel.Path });
            }
            catch (Exception)
            {
                return NotFound("Page was not found");
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
            catch (Exception)
            {
                return NotFound("Page was not found");
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
            catch (Exception)
            {
                return NotFound("Page was not found");
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
                return View(nameof(FileViewer));
            }
            catch (Exception)
            {
                return NotFound("Page was not found");
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
            catch (Exception)
            {
                return NotFound("Page was not found");
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
            catch (Exception)
            {
                return NotFound("Page was not found");
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