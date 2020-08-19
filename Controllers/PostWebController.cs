using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iNOBStudios.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace iNOBStudios.Controllers
{
    [Route("api/Posts")]
    [ApiController]
    public class PostWebController : ControllerBase
    {
        private IPostRepository postRepository;

        public PostWebController(IPostRepository postRepository) {
            this.postRepository = postRepository;
        }


    }
}