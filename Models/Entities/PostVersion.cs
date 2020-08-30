using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.Entities {
    public class PostVersion {
        [Key]
        public int PostVersionId { get; set; }
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        [ForeignKey("CurrentVersion")]
        public int? CurrentVersionId { get; set; }
        public virtual Post CurrentVersion { get; set; }
        public DateTime PostedDate { get; set; }
        [MaxLength(191)]
        public string Title { get; set; }
        [MaxLength(511)]
        public string PreviewText { get; set; }
        public virtual RawText RawText { get; set; }

    }
}
