using Blog.Application.Comments.Commands;
using Blog.Application.Common.Mappings;

namespace Blog.Api.Models.Comments
{
    public class UpdateCommentDto : IMapped<UpdateCommentCommand>
    {
        public string Text { get; set; }
    }
}