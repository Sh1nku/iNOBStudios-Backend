using System;
using System.Collections.Generic;
using System.Linq;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace iNOBStudios.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {

        }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

        }

    }
}
