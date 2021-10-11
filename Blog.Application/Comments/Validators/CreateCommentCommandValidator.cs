using Blog.Application.Comments.Commands;
using FluentValidation;

namespace Blog.Application.Comments.Validators
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(createCommentCommand => createCommentCommand.Text).NotEmpty().MaximumLength(512);
        }
    }
}