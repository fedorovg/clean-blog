using Blog.Application.Posts.Commands;
using FluentValidation;

namespace Blog.Application.Posts.Validators
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(updatePostCommand => updatePostCommand.Title).NotEmpty().MaximumLength(256);
            RuleFor(updatePostCommand => updatePostCommand.Content).NotEmpty();
        }
    }
}