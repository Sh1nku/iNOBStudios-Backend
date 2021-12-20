using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.ViewModels.Menu {
    public class MenuViewModel {
        public string Name { get; set; }
        public string JSON { get; set; }
        public virtual List<MenuItemViewModel> MenuItems { get; set; }
    }
}
