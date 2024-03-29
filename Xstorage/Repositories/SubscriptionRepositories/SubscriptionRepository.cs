using Microsoft.EntityFrameworkCore;
using Xstorage.Data;
using Xstorage.Entities.Models;
using Xstorage.Enums;

namespace Xstorage.Repositories
{
    /// <summary>
    /// Class for CRUD over subscription table
    /// </summary>
    public class SubscriptionRepository
    {
        readonly XstorageDbContext context;
        public SubscriptionRepository(XstorageDbContext context) 
        {
            this.context = context;
        }

        public class SubscriptionArgs
        {
            public required string UserId { get; set; }
            public required SubscriptionLevel Level { get; set; }
            public required DateTimeOffset DateStart { get; set; }
            public required DateTimeOffset DateExpiration { get; set; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Object of new subscription by its args</returns>
        public static Subscription InitSubscription(SubscriptionArgs args)
        {
            return new Subscription()
            {
                UserId = args.UserId,
                Level = args.Level,
                DateStart = args.DateStart,
                DateExpiration = args.DateExpiration,
            };
        }

        /// <returns>Object of new subscription with default values</returns>
        public static Subscription InitSubscription(string userId)
        {
            SubscriptionArgs args = new SubscriptionArgs()
            {
                UserId = userId,
                Level = SubscriptionLevel.Free,
                DateStart = DateTimeOffset.Now,
                DateExpiration = DateTimeOffset.Now.AddYears(999)
            };
            return new Subscription()
            {
                UserId = args.UserId,
                Level = args.Level,
                DateStart = args.DateStart,
                DateExpiration = args.DateExpiration,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns>Available user space on server for his sub level</returns>
        public static long GetAvailableBytesForLevel(SubscriptionLevel level)
        {
            return level switch
            {
                SubscriptionLevel.Free => 16_777_216_000L,
                SubscriptionLevel.Basic => 107_374_182_400L,
                SubscriptionLevel.Standart => 268_435_456_000L,
                SubscriptionLevel.Premium => 536_870_912_000L,
                _ => 0,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Subscription of user</returns>
        public async Task<Subscription?> GetUserSubscriptionAsync(string userId)
        {
            return await context.Subscriptions.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        /// <returns>Subscription of user</returns>
        public async Task<Subscription?> GetUserSubscriptionAsync(User user)
        {
            return await GetUserSubscriptionAsync(user.Id);
        }

        /// <summary>
        /// Adds subscription to db
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            await context.Subscriptions.AddAsync(subscription);
            await context.SaveChangesAsync();
        }

        public async Task<Subscription?> GetSubscriptionAsync(string id)
        {
            return await context.Subscriptions.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task EditSubscriptionLevelAsync(string id, SubscriptionLevel level)
        {
            throw new NotImplementedException("n");
        }

        public async Task EditSubscriptionExpireDateAsync(string id, DateTimeOffset dateExpire)
        {
            throw new NotImplementedException("n");
        }

        public async Task EditSubscriptionStartDateAsync(string id, DateTimeOffset dateExpire)
        {
            throw new NotImplementedException("n");
        }
    }
}
