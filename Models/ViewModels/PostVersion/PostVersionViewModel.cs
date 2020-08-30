using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.ViewModels.PostVersion {
    public class PostVersionViewModel {
        public int PostVersionId { get; set; }
        public int PostId { get; set; }
        public int? CurrentVersionId { get; set; }
        public DateTime PostedDate { get; set; }
        public string Title { get; set; }
        public string PreviewText { get; set; }
        public string RawText { get; set; }
    }
}
