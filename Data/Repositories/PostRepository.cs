using iNOBStudios.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Data.Repositories {
    public class PostRepository : IPostRepository {
        private ApplicationDbContext db;
        public PostRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public Post GetPostByPostId(int postId, bool track = false, string[] info = null) {
            var post = db.Posts.AsQueryable();
            foreach (var include in info ?? Enumerable.Empty<string>()) {
                post = post.Include(include);
            }
            if (track) {
                return post.Where(x => x.PostId == postId).SingleOrDefault();
            }
            return post.Where(x => x.PostId == postId).AsNoTracking().SingleOrDefault();
        }

        public IEnumerable<PostVersion> GetPostVersionsByPostId(int postId, bool track = false, string[] info = null) {
            var posts = db.PostVersions.AsQueryable();
            foreach (var include in info ?? Enumerable.Empty<string>()) {
                posts = posts.Include(include);
            }
            if (track) {
                return posts.Where(x => x.PostId == postId);
            }
            return posts.Where(x => x.PostId == postId).AsNoTracking();
        }
    }
}
