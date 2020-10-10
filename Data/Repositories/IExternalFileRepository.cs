using iNOBStudios.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Data.Repositories {
    public interface IExternalFileRepository {
        public ExternalFile GetExternalFileByFileName(string fileName, bool track = false, string[] info = null);
        public IEnumerable<ExternalFile> GetExternalFilesByPostId(string fileName, bool track = false, string[] info = null);
        public ExternalFile CreateExternalFile(ExternalFile file);
        public void RemoveFile(ExternalFile file);
    }
}
