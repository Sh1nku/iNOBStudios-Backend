using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.Entities
{
    public class Post {
        [Key]
        public int PostId { get; set; }
        public bool Published { get; set; }

        public bool List { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime? FirstPublished { get; set; }

        [MaxLength(255)]
        public string Alias { get; set; }

        [InverseProperty("CurrentVersion")]
        public virtual PostVersion CurrentVersion { get; set; }
        public virtual IEnumerable<PostVersion> PostVersions { get; set; }
        [Required, ForeignKey("Author")]
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public IEnumerable<PostTag> PostTags { get; set; }
        public IEnumerable<ExternalFile> ExternalFiles { get; set; }
        public IEnumerable<MenuItem> MenuItems { get; set; }
    }
}
