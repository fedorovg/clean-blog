using Blog.Application.Posts.Commands;
using FluentValidation;

namespace Blog.Application.Posts.Validators
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(createPostCommand => createPostCommand.Title).NotEmpty().MaximumLength(256);
            RuleFor(createPostCommand => createPostCommand.Content).NotEmpty();
        }
    }
}