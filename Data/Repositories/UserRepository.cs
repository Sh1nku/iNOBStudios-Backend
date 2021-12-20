using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using iNOBStudios.Models.Entities;
using iNOBStudios.Models.ViewModels.Account;

namespace iNOBStudios.Data.Repositories
{
    public class UserRepository : IUserRepository {

        private ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;

        public UserRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager) {
            this.userManager = userManager;
            this.db = db;
        }

        public async Task<IdentityResult> CreateUser(RegisterUserViewModel model) {
            ApplicationUser user = new ApplicationUser() { Email = model.Email, UserName = model.UserName};
            var result = await userManager.CreateAsync(user, model.Password);
            return result;
        }

        public ApplicationUser GetApplicationUserById(string id, bool track = false, string[] info = null) {
            var user = db.Users.AsQueryable();
            foreach (var include in info ?? Enumerable.Empty<string>()) {
                user = user.Include(include);
            }
            if(track) {
                return (ApplicationUser)user.Where(x => x.Id == id).SingleOrDefault();
            }
            return (ApplicationUser)user.Where(x => x.Id == id).AsNoTracking().SingleOrDefault();
        }

        public void UpdateApplicationUser(ApplicationUser user) {
            db.Users.Update(user);
            db.SaveChanges();
        }

        public Tuple<bool, List<string>> ValidatePassword(string password) {
            List<string> errors = new List<string>();
            bool success = true;
            var validators = userManager.PasswordValidators;
            foreach(var validator in validators) {
                Task<IdentityResult> a = validator.ValidateAsync(userManager, null, password);
                a.Wait();
                if(!a.Result.Succeeded) {
                    success = false;
                    foreach(var error in a.Result.Errors) {
                        errors.Add(error.Description);
                    }
                }
            }
            return new Tuple<bool, List<string>>(success, errors);
        }

        public ApplicationUser GetApplicationUserByUsername(string userName, bool track = false, string[] info = null) {
            var user = db.Users.AsQueryable();
            foreach (var include in info ?? Enumerable.Empty<string>()) {
                user = user.Include(include);
            }
            if(track) {
                return (ApplicationUser)user.Where(x => x.UserName == userName).SingleOrDefault();
            }
            return (ApplicationUser)user.Where(x => x.UserName == userName).AsNoTracking().SingleOrDefault();
        }

        public bool LeastOneUser() {
            return db.Users.Count() > 0;
        }
    }
}
