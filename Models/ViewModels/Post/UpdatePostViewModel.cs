using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.ViewModels.Post {
    public class UpdatePostViewModel {
        [Required]
        public int PostId { get; set; }
        public bool? Published { get; set; }
        public int? CurrentVersion { get; set; }
        public bool? List { get; set; }

        private string? _alias;
        [System.Text.Json.Serialization.JsonIgnore]
        public bool AliasSet { get; private set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string? Alias {
            get => _alias;
            set {
                _alias = value;
                AliasSet = true;
            }
        }
        public List<string> PostTags { get; set; }
        public List<string> ExternalFiles { get; set; }
    }
}
