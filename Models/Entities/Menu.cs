using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.Entities {
    public class Menu {
        [Key]
        [MaxLength(191)]
        public string Name { get; set; }
        public string JSON { get; set; }
        public virtual IEnumerable<MenuItem> MenuItems { get; set; }
        
    }
}
