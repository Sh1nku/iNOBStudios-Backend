using iNOBStudios.Data.Repositories;
using iNOBStudios.Models;
using iNOBStudios.Models.Entities;
using iNOBStudios.Models.ViewModels.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Controllers {

    [Route("Menu")]
    [ApiController]
    public class MenuWebController : ControllerBase{
        private IMenuRepository menuRepository;

        public MenuWebController(IMenuRepository menuRepository) {
            this.menuRepository = menuRepository;
        }

        [HttpGet]
        [Route("Menu/{name}")]
        public IActionResult GetMenuByMenuName([FromRoute] string name, [FromQuery] bool rawMenuItems = false) {
            var includes = new List<string>();
            if (rawMenuItems) {
                includes.Add("MenuItems");
            }

            var menu = menuRepository.GetMenuByName(name, false, includes.ToArray());
            if (menu == null) {
                return NotFound();
            }
            return Ok(Conversions.MenuViewModelFromMenu(menu));
        }

        [HttpPost]
        [Route("Menu")]
        [Authorize]
        public IActionResult CreateMenu([FromBody] CreateMenuViewModel model) {
            var menu = new Menu() {
                Name = model.Name
            };
            try {
                menu = menuRepository.CreateMenu(menu);
                return Ok(Conversions.MenuViewModelFromMenu(menu));
            }
            catch (Exception e) {
                ModelState.AddModelError("DB", e.Message);
                return BadRequest(ModelState.GetModelStateErrors());
            }
        }
    }
}
