using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Principal;
using Xstorage.Data;
using Xstorage.Entities.Models;
using Xstorage.Entities.ViewModels;
using Xstorage.Extentions;
using Xstorage.Services;
using Xstorage.Shared;

namespace Xstorage.Repositories
{
    public class UserRepository
    {
        readonly XstorageDbContext context;
        readonly UserManager<User>? userManager;
        readonly StorageRepository storageRepository;
        readonly SubscriptionRepository subscriptionRepository;

        public UserRepository(XstorageDbContext context, UserManager<User> userManager,
            StorageRepository storageRepository,
            SubscriptionRepository subscriptionRepository)
        {
            this.context = context;
            this.userManager = userManager;
            this.storageRepository = storageRepository;
            this.subscriptionRepository = subscriptionRepository;
        }

        /// <summary>
        /// Creates user in db
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public async Task<IEnumerable<IdentityError>> CreateUserAsync(SignUpViewModel userData)
        {
            // todo: add mapping
            User user = new User();
            user.UserName = userData.UserName;
            user.Email = userData.Email;
            user.FirstName = string.Empty;
            user.LastName = string.Empty;
            user.FolderPath = string.Empty;
            IdentityResult res = await userManager.CreateAsync(user, userData.Password);

            if (!res.Errors.Any())
            {
                user.FolderPath = DirectoryHelper.CreateUserFolder();
                context.Users.Update(user);
                await context.SaveChangesAsync();

                user = await userManager.FindByEmailAsync(userData.Email);

                Subscription subscription = SubscriptionRepository.InitSubscription(user.Id);
                await subscriptionRepository.AddSubscriptionAsync(subscription);
            }

            return res.Errors;
        }


        /// <summary>
        /// Get user from db by identity
        /// </summary>
        /// <param name="context"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<User?> GetUserAsync(IIdentity? identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("IIdentity is null");
            }

            return await context.GetUserAsync(identity);
        }

        /// <summary>
        /// Get user id by its identity
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<string> GetUserIdAsync(IIdentity? identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("IIdentity is null");
            }

            return (await context.GetUserAsync(identity))?.Id;
        }

        /// <summary>
        /// Finds user in db by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<User?> GetUserAsync(Expression<Func<User, bool>> predicate)
        {
            return await context.Users.FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Counts memory that user takes on the server memory space in total
        /// </summary>
        /// <returns>Bytes of taken memory space</returns>
        public async Task<long> CountMemoryUserTakesInServerAsync(IIdentity userIdentity)
        {
            if (userIdentity == null)
            { 
                throw new ArgumentNullException("IIdentity is null");
            }

            IEnumerable<Storage> storages = await storageRepository.GetUserStoragesAsync(userIdentity);

            long dirSize = 0;
            foreach (var storage in storages)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(storage.FullPath);
                dirSize += await Task.Run(() => dirInfo.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length));
            }

            return dirSize;
        }
    }
}
