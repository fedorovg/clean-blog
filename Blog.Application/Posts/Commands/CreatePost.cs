using System;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Interfaces;
using Blog.Domain;
using MediatR;

namespace Blog.Application.Posts.Commands
{
    public class CreatePostCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
    }

    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
    {
        private readonly IBlogDbContext _context;

        public CreatePostCommandHandler(IBlogDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = new Post()
            {
                Title = request.Title,
                Content = request.Content,
                UserId = request.UserId,
                Created = DateTime.Now,
                Updated = null
            };
            await _context.Posts.AddAsync(post, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return post.Id;
        }
    }
}