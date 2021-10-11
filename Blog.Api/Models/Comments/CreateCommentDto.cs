using Blog.Application.Comments.Commands;
using Blog.Application.Common.Mappings;

namespace Blog.Api.Models.Comments
{
    public class CreateCommentDto : IMapped<CreateCommentCommand>
    {
        public string Text { get; set; }
        public int PostId { get; set; }
    }
}