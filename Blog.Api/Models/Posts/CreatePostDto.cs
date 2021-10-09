using Blog.Application.Common.Mappings;
using Blog.Application.Posts.Commands;

namespace Blog.Api.Models.Posts
{
    public class CreatePostDto : IMapped<CreatePostCommand>
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}