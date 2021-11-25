using iNOBStudios.Models;
using iNOBStudios.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Data.Repositories {
    public class MenuRepository : IMenuRepository{
        private ApplicationDbContext db;
        public MenuRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public Menu CreateMenu(Menu menu) {
            menu.JSON = menu.MenuItems != null ? Conversions.MenuJSONFromMenuItemViewModels(menu.MenuItems.Select(x => Conversions.MenuItemViewModelFromMenuItem(x)).ToList()) : null;
            db.Menus.Add(menu);
            db.SaveChanges();
            return menu;
        }

        public Menu GetMenuByName(string name, bool track = false, string[] info = null) {
            var menus = db.Menus.AsQueryable();
            foreach (var include in info ?? Enumerable.Empty<string>()) {
                menus = menus.Include(include);
            }
            menus = menus.Where(x => x.Name == name);
            if (track) {
                return menus.SingleOrDefault();
            }
            return menus.AsNoTracking().SingleOrDefault();
        }

        public IEnumerable<Menu> GetMenus(bool track = false, string[] info = null) {
            var menus = db.Menus.AsQueryable();
            foreach (var include in info ?? Enumerable.Empty<string>()) {
                menus = menus.Include(include);
            }
            if (track) {
                return menus;
            }
            return menus.AsNoTracking();
        }
    }
}
