using iNOBStudios.Data.Repositories;
using iNOBStudios.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Data {
    public static class SeedData {
        public static async System.Threading.Tasks.Task Seed(IServiceProvider app, ApplicationDbContext db, IConfigurationRoot config, IUserRepository userRepository, IMenuRepository menuRepository) {
            if(!userRepository.LeastOneUser()) {
                var email = config.GetSection("StandardUser")["Email"];
                var username = config.GetSection("StandardUser")["Username"];
                var password = config.GetSection("StandardUser")["Password"];

                if (!(email.Length > 0 && username.Length > 0 && password.Length > 0)) {
                    await userRepository.CreateUser(new Models.ViewModels.Account.RegisterUserViewModel() {
                        Email = email,
                        UserName = username,
                        Password = password,
                        ConfirmPassword = password
                    });
                }
                else {
                    throw new Exception("User not set up in config");
                }
            }
            if(menuRepository.GetMenuByName("Main") == null) {
                MenuItem home = new MenuItem() {
                    Name = "Home",
                    Link = "/",
                    Priority = 1000
                };
                Menu menu = new Menu() {
                    Name = "Main",
                    MenuItems = new List<MenuItem>() { home }
                };
                home.ParentMenu = menu;
                menuRepository.CreateMenu(menu);
            }
        }
    }
}
