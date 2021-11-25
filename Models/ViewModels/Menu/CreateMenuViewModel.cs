using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.ViewModels.Menu {
    public class CreateMenuViewModel {
        [StringLength(255, MinimumLength = 1, ErrorMessage = "{0} must be between {1} and {2} characters")]
        public string Name { get; set; }
    }
}
