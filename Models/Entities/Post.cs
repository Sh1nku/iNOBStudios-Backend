using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.Entities
{
    public class Post {
        [Key]
        public int PostId { get; set; }
        
        public int CurrentVersionId { get; set; }
        public virtual PostVersion CurrentVersion { get; set; }
        public virtual IEnumerable<PostVersion> PostVersions { get; set; }
        [Required]
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public IEnumerable<PostTag> PostTags { get; set; }
        public IEnumerable<ExternalFile> ExternalFiles { get; set; }
    }
}
