using iNOBStudios.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Data.Repositories {
    public interface IMenuRepository {
        public Menu CreateMenu(Menu menu);
        public Menu GetMenuByName(string name, bool track = false, string[] info = null);
        public IEnumerable<Menu> GetMenus(bool track = false, string[] info = null);
    }
}
