using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.Entities {
    public class RawText {
            [MaxLength(191)]
            [Key, ForeignKey("PostVersion")]
            public int PostVersionId { get; set; }
            public string Text { get; set; }
    }
}
