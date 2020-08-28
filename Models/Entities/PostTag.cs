using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.Entities {
    public class PostTag {
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        [MaxLength(64)]
        public string TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
