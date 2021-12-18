using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.ViewModels.Menu {
    public sealed class UpdateMenuItemViewModel {
        public int MenuItemId { get; set; }

        private int? _parentMenuItemId;
        [System.Text.Json.Serialization.JsonIgnore]
        public bool ParentMenuItemIdSet { get; private set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public int? ParentMenuItemId { 
            get => _parentMenuItemId; 
            set {
                _parentMenuItemId = value;
                ParentMenuItemIdSet = true;
            }
        }


        public string? Name { get; set; }
        public int? Priority { get; set; }

        private string? _link;
        [System.Text.Json.Serialization.JsonIgnore]
        public bool LinkSet { get; private set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string? Link {
            get => _link;
            set {
                _link = value;
                LinkSet = true;
            } 
        }

        private int? _postId;
        [System.Text.Json.Serialization.JsonIgnore]
        public bool PostIdSet { get; private set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public int? PostId {
            get => _postId;
            set {
                _postId = value;
                PostIdSet = true;
            }
        }
    }
}
