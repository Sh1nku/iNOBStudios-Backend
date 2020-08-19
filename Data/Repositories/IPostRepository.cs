using iNOBStudios.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Data.Repositories {
    public interface IPostRepository {
        public Post GetPostByPostId(int postId, bool track = false, string[] info = null);
    }
}
