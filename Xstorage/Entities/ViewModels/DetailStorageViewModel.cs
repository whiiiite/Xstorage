using System.Collections;
using System.ComponentModel;
using Xstorage.Mixins;

namespace Xstorage.Entities.ViewModels
{
    public class DetailStorageViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public string UserNameOfHost { get; set; } = null!;

        /// <summary>
        /// Indicates if visitor of detailed storage page is owner of storage
        /// </summary>
        public bool VisitorIsHost { get; set; }

        [DisplayName("Storage is private")]
        public bool IsPrivate { get; set; }

        public string Path { get; set; } = string.Empty!;

        public string[] PathParts { get; set; } = null!;

        public string HostId { get; set; }

        public IEnumerable<FileSystemItemData> ItemsData { get; set; } = null!;
    }
}
