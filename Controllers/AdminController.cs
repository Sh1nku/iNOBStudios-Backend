using iNOBStudios.Data.Repositories;
using iNOBStudios.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Controllers {
    public class AdminController : Controller {
        private IUserRepository userRepository;
        private IPostRepository postRepository;
        private ITagRepository tagRepository;

        public AdminController(IUserRepository userRepository, IPostRepository postRepository, ITagRepository tagRepository) {
            this.userRepository = userRepository;
            this.postRepository = postRepository;
            this.tagRepository = tagRepository;
        }

        [Authorize]
        public IActionResult Index() {
            var posts = postRepository.GetPosts(false, new string[] { "PostVersions", "CurrentVersion" });
            return View(posts.Select(x => Conversions.PostViewModelFromPost(x)).ToDictionary(x=> x.PostId.ToString(), x => x));
        }

        [Authorize]
        public IActionResult Edit([FromRoute]int id) {
            var postVersion = postRepository.GetPostVersionByPostVersionId(id, false, new string[] {"RawText"});
            if(postVersion == null) {
                return NotFound();
            }
            var post = postRepository.GetPostByPostId(postVersion.PostId, false, new string[] {"ExternalFiles", "PostTags"});
            var tags = tagRepository.GetTags();
            return View(new Dictionary<string, object>(){ {"post", Conversions.PostViewModelFromPost(post) }, {"postVersion", Conversions.PostVersionViewModelFromPostVersion(postVersion) },
                { "tags", tags.Select(x => x.TagId)} });

        }
    }
}
