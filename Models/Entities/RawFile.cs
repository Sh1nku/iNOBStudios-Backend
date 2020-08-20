using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.Entities {
    public class RawFile {
        [MaxLength(191)]
        [Key, ForeignKey("ExternalFile")]
        public string FileName { get; set; }
        public virtual ExternalFile ExternalFile { get; set; }
        public byte[] Data { get; set; }
    }
}
