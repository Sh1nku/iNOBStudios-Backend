using iNOBStudios.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Data.Repositories {
    public interface IMenuRepository {
        public Menu CreateMenu(Menu menu);
        public MenuItem CreateMenuItem(MenuItem menuItem);
        public Menu GetMenuByName(string name, bool track = false, string[] info = null);
        public MenuItem GetMenuItemByMenuItemId(int id, bool track = false);
        public IEnumerable<Menu> GetMenus(bool track = false, string[] info = null);

        public MenuItem UpdateMenuItem(MenuItem menuItem);
        public void RemoveMenuItem(MenuItem menuItem);
        public void RemoveMenu(Menu menu);
    }
}
