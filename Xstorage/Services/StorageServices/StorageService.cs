using Microsoft.EntityFrameworkCore;
using Xstorage.Data;
using Xstorage.Entities.Models;
using Xstorage.Entities.ViewModels;
using Xstorage.Repositories;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.CompilerServices;
using Xstorage.Shared;
using Xstorage.Shared.Models;

namespace Xstorage.Services
{
    /// <summary>
    /// Logic of Storages
    /// </summary>
    public class StorageService
    {
        readonly XstorageDbContext context;
        readonly UserRepository userRepository;
        readonly StorageRepository storageRepository;
        readonly SubscriptionRepository subscriptionRepository;

        public StorageService(XstorageDbContext context,
            UserRepository userRepository, 
            StorageRepository storageRepository,
            SubscriptionRepository subscriptionRepository) 
        {
            this.context = context;
            this.userRepository = userRepository;
            this.storageRepository = storageRepository;
            this.subscriptionRepository = subscriptionRepository;
        }


        /// <summary>
        /// Get detailed inforamtion(model) of specific storage part
        /// </summary>
        /// <param name="id"></param>
        /// <param name="path"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<DetailStorageViewModel?> DetailsAsync(string id, string path, IIdentity? identity)
        {
            User? visitor = (await userRepository.GetUserAsync(identity!))!;
            if (visitor == null) return null;
 
            Storage? storage = await context.Storage.FirstOrDefaultAsync(m => m.Id == id);
            if (!await UserHasAccessAsync(id, visitor.Id) || storage == null)
            {
                return null;
            }

            User? user = await userRepository.GetUserAsync(x => x.Id == storage.HostId);
            if (user == null) return null;

            if (!Directory.Exists(storage.Path))
            {
                return null;
            }

            // make path parts for arrow it in view
            string[]? pathParts = pathParts = path.Split("\\");

            bool isOwner = await storageRepository.UserIsOwnerAsync(visitor.Id, storage.Id);
            var storageView = new DetailStorageViewModel()
            {
                Id = storage.Id,
                Name = storage.Name,
                UserNameOfHost = user.UserName,
                VisitorIsHost = isOwner,
                IsPrivate = storage.IsPrivate,
                HostId = storage.HostId,
                PathParts = pathParts,
                Path = path,
                ItemsData = DirectoryHelper.GetDataOfItems(Path.Combine(storage.Path, path)),
            };

            return storageView;
        }

        /// <summary>
        /// Creates directory into storage of user
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dirName"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task CreateDirectoryAsync(string storageId, string path, string dirName)
        {
            Storage s = await storageRepository.GetStorage(storageId);
            if (s == null) return;
            DirectoryHelper.CreateDirectoryInStorage(Path.Combine(s.Path, path), dirName);
        }


        /// <summary>
        /// Creates file on the server in storage of user
        /// </summary>
        /// <param name="fileViewModel"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<CreationFilesResult> CreateFilesAsync(UploadFileViewModel fileViewModel, IIdentity userIdentity)
        {
            Storage? storage = await storageRepository.GetStorage(fileViewModel.StorageId);
            if (storage == null) return new CreationFilesResult() 
            {
                IsSuccess = false, Message = "Storage is null" 
            };
            string path = Path.Combine(storage.Path, fileViewModel.Path);
            IEnumerable<IFormFile>? formFiles = fileViewModel.Files;

            // if user uploaded something from local
            if (formFiles != null)
            {
                bool isBreakedDueSize = false;
                await Task.Run(async () =>
                {
                    foreach (var formFile in formFiles)
                    {
                        User? user = await userRepository.GetUserAsync(userIdentity);
                        Entities.Models.Subscription? userSub = await subscriptionRepository.GetUserSubscriptionAsync(user);
                        long userBytesTakes = await userRepository.CountMemoryUserTakesInServerAsync(userIdentity);
                        long available = SubscriptionRepository.GetAvailableBytesForLevel(userSub.Level);
                        if(!UserRepository.UserCanUploadFile(userBytesTakes, available, formFile.Length)) 
                        {
                            isBreakedDueSize = true;
                            break;
                        }

                        await CreateFileAsync(formFile, path);
                    }
                });

                if(isBreakedDueSize)
                {
                    return new CreationFilesResult()
                    {
                        IsSuccess = false,
                        Message = "Your storage size limit is reached"
                    };
                }
            }
            else // if user want to create empty file 
            {
                string fileName = fileViewModel.FileName;
                FileHelper.CreateFileInStorage(path, fileName);
            }

            return new CreationFilesResult()
            {
                IsSuccess = true,
                Message = "OK"
            };
        }

