using System;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Common.Exceptions;
using Blog.Application.Interfaces;
using Blog.Domain;
using MediatR;

namespace Blog.Application.Posts.Commands
{
    public class UpdatePostCommand : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
    }

    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
    {
        private readonly IBlogDbContext _context;

        public UpdatePostCommandHandler(IBlogDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.FindAsync(new object[] {request.Id}, cancellationToken);

            if (post is null || post.UserId != request.UserId)
                throw new ResourceNotFoundException(nameof(Post), request.Id);

            post.Content = request.Content;
            post.Title = request.Title;
            post.Updated = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}