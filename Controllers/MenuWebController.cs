using iNOBStudios.Data.Repositories;
using iNOBStudios.Models;
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
        public IActionResult GetMenuByMenuName([FromRoute] string name) {
            var menu = menuRepository.GetMenuByName(name, false);
            if (menu == null) {
                return NotFound();
            }
            return Ok(Conversions.MenuViewModelFromMenu(menu));
        }
    }
}
