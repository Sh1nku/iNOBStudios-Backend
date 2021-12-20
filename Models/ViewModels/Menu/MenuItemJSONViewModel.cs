using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.ViewModels.Menu {
    public class MenuItemJSONViewModel {
        public string Name { get; set; }
        public string? Link { get; set; }
        public virtual List<MenuItemJSONViewModel> ChildMenuItems { get; set; }
    }
}
