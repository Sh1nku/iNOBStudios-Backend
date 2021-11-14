using iNOBStudios.Data.Repositories;
using iNOBStudios.Models;
using iNOBStudios.Models.Entities;
using iNOBStudios.Models.ViewModels.ExternalFile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Controllers {
    [Route("ExternalFile")]
    [ApiController]
    public class ExternalFileWebController : ControllerBase {
        private IUserRepository userRepository;
        private IPostRepository postRepository;
        private IExternalFileRepository externalFileRepository;


        public ExternalFileWebController(IUserRepository userRepository, IPostRepository postRepository, IExternalFileRepository externalFileRepository) {
            this.userRepository = userRepository;
            this.postRepository = postRepository;
            this.externalFileRepository = externalFileRepository;
        }

        [Authorize]
        [HttpPost]
        [Route("ExternalFile")]
        public IActionResult CreateFile([FromForm] CreateExternalFileViewModel model) {
            ApplicationUser user = userRepository.GetApplicationUserByUsername(User.Identity.Name);
            Post post = postRepository.GetPostByPostId(model.PostId, true);
            if(post == null) {
                return NotFound();
            }
            if( user == null) {
                return Unauthorized();
            }
            var time = DateTime.Now.ToBinary();
            ExternalFile file = new ExternalFile() {
                FileName = model.RawFile.FileName+time,
                MIMEType = model.RawFile.ContentType,
                PostId = post.PostId,
                PostedTime = DateTime.Now
            };

            RawFile rawFile = new RawFile() {
                Data = GetByteArrayFromFile(model.RawFile),
                FileName = model.RawFile.FileName+time,
            };
            file.RawFile = rawFile;
            try {
                externalFileRepository.CreateExternalFile(file);
            }
            catch (Exception) {
                return BadRequest();
            }
            return new OkObjectResult(Conversions.ExternalFileViewModelFromExternalFile(file));
        }

        [Authorize]
        [HttpDelete]
        [Route("ExternalFile/{filename}")]
        public IActionResult RemoveFile(string filename) {
            ApplicationUser user = userRepository.GetApplicationUserByUsername(User.Identity.Name);
            if (user == null) {
                return Unauthorized();
            }
            var file = externalFileRepository.GetExternalFileByFileName(filename, true);
            if(file == null) {
                return NotFound();
            }
            externalFileRepository.RemoveFile(file);
            return Ok();

        }

        [HttpGet]
        [Route("{filename}")]
        public IActionResult GetFile(string filename) {
            var file = externalFileRepository.GetExternalFileByFileName(filename, false, new string[] { "RawFile" });
            if (file == null) {
                return NotFound();
            }
            return File(file.RawFile.Data, file.MIMEType, file.FileName.Substring(0, file.FileName.LastIndexOf('-')));

        }

        private byte[] GetByteArrayFromFile(IFormFile file) {
            using (var target = new MemoryStream()) {
                file.CopyTo(target);
                return target.ToArray();
            }
        }
    }
}
