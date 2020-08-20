using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.Entities {
    public class ExternalFile {
        [Key]
        [MaxLength(191)]
        public string FileName { get; set; }
        [MaxLength(128)]
        public string MIMEType { get; set; }
        public virtual RawFile RawFile { get; set; }
        public DateTime PostedTime { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
