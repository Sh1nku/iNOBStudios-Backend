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
    [Route("Post")]
    [ApiController]
    public class PostWebController : ControllerBase
    {
        private IUserRepository userRepository;
        private IPostRepository postRepository;
        private ITagRepository tagRepository;


        public PostWebController(IUserRepository userRepository, IPostRepository postRepository, ITagRepository tagRepository) {
            this.userRepository = userRepository;
            this.postRepository = postRepository;
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        [Route("Posts")]
        public IActionResult GetPosts([FromQuery] int offset = 0, int limit = 10, string tag = null) {
            limit = limit > 10 ? 10 : limit;
            IEnumerable<Post> posts = new List<Post>();
            var model = new Dictionary<string, Object>();
            if (tag != null) {
                var tagEntity = tagRepository.GetTagByTagId(tag, true, new string[] { "PostTags.Post.CurrentVersion", "PostTags.Post.PostTags" });
                if (tagEntity != null) {
                    posts = tagEntity.PostTags.Select(x => x.Post);
                    model.Add("tag", tag);
                }
            }
            else {
                posts = postRepository.GetPosts(false, new string[] { "CurrentVersion", "PostTags" });
            }
            posts = posts.Where(x => x.Published && x.List).OrderByDescending(x => x.FirstPublished).Skip(offset).Take(limit);
            model.Add("posts", posts.Select(x => Conversions.PostViewModelFromPost(x)));
            model.Add("offset", offset);
            model.Add("limit", limit);
            return Ok(model);
        }

        [HttpGet]
        [Route("Post/{postId:int}")]
        [Route("Post/{postId:int}/{postName}")]
        public IActionResult GetPostByPostId([FromRoute] int? postId) {
            Post post = null;
            if (postId != null) {
                post = postRepository.GetPostByPostId((int)postId, false, new string[] { "CurrentVersion.RawText", "PostTags" });
            }
            if (post == null) {
                return NotFound();
            }
            if (!post.Published && !User.Identity.IsAuthenticated) {
                return Forbid();
            }
            return Ok(Conversions.PostViewModelFromPost(post));
        }

        [HttpGet]
        [Route("Post/{alias}")]
        public IActionResult GetPostByAlias([FromRoute] string? alias) {
            Post post = null;
            if (alias != null) {
                post = postRepository.GetPostByAlias(alias, false, new string[] { "CurrentVersion.RawText", "PostTags" });
            }
            if (post == null) {
                return NotFound();
            }
            if (!post.Published && !User.Identity.IsAuthenticated) {
                return Forbid();
            }
            return Ok(Conversions.PostViewModelFromPost(post));
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
                AddedTime = DateTime.Now,
                FirstPublished = null,
                List = true
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
                if (model.AliasSet) {
                    post.Alias = model.Alias;
                }
                if (model.Published != null) {
                    post.Published = (bool)model.Published;
                    if(post.FirstPublished == null) {
                        post.FirstPublished = DateTime.Now;
                    }
                }
                if (model.List != null) {
                    post.List = (bool)model.List;
                }
                if (model.PostTags != null) {
                    var newTags = new List<PostTag>();
                    var allTags = tagRepository.GetTags().ToDictionary(x => x.TagId, x => x);
                    foreach(var tag in model.PostTags) {
                        if(!allTags.ContainsKey(tag)) {
                            return BadRequest();
                        }
                        newTags.Add(new PostTag() { PostId = post.PostId, TagId = tag });
                    }
                    post.PostTags = newTags;

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

        [Authorize]
        [HttpPut]
        [Route("PostVersion")]
        public IActionResult UpdatePostVersion([FromBody] UpdatePostVersionViewModel model) {
            var user = userRepository.GetApplicationUserByUsername(User.Identity.Name, true);
            if (user == null) {
                return Unauthorized();
            }
            var postVersion = postRepository.GetPostVersionByPostVersionId(model.PostVersionId, true, new string[] { "RawText" });
            if(postVersion == null) {
                return NotFound();
            }
            if(model.RawText != null) {
                postVersion.RawText.Text = model.RawText;
                postVersion.PreviewText = model.PreviewText;
            }
            if (model.Title != null && model.Title.Length > 0) {
                postVersion.Title = model.Title;
            }
            try {
                postVersion.PostedDate = DateTime.Now;
                postVersion = postRepository.UpdatePostVersion(postVersion);
                return Ok(Conversions.PostVersionViewModelFromPostVersion(postVersion));
            }
            catch (Exception) {
                return BadRequest();
            }
        }
    }
}