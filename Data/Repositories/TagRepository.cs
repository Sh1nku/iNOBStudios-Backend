using iNOBStudios.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Data.Repositories {
    public class TagRepository : ITagRepository {
        private ApplicationDbContext db;
        public TagRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public Tag CreateTag(Tag tag) {
            db.Tags.Add(tag);
            db.SaveChanges();
            return tag;
        }

        public Tag GetTagByTagId(string tag, bool track = false, string[] info = null) {

            var tags = db.Tags.AsQueryable();
            foreach (var include in info ?? Enumerable.Empty<string>()) {
                tags = tags.Include(include);
            }
            if (track) {
                return tags.Where(x => x.TagId == tag).SingleOrDefault();
            }
            return tags.Where(x => x.TagId == tag).AsNoTracking().SingleOrDefault();
        }

        public IEnumerable<Tag> GetTags() {
            return db.Tags;
        }
    }
}
