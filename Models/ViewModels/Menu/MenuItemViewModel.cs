using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.ViewModels.Menu {
    public class MenuItemViewModel {
        public int MenuItemId { get; set; }
        public string ParentMenuName { get; set; }
        public int? ParentMenuItemId { get; set; }
        public virtual List<MenuItemViewModel> ChildMenuItems { get; set; }

        public string Name { get; set; }
        public int Priority { get; set; }
        //Could either be an external link, or a Post
        public string? Link { get; set; }


        public int? PostId { get; set; }
    }
}