        /// <summary>
        /// Creates file that was passed from form
        /// </summary>
        /// <param name="file"></param>
        /// <param name="path"></param>
        /// <param name="identity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task CreateFileAsync(IFormFile file, string path, CancellationToken ct = default!)
        {
            string formFileName = Path.GetFileName(file.FileName);
            string serverPath = FileHelper.CreateFileInStorage(path, formFileName);

            using (var stream = new FileStream(serverPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                await file.CopyToAsync(stream, ct);
            }
        }

        /// <summary>
        /// Create storage in db and in server
        /// </summary>
        /// <param name="storageViewModel"></param>
        /// <param name="modelState"></param>
        /// <param name="identity"></param>
        /// <returns>If create is success</returns>
        public async Task<bool> CreateStorageAsync(StorageViewModel storageViewModel, 
            ModelStateDictionary modelState, IIdentity identity)
        {
            User user = (await userRepository.GetUserAsync(identity!))!;

            if (await StorageForUserExistsAsync(storageViewModel.Name, user.Id))
            {
                modelState.AddModelError("name taken",
                    $"Name of storage '{storageViewModel.Name}' is already exists");
                return false;
            }

            if (modelState.IsValid)
            {
                var storage = new Storage()
                {
                    Name = storageViewModel.Name,
                    IsPrivate = storageViewModel.IsPrivate,
                    IsDeleted = false,
                    Path = user.FolderPath,
                    HostId = user.Id!
                };

                DirectoryHelper.CreateUserFolderStorage(Path.Combine(user.FolderPath, storageViewModel.Name));

                storage.IsDeleted = false;
                await context.AddAsync(storage);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }


        /// <summary>
        /// Checks if storage is exists 
        /// </summary>
        /// <param name="storageName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> StorageForUserExistsAsync(string storageName, string userId)
        {
            return await context.Storage.AnyAsync(x => x.Name == storageName && x.HostId == userId);
        }

        /// <summary>
        /// Cleaning the physical memory on the server after deletion of storage
        /// </summary>
        /// <param name="pathToDir"></param>
        private static void CleanUpMemoryOfStorage(string pathToDir)
        {
            Directory.Delete(pathToDir, true);
        }

        /// <summary>
        /// Checks if user permitted to enter to this storage
        /// </summary>
        /// <param name="storageId"></param>
        /// <param name="userId"></param>d
        /// <returns></returns>
        public async Task<bool> UserHasAccessAsync(string storageId, string userId)
        {
            if (storageId == null || context.Storage == null) return false;

            Storage? storage = await storageRepository.GetStorage(storageId);
            if (storage == null) return false;
            if (storage.IsDeleted) return false;

            bool isOwner = await storageRepository.UserIsOwnerAsync(userId, storage.Id);
            if (!isOwner && storage.IsPrivate)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Deletes storage from db and memory of server
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userIdentity"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                Storage? storage = await context.Storage.FindAsync(id);
                if (storage != null)
                {
                    context.Storage.Remove(storage);
                    CleanUpMemoryOfStorage(Path.Combine(storage.Path, storage.Name));
                }

                await context.SaveChangesAsync();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
