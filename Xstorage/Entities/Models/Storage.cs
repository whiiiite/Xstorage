using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Xstorage.Entities.Models
{
    /// <summary>
    /// Represents storage of user
    /// </summary>
    public class Storage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;

        [Required]
        public required string Name { get; set; } = null!;

        [Required]
        [DisplayName("Storage is private")]
        public required bool IsPrivate { get; set; }


        private string path;
        [Required]
        [DisplayName("Path to storage")]
        public required string Path { get { return path.Remove(path.Length - Name.Length); } set { path = value; } }

        [NotMapped]
        public string FullPath
        {
            get { return System.IO.Path.Combine(Path, Name); }
        }

        public User Host { get; set; } = default!;

        [Required]
        public required string HostId { get; set; } = null!;

        
        [DisplayName("Storage is deleted")]
        public required bool IsDeleted { get; set; }
    }
}
