using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iNOBStudios.Data.Repositories;
using iNOBStudios.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iNOBStudios.Controllers
{
    [Route("Admin")]
    [ApiController]
    [Authorize]
    public class AdminWebController : ControllerBase
    {
        private IPostRepository postRepository;
        private ITagRepository tagRepository;
        private IMenuRepository menuRepository;

        public AdminWebController(IPostRepository postRepository, ITagRepository tagRepository, IMenuRepository menuRepository) {
            this.postRepository = postRepository;
            this.tagRepository = tagRepository;
            this.menuRepository = menuRepository;
        }

        [HttpGet]
        [Route("Posts")]
        public IActionResult Posts() {
            var posts = postRepository.GetPosts(false, new string[] { "PostVersions", "CurrentVersion" });
            return Ok(posts.Select(x => Conversions.PostViewModelFromPost(x)).ToDictionary(x => x.PostId.ToString(), x => x));
        }

        [HttpGet]
        [Route("Menus")]
        public IActionResult Menus() {
            var menus = menuRepository.GetMenus(false, new string[] { "MenuItems" });
            return Ok(menus.Select(x => Conversions.MenuViewModelFromMenu(x)).ToDictionary(x => x.Name, x => x));
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit([FromRoute]int id) {
            var postVersion = postRepository.GetPostVersionByPostVersionId(id, false, new string[] { "RawText" });
            if (postVersion == null) {
                return NotFound();
            }
            var post = postRepository.GetPostByPostId(postVersion.PostId, false, new string[] { "ExternalFiles", "PostTags" });
            var tags = tagRepository.GetTags();
            return Ok(new Dictionary<string, object>(){ {"post", Conversions.PostViewModelFromPost(post) }, {"postVersion", Conversions.PostVersionViewModelFromPostVersion(postVersion) },
                { "tags", tags.Select(x => x.TagId)} });

        }

    }
}