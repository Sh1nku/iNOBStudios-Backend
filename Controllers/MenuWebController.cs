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
        private IPostRepository postRepository;

        public MenuWebController(IMenuRepository menuRepository, IPostRepository postRepository) {
            this.menuRepository = menuRepository;
            this.postRepository = postRepository;
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

        [HttpPut]
        [Route("MenuItem")]
        [Authorize]
        public IActionResult UpdateMenuItem([FromBody] UpdateMenuItemViewModel model) {
            var menuItem = menuRepository.GetMenuItemByMenuItemId(model.MenuItemId);
            if (menuItem == null) {
                return NotFound();
            }
            try {
                if (model.ParentMenuItemIdSet) {
                    if(model.ParentMenuItemId != null) {
                        var parentItem = menuRepository.GetMenuItemByMenuItemId((int)model.ParentMenuItemId);
                        if (parentItem == null) {
                            ModelState.AddModelError("parent", "Could not find specified parent");
                        }
                        else if (parentItem.ParentMenuName != menuItem.ParentMenuName) {
                            ModelState.AddModelError("parent", "Parent must be part of the same menu");
                        }
                    }
                    menuItem.ParentMenuItemId = model.ParentMenuItemId;
                }
                if (model.Name != null) {
                    if (model.Name.Length < 1) {
                        ModelState.AddModelError("name", "Name must be 1 character or longer");
                    }
                    menuItem.Name = model.Name;
                }
                if(model.Priority != null) {
                    if (model.Priority < 0 || model.Priority > 1000) {
                        ModelState.AddModelError("priority", "Priority must be between 0 and 1000");
                    }
                    menuItem.Priority = (int)model.Priority;
                }
                if(model.LinkSet) {
                    menuItem.Link = model.Link;
                }
                if(model.PostIdSet) {
                    if(model.PostId != null) {
                        var post = postRepository.GetPostByPostId((int)model.PostId);
                        if (post == null) {
                            ModelState.AddModelError("postid", "Could not find post");
                        }
                    }
                    menuItem.PostId = model.PostId;
                }
                if (!ModelState.IsValid) {
                    return BadRequest(ModelStateHelper.GetModelStateErrors(ModelState));
                }
                menuRepository.UpdateMenuItem(menuItem);
                return Ok(Conversions.MenuItemViewModelFromMenuItem(menuItem));
            }
            catch (Exception) {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("MenuItem")]
        [Authorize]
        public IActionResult CreateMenuItem([FromBody] CreateMenuItemViewModel model) {
            var parentMenu = menuRepository.GetMenuByName(model.ParentMenuName);
            if (parentMenu == null) {
                ModelState.AddModelError("parentMenu", "Could not find parent menu");
            }
            var menuItem = new MenuItem() {
                Link = model.Link,
                Name = model.Name,
                ParentMenuItemId = model.ParentMenuItemId,
                PostId = model.PostId,
                Priority = model.Priority,
                ParentMenuName = model.ParentMenuName,
            };
            if(!ModelState.IsValid) {
                return BadRequest(ModelState.GetModelStateErrors());
            }
            try {
                menuItem = menuRepository.CreateMenuItem(menuItem);
                return Ok(Conversions.MenuItemViewModelFromMenuItem(menuItem));
            }
            catch (Exception e) {
                ModelState.AddModelError("DB", e.Message);
                return BadRequest(ModelState.GetModelStateErrors());
            }
        }

        [HttpDelete]
        [Route("MenuItem/{id}")]
        [Authorize]
        public IActionResult RemoveMenuItem([FromRoute] int id) {
            var menuItem = menuRepository.GetMenuItemByMenuItemId(id);
            if (menuItem == null) {
                return NotFound();
            }
            menuRepository.RemoveMenuItem(menuItem);
            return Ok();
        }
    }
}
