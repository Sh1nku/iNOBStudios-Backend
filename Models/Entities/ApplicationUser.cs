using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.Entities {
    public class ApplicationUser : IdentityUser {
        [MaxLength(191)]
        public string ProfilePicture { get; set; }

    }
}
