using iNOBStudios.Data.Repositories;
using iNOBStudios.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Controllers {
    public class PostController : Controller {
        private IUserRepository userRepository;
        private IPostRepository postRepository;
        
        public PostController(IUserRepository userRepository, IPostRepository postRepository) {
            this.userRepository = userRepository;
            this.postRepository = postRepository;
        }
        [Route("Post/{postId}")]
        [Route("Post/{postId}/{postName}")]
        public IActionResult Index([FromRoute] int postId,[FromRoute] string postName) {
            var post = postRepository.GetPostByPostId(postId, false, new string[] { "CurrentVersion.RawText", "PostTags"});
            if(post == null) {
                return NotFound();
            }
            if(!post.Published) {
                return Forbid();
            }
            return View(Conversions.PostViewModelFromPost(post));
        }
    }
}
