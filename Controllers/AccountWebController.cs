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
    [Route("Account")]
    [ApiController]
    public class AccountWebController : ControllerBase {
        private UserManager<ApplicationUser> userManager;
        public static string TOKEN;
        public AccountWebController(UserManager<ApplicationUser> userManager) {
            this.userManager = userManager;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<String>> Login([FromBody] LoginUserViewModel u) {
            ApplicationUser user = await userManager.FindByNameAsync(u.UserName);
            if (user == null) {
                return NotFound();
            }
            
            if (await userManager.CheckPasswordAsync(user, u.Password)) {
                var roles = await userManager.GetRolesAsync(user);
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
            return StatusCode(403);
        }
    }
}
