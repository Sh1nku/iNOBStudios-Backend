using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models.Entities {
    public class MenuItem {
        [Key]
        public int MenuItemId { get; set; }

        [ForeignKey("ParentMenuName")]
        [Required]
        public string ParentMenuName { get; set; }
        public virtual Menu ParentMenu { get; set; }

        [ForeignKey("ParentMenuItemId")]
        public int? ParentMenuItemId { get; set; }
        public virtual MenuItem ParentMenuItem { get; set; }
        public virtual IEnumerable<MenuItem> ChildMenuItems { get; set; }

        public string Name { get; set; }
        public int Priority { get; set; }
        //Could either be an external link, or a Post
        public string? Link { get; set; }


        public int? PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
