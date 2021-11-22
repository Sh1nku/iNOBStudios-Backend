﻿using iNOBStudios.Models.Entities;
using iNOBStudios.Models.ViewModels.ExternalFile;
using iNOBStudios.Models.ViewModels.Menu;
using iNOBStudios.Models.ViewModels.Post;
using iNOBStudios.Models.ViewModels.PostVersion;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models {
    public class Conversions {
        public static PostViewModel PostViewModelFromPost(Post post) {
            return new PostViewModel() {
                AuthorId = post.AuthorId,
                PostId = post.PostId,
                ExternalFiles = post.ExternalFiles?.Select(x => ExternalFileViewModelFromExternalFile(x)).ToList(),
                PostTags = post.PostTags?.Select(x => x.TagId).ToList(),
                PostVersions = post.PostVersions?.Select(x => PostVersionViewModelFromPostVersion(x)).ToDictionary(x => x.PostVersionId.ToString(), x => x),
                CurrentVersion = post.CurrentVersion != null ? PostVersionViewModelFromPostVersion(post.CurrentVersion) : null,
                Published = post.Published,
                AddedTime = post.AddedTime,
                FirstPublished = post.FirstPublished
            };
        }

        public static ExternalFileViewModel ExternalFileViewModelFromExternalFile(ExternalFile externalFile) {
            return new ExternalFileViewModel() {
                FileName = externalFile.FileName,
                MIMEType = externalFile.MIMEType,
                PostId = externalFile.PostId,
                PostedTime = externalFile.PostedTime
            };
        }

        public static PostVersionViewModel PostVersionViewModelFromPostVersion(PostVersion version) {
            return new PostVersionViewModel() {
                PostId = version.PostId,
                PostedDate = version.PostedDate,
                PostVersionId = version.PostVersionId,
                Title = version.Title,
                CurrentVersionId = version.CurrentVersionId,
                PreviewText = version.PreviewText,
                RawText = version.RawText != null ? version.RawText.Text : null
            };
        }

        public static MenuViewModel MenuViewModelFromMenu(Menu menu) {
            return new MenuViewModel() {
                Name = menu.Name,
                MenuItems = menu.MenuItems?.Select(x => MenuItemViewModelFromMenuItem(x)).ToList(),
                JSON = menu.JSON
            };
        }

        public static MenuItemViewModel MenuItemViewModelFromMenuItem(MenuItem menuItem) {
            return new MenuItemViewModel() {
                MenuItemId = menuItem.MenuItemId,
                Name = menuItem.Name,
                Priority = menuItem.Priority,
                Link = menuItem.Link,
                ChildMenuItems = menuItem.ChildMenuItems?.Select(x => MenuItemViewModelFromMenuItem(x)).ToList(),
                PostId = menuItem.PostId,
                ParentMenuName = menuItem.ParentMenuName,
                ParentMenuItemId = menuItem.ParentMenuItemId
            };
        }

        //This does not need to be fast, as it only gets updated  when the menu updates
        public static string MenuJSONFromMenuItemViewModels(List<MenuItemViewModel> menuItems) {
            var returnItems = new List<MenuItemViewModel>();
            for (int i = 0; i < menuItems.Count; i++) {
                if(menuItems[i].ParentMenuItemId == null) {
                    returnItems.Add(RecursiveSortMenuItemViewModel(menuItems, menuItems[i]));
                }
            }
            return JsonConvert.SerializeObject(returnItems.OrderByDescending(x => x.Priority), new JsonSerializerSettings {
                ContractResolver = new DefaultContractResolver() { 
                     NamingStrategy = new CamelCaseNamingStrategy()
                }
            });
        }

        protected static MenuItemViewModel RecursiveSortMenuItemViewModel(List<MenuItemViewModel> items, MenuItemViewModel item) {
            List<MenuItemViewModel> children = new List<MenuItemViewModel>();
            for (int i = 0; i < items.Count; i++) {
                if (items[i].ParentMenuItemId == item.MenuItemId) {
                    children.Add(RecursiveSortMenuItemViewModel(items, items[i]));
                }
            }
            item.ChildMenuItems = children.OrderByDescending(x => x.Priority).ToList();
            return item;
        }




    }
}
