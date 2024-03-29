using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using Xstorage.Data;
using Xstorage.Entities.Models;
using Xstorage.Extentions;

namespace Xstorage.Repositories
{
    public class StorageRepository
    {
        XstorageDbContext context;

        public StorageRepository(XstorageDbContext context)
        {
            this.context = context;
        }


        /// <summary>
        /// Checks if user is owner of storage
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="storageId"></param>
        /// <returns></returns>
        public async Task<bool> UserIsOwnerAsync(string userId, string storageId)
        {
            return await 
                context.Storage.AnyAsync(x => x.Id == storageId && x.HostId == userId);
        }


        /// <summary>
        /// Gets storage object by its id
        /// </summary>
        /// <param name="storageId"></param>
        /// <returns></returns>
        public async Task<Storage?> GetStorage(string storageId)
        {
            return await context.Storage.FirstOrDefaultAsync(x=>x.Id == storageId);
        }


        /// <summary>
        /// Gets host(user) that created storage
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        public async Task<User?> GetHostOfStorage(Storage storage)
        {
            if (storage == null) return null;
            return await context.Users.FirstOrDefaultAsync(u => u.Id == storage.HostId);
        }

        /// <summary>
        /// Gets host(user) that created storage
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        public async Task<User?> GetHostOfStorage(string storageId)
        {
            if (storageId == null || string.IsNullOrWhiteSpace(storageId)) return null;
            Storage? storage = await context.Storage.FirstOrDefaultAsync(s => s.Id == storageId);
            if (storage == null) return null;
            return await context.Users.FirstOrDefaultAsync(u => u.Id == storage.HostId);
        }


        /// <summary>
        /// Get count of storages that user had created
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<int> GetCountOfUserStoragesAsync(IIdentity? userIdentity)
        {
            if (userIdentity == null)
            {
                throw new NullReferenceException("IIdentity is null");
            }

            User? user = await context.GetUserAsync(userIdentity);
            if (user == null)
            {
                throw new NullReferenceException("User is null");
            }

            return await context.Storage.Where(s => s.HostId == user.Id).CountAsync();
        }

        /// <returns>Count of user storages</returns>
        public async Task<int> GetCountOfUserStoragesAsync(string userId)
        {
            return await context.Storage.Where(s => s.HostId == userId).CountAsync();
        }

        /// <summary>
        /// Finds all of storages that user had created
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <returns>User storages that had created</returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<IEnumerable<Entities.Models.Storage>> GetUserStoragesAsync(IIdentity? userIdentity)
        {
            if (userIdentity == null)
            {
                throw new NullReferenceException("IIdentity is null");
            }

            User? user = await context.GetUserAsync(userIdentity);
            if (user == null)
            {
                throw new NullReferenceException("User is null");
            }

            return await context.Storage.Where(s => s.HostId == user.Id).ToListAsync();
        }

        /// <summary>
        /// Finds all of storages that user had created
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User storages that had created</returns>
        public async Task<IEnumerable<Entities.Models.Storage>> GetUserStoragesAsync(string userId)
        {
            return await context.Storage.Where(s => s.HostId == userId).ToListAsync();
        }
    }
}
