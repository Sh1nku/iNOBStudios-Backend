using iNOBStudios.Models.Entities;
using iNOBStudios.Models.ViewModels.ExternalFile;
using iNOBStudios.Models.ViewModels.Post;
using iNOBStudios.Models.ViewModels.PostVersion;
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
                PostVersions = post.PostVersions?.Select(x => PostVersionViewModelFromPostVersion(x)).ToList(),
                CurrentVersion = post.CurrentVersion != null ? PostVersionViewModelFromPostVersion(post.CurrentVersion) : null,
                Published = post.Published
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
                RawText = version.RawText != null ? version.RawText.Text : null
            };
        }


    }
}
