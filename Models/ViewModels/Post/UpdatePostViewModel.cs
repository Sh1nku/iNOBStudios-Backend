using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.ViewModels.Post {
    public class UpdatePostViewModel {
        [Required]
        public int PostId { get; set; }
        public bool? Published { get; set; }
        public int? CurrentVersion { get; set; }
        public bool? List { get; set; }
        public List<string> PostTags { get; set; }
        public List<string> ExternalFiles { get; set; }
    }
}
