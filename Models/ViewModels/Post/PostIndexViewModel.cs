using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.ViewModels.Post {
    public class PostIndexViewModel {
        int Offset { get; set; }
        int Limit { get; set; }
        List<PostViewModel> Posts { get; set; }
    }
}
