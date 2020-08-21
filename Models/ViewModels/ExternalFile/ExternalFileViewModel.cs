using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.ViewModels.ExternalFile {
    public class ExternalFileViewModel {
        public string FileName { get; set; }
        public string MIMEType { get; set; }
        public DateTime PostedTime { get; set; }
        public int PostId { get; set; }
    }
}
