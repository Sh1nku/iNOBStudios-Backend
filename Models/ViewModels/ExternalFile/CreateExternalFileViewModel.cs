using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.ViewModels.ExternalFile {
    public class CreateExternalFileViewModel {
        [Required]
        public IFormFile RawFile { set; get; }
        [Required]
        public int PostId { get; set; }
    }
}
