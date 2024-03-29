using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xstorage.Enums;

namespace Xstorage.Entities.Models
{
    /// <summary>
    /// Represents the paid subscription of user
    /// </summary>
    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public User User { get; set; }
        public required string UserId { get; set; }
        public required SubscriptionLevel Level { get; set; }
        [DisplayName("Start date")]
        public required DateTimeOffset DateStart { get; set; }
        [DisplayName("Expiration date")]
        public required DateTimeOffset DateExpiration { get; set; }
    }
}
