using iNOBStudios.Models.Entities;
using iNOBStudios.Models.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Data.Repositories {
    public interface IUserRepository {
        ApplicationUser GetApplicationUserByUsername(string userName, bool track = false, string[] info = null);
        ApplicationUser GetApplicationUserById(string id, bool track = false, string[] info = null);

        bool LeastOneUser();

        Tuple<bool, List<string>> ValidatePassword(string password);

        Task<IdentityResult> CreateUser(RegisterUserViewModel model);
    }
}
