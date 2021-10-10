using System;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Common.Exceptions;
using Blog.Application.Interfaces;
using Blog.Domain;
using MediatR;

namespace Blog.Application.Posts.Commands
{
    public class DeletePostCommand : IRequest
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
    }

    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IBlogDbContext _context;

        public DeletePostCommandHandler(IBlogDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.FindAsync(new object[] {request.Id}, cancellationToken);

            if (post is null || post.UserId != request.UserId)
                throw new ResourceNotFoundException(nameof(Post), request.Id);

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}