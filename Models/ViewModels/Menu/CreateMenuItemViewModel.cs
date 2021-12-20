using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.ViewModels.Menu {
    public class CreateMenuItemViewModel {
        [Required]
        public string ParentMenuName { get; set; }
        public int? ParentMenuItemId { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        [Required]
        [Range(0, 1000)]
        public int Priority { get; set; }
        public string? Link { get; set; }


        public int? PostId { get; set; }
    }
}
