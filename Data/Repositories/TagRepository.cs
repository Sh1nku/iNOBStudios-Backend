using iNOBStudios.Models.Entities;
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

        public Tag GetTagByTagId(string tag) {
            return this.db.Tags.Find(tag);
        }

        public IEnumerable<Tag> GetTags() {
            return db.Tags;
        }
    }
}
