using iNOBStudios.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Data.Repositories {
    public interface ITagRepository {
        public IEnumerable<Tag> GetTags();
        public Tag GetTagByTagId(string tag, bool track = false, string[] info = null);
        public Tag CreateTag(Tag tag);
    }
}
