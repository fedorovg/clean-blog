using Blog.Application.Comments.Commands;
using FluentValidation;

namespace Blog.Application.Comments.Validators
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(createCommentCommand => createCommentCommand.Text).NotEmpty().MaximumLength(512);
        }
    }
}