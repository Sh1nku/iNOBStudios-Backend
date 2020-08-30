using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using iNOBStudios.Data.Repositories;
using iNOBStudios.Models;
using iNOBStudios.Models.Entities;
using iNOBStudios.Models.ViewModels.Post;
using iNOBStudios.Models.ViewModels.PostVersion;
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
        [Route("Post")]
        public IActionResult CreatePost([FromBody] CreatePostViewModel model) {
            var user = userRepository.GetApplicationUserByUsername(User.Identity.Name, true);
            if(user == null) {
                return Unauthorized();
            }
            var rawText = new RawText() {
                Text = ""
            };
            var postVersion = new PostVersion() {
                Title = model.Title,
                RawText = rawText,
                PreviewText = ""
            };
            var post = new Post() {
                Author = user,
                Published = false,
                CurrentVersion = postVersion,
                PostVersions = new List<PostVersion>() { postVersion },
                AddedTime = DateTime.Now
            };
            try {
                post = postRepository.CreatePost(post);
                return Ok(Conversions.PostViewModelFromPost(post));
            }
            catch (Exception) {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Post")]
        public IActionResult UpdatePost([FromBody] UpdatePostViewModel model) {
            var user = userRepository.GetApplicationUserByUsername(User.Identity.Name, true);
            if (user == null) {
                return Unauthorized();
            }
            var post = postRepository.GetPostByPostId(model.PostId, true, new string[] { "CurrentVersion", "PostTags", "ExternalFiles" });
            if (post == null) {
                return NotFound();
            }
            try {
                bool errors = false;
                if (model.CurrentVersion != null) {
                    var postVersion = postRepository.GetPostVersionByPostVersionId((int)model.CurrentVersion);
                    if(postVersion == null) {
                        errors = true;
                        ModelState.AddModelError("PostVersion", "PostVersion not found");
                    }
                    post.CurrentVersion = postVersion;
                }
                if (model.Published != null) {
                    post.Published = (bool)model.Published;
                }
                if (errors) {
                    return BadRequest(ModelStateHelper.GetModelStateErrors(ModelState));
                }
                postRepository.UpdatePost(post);
                return Ok(Conversions.PostViewModelFromPost(post));
            }
            catch (Exception) {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost]
        [Route("PostVersion")]
        public IActionResult CreatePostVersion([FromBody] CreatePostVersionViewModel model) {
            var user = userRepository.GetApplicationUserByUsername(User.Identity.Name, true);
            if (user == null) {
                return Unauthorized();
            }
            var rawText = new RawText() {
                Text = ""
            };
            var postVersion = new PostVersion() {
                Title = model.Title,
                RawText = rawText,
                PostId = model.PostId,
                PreviewText = ""
            };
            try {
                postVersion = postRepository.CreatePostVersion(postVersion);
                return Ok(Conversions.PostVersionViewModelFromPostVersion(postVersion));
            }
            catch (Exception) {
                return BadRequest();
            }
        }
    }
}