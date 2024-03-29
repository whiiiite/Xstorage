using Microsoft.AspNetCore.Identity;

namespace Xstorage.Entities.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Storage> Storages { get; set; }
        public Subscription Subscription { get; set; }
        public ApiKey ApiKey { get; set; }
        /// <summary>
        /// Physical path on server for folder of user
        /// </summary>
        public string FolderPath { get; set; }  
        
    }
}
