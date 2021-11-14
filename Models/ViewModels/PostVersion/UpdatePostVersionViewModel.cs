using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.ViewModels.PostVersion {
    public class UpdatePostVersionViewModel {
        [Required]
        public int PostVersionId { get; set; }

        public String? Title { get; set; }

        public String RawText { get; set; }
        public String PreviewText { get; set; }
    }
}
