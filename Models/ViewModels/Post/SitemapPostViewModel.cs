using System;

namespace iNOBStudios.Models.ViewModels.Post
{
    public class SitemapPostViewModel
    {
        public int PostId { get; set; }
        public DateTime FirstPublished { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Alias { get; set; }
        public string Title { get; set; }
    }
}