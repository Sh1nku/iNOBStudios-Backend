using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.ViewModels.PostVersion {
    public class CreatePostVersionViewModel {
        public int PostId { get; set; }
        [StringLength(191, MinimumLength = 1, ErrorMessage = "{0} must be between {1} and {2} characters")]
        public string Title { get; set; }
    }
}
