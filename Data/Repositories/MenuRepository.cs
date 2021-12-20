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

        public static void UpdateMenuJson(ApplicationDbContext db, string menuName) {
            if (menuName != null) {
                var menu = db.Menus.Include("MenuItems.Post.CurrentVersion").Where(x => x.Name == menuName).SingleOrDefault();
                if (menu != null) {
                    menu.JSON = menu.MenuItems.Count() > 0 ? Conversions.MenuJSONFromMenuItems(menu.MenuItems.ToList()) : null;
                }
            }
        }

        public MenuRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public Menu CreateMenu(Menu menu) {
            db.Menus.Add(menu);
            UpdateMenuJson(db, menu.Name);
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

        public MenuItem GetMenuItemByMenuItemId(int id, bool track = false) {
            var menuItem = db.MenuItems.Where(x => x.MenuItemId == id);
            return track ?
                menuItem.SingleOrDefault() : menuItem.AsNoTracking().SingleOrDefault();
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

        public MenuItem UpdateMenuItem(MenuItem menuItem) {
            db.MenuItems.Update(menuItem);
            UpdateMenuJson(db, menuItem.ParentMenuName);
            db.SaveChanges();
            return menuItem;
        }

        public MenuItem CreateMenuItem(MenuItem menuItem) {
            db.MenuItems.Add(menuItem);
            UpdateMenuJson(db, menuItem.ParentMenuName);
            db.SaveChanges();
            return menuItem;
        }

        public void RemoveMenuItem(MenuItem menuItem) {
            using var transaction = db.Database.BeginTransaction();
            db.MenuItems.Remove(menuItem);
            db.SaveChanges();
            UpdateMenuJson(db, menuItem.ParentMenuName);
            db.SaveChanges();
            transaction.Commit();
        }

        public void RemoveMenu(Menu menu) {
            db.Menus.Remove(menu);
            db.SaveChanges();
        }
    }
}
