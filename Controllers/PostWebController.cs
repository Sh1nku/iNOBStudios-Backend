using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using iNOBStudios.Data.Repositories;
using iNOBStudios.Models;
using iNOBStudios.Models.Entities;
using iNOBStudios.Models.ViewModels.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iNOBStudios.Controllers
{
    [Route("api/Post")]
    [ApiController]
    public class PostWebController : ControllerBase
    {
        private IUserRepository userRepository;
        private IPostRepository postRepository;


        public PostWebController(IUserRepository userRepository, IPostRepository postRepository) {
            this.userRepository = userRepository;
            this.postRepository = postRepository;
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreatePost([FromBody] CreatePostViewModel model) {
            var user = userRepository.GetApplicationUserByUsername(User.Identity.Name, true);
            var post = new Post() {
                Author = user,
                Published = false
            };
            var postVersion = new PostVersion() {
                Post = post,
                Title = model.Title
            };
            var rawText = new RawText() {
                PostVersion = postVersion,
                Text = ""
            };
            try {
                post = postRepository.CreatePost(post);
                return Ok(Conversions.PostViewModelFromPost(post));
            }
            catch (Exception) {
                return BadRequest();
            }
        }
    }
}