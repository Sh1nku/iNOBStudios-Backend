using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading.Tasks;

namespace iNOBStudios.Models.Entities {
    public class Tag {
        [Key]
        [MaxLength(64)]
        public string TagId { get; set; }
        public IEnumerable<PostTag> PostTags { get; set; }
    }
}
