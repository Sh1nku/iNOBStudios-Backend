using iNOBStudios.Models.Entities;
using iNOBStudios.Models.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace iNOBStudios.Controllers {
    [Route("api/Account")]
    [ApiController]
    public class AccountWebController : ControllerBase {
        private UserManager<ApplicationUser> userManager;
        public static string TOKEN;
        public AccountWebController(UserManager<ApplicationUser> userManager) {
            this.userManager = userManager;
        }
        [HttpPost]
        public async Task<ActionResult<String>> TryLogin([FromBody] LoginUserViewModel u) {
            String token = await CreateToken(u, userManager);
            if (token != null) {
                return token;
            }
            return Forbid();
        }

        public static async Task<String> CreateToken(LoginUserViewModel u, UserManager<ApplicationUser> userManager) {
            ApplicationUser user = await userManager.FindByNameAsync(u.UserName);
            bool result = await userManager.CheckPasswordAsync(await userManager.FindByNameAsync(u.UserName), u.Password);
            if (await userManager.CheckPasswordAsync(await userManager.FindByNameAsync(u.UserName), u.Password)) {
                var roles = await userManager.GetRolesAsync(await userManager.FindByNameAsync(u.UserName));
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, u.UserName));
                foreach (var role in roles) {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                var token = new JwtSecurityToken(
                    new JwtHeader(new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TOKEN)),
                        SecurityAlgorithms.HmacSha256)),
                    new JwtPayload(claims));

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return null;
        }
    }
}
