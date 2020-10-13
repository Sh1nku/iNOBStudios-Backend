using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.ViewModels.Tag {
    public class CreateTagViewModel {
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
    }
}
