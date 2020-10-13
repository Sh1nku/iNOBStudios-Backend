using iNOBStudios.Data.Repositories;
using iNOBStudios.Models.Entities;
using iNOBStudios.Models.ViewModels.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Controllers {
    [Route("api/Tag")]
    [ApiController]
    public class TagWebController : ControllerBase {
        private IUserRepository userRepository;
        private ITagRepository tagRepository;

        public TagWebController(IUserRepository userRepository, ITagRepository tagRepository) {
            this.userRepository = userRepository;
            this.tagRepository = tagRepository;
        }

        [Authorize]
        [HttpPost]
        [Route("Tag")]
        public IActionResult CreateTag([FromBody] CreateTagViewModel model) {
            var user = userRepository.GetApplicationUserByUsername(User.Identity.Name, true);
            if (user == null) {
                return Unauthorized();
            }
            try {
                Tag tag = tagRepository.CreateTag(new Tag() {TagId = model.Name });
                return Ok(tag.TagId);
            }
            catch (Exception) {
                return BadRequest();
            }
        }
    }
}
