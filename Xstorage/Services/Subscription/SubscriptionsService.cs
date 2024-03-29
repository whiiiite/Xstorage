using NuGet.Protocol.Core.Types;
using Xstorage.Data;
using Xstorage.Enums;
using Xstorage.Repositories;

namespace Xstorage.Services.Subscription
{
    public class SubscriptionService
    {
        readonly XstorageDbContext context;
        readonly SubscriptionRepository repository;
        public SubscriptionService(XstorageDbContext context, SubscriptionRepository repository)
        {
            this.context = context;
            this.repository = repository;
        }
    }
}
