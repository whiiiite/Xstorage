using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xstorage.Entities.Models
{
    public class ApiKey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;
        public required string Key { get; set; }
        public required long CallsCount { get; set; }
        public required DateTime DateExpire { get; set; }
        public User User { get; set; } = null!;
        public required string UserId { get; set; } = null!;
        public required bool Enabled { get; set; }
    }
}
