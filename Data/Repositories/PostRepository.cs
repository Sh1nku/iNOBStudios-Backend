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

        public Post CreatePost(Post post) {
            db.Posts.Add(post);
            db.SaveChanges();
            return post;
        }

        public PostVersion CreatePostVersion(PostVersion postVersion) {
            db.PostVersions.Add(postVersion);
            db.SaveChanges();
            return postVersion;
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

        public IEnumerable<Post> GetPosts(bool track = false, string[] info = null) {
            var posts = db.Posts.AsQueryable();
            foreach (var include in info ?? Enumerable.Empty<string>()) {
                posts = posts.Include(include);
            }
            if (track) {
                return posts;
            }
            return posts.AsNoTracking();
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

        public PostVersion GetPostVersionByPostVersionId(int postVersionId, bool track = false, string[] info = null) {
            var postVersion = db.PostVersions.AsQueryable();
            foreach (var include in info ?? Enumerable.Empty<string>()) {
                postVersion = postVersion.Include(include);
            }
            if (track) {
                return postVersion.Where(x => x.PostVersionId == postVersionId).SingleOrDefault();
            }
            return postVersion.Where(x => x.PostVersionId == postVersionId).AsNoTracking().SingleOrDefault();
        }

        public Post UpdatePost(Post post) {
            db.Posts.Update(post);
            db.SaveChanges();
            return post;
        }
    }
}
