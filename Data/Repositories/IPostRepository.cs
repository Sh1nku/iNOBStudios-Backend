using iNOBStudios.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Data.Repositories {
    public interface IPostRepository {
        public IEnumerable<Post> GetPosts(bool track = false, string[] info = null);
        public Post GetPostByPostId(int postId, bool track = false, string[] info = null);
        public Post GetPostByAlias(string alias, bool track = false, string[] info = null);
        public IEnumerable<PostVersion> GetPostVersionsByPostId(int postId, bool track = false, string[] info = null);
        public PostVersion GetPostVersionByPostVersionId(int postVersionId, bool track = false, string[] info = null);

        public Post CreatePost(Post post);
        public Post UpdatePost(Post post);
        public PostVersion CreatePostVersion(PostVersion postVersion);
        public PostVersion UpdatePostVersion(PostVersion postVersion);
    }
}
