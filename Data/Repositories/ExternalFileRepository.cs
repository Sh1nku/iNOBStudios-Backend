using iNOBStudios.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Data.Repositories {
    public class ExternalFileRepository : IExternalFileRepository {
        private ApplicationDbContext db;
        public ExternalFileRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public ExternalFile CreateExternalFile(ExternalFile file) {
            db.ExternalFiles.Add(file);
            db.SaveChanges();
            return file;
        }

        public ExternalFile GetExternalFileByFileName(string fileName, bool track = false, string[] info = null) {
            var file = db.ExternalFiles.AsQueryable();
            foreach (var include in info ?? Enumerable.Empty<string>()) {
                file = file.Include(include);
            }
            if (track) {
                return file.Where(x => x.FileName == fileName).SingleOrDefault();
            }
            return file.Where(x => x.FileName == fileName).AsNoTracking().SingleOrDefault();
        }

        public IEnumerable<ExternalFile> GetExternalFilesByPostId(string fileName, bool track = false, string[] info = null) {
            var files = db.ExternalFiles.AsQueryable();
            foreach (var include in info ?? Enumerable.Empty<string>()) {
                files = files.Include(include);
            }
            if (track) {
                return files.Where(x => x.FileName == fileName);
            }
            return files.Where(x => x.FileName == fileName).AsNoTracking();
        }

        public void RemoveFile(ExternalFile file) {
            db.ExternalFiles.Remove(file);
            db.SaveChanges();
        }
    }
}
