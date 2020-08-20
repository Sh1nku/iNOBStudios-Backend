using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.Entities {
    public class PostVersion {
        [Key]
        public int PostVersionId { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public DateTime PostedDate { get; set; }
        [MaxLength(191)]
        public string Title { get; set; }
        public virtual RawText RawText { get; set; }

    }
}
