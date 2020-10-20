using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using iNOBStudios.Models;
using iNOBStudios.Data.Repositories;
using iNOBStudios.Models.Entities;

namespace iNOBStudios.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IPostRepository postRepository;
        private ITagRepository tagRepository;

        public HomeController(ILogger<HomeController> logger, IPostRepository postRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            this.postRepository = postRepository;
            this.tagRepository = tagRepository;
        }

        public IActionResult Index([FromQuery] int offset = 0, int limit = 10, string tag = null)
        {
            limit = limit > 10 ? 10 : limit;
            IEnumerable<Post> posts = new List<Post>();
            var model = new Dictionary<string, Object>();
            if (tag != null) {
                var tagEntity = tagRepository.GetTagByTagId(tag, true, new string[] { "PostTags.Post.CurrentVersion", "PostTags.Post.PostTags" });
                if(tagEntity != null) {
                    posts = tagEntity.PostTags.Select(x => x.Post);
                    model.Add("tag", tag);
                }
            }
            else {
                posts = postRepository.GetPosts(false, new string[] { "CurrentVersion", "PostTags" });
            }
            posts = posts.Where(x => x.Published).OrderByDescending(x => x.FirstPublished).Skip(offset).Take(limit);
            model.Add("posts", posts.Select(x => Conversions.PostViewModelFromPost(x))) ;
            model.Add("offset", offset);
            model.Add("limit", limit);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
