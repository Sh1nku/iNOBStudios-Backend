using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using iNOBStudios.Models;
using iNOBStudios.Data.Repositories;

namespace iNOBStudios.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IPostRepository postRepository;

        public HomeController(ILogger<HomeController> logger, IPostRepository postRepository)
        {
            _logger = logger;
            this.postRepository = postRepository;
        }

        public IActionResult Index([FromQuery] int offset = 0, int limit = 10)
        {
            limit = limit > 10 ? 10 : limit;
            var posts = postRepository.GetPosts(false, new string[] { "CurrentVersion", "PostTags"}).Where(x => x.Published).OrderByDescending(x => x.FirstPublished).Skip(offset).Take(limit);
            var model = new Dictionary<string, Object>() {
                { "posts", posts.Select(x => Conversions.PostViewModelFromPost(x)) },
                { "offset", offset},
                { "limit", limit}
            };
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
