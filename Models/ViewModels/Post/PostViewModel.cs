using iNOBStudios.Models.ViewModels.Account;
using iNOBStudios.Models.ViewModels.ExternalFile;
using iNOBStudios.Models.ViewModels.PostVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.ViewModels.Post {
    public class PostViewModel {
        public int PostId { get; set; }
        public bool Published { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime? FirstPublished { get; set; }
        public string Alias { get; set; }
        public virtual PostVersionViewModel CurrentVersion { get; set; }
        public virtual Dictionary<string, PostVersionViewModel> PostVersions { get; set; }
        public string AuthorId { get; set; }
        public List<string> PostTags { get; set; }
        public List<ExternalFileViewModel> ExternalFiles { get; set; }
    }
}
