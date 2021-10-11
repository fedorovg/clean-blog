using System;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Common.Exceptions;
using Blog.Application.Interfaces;
using Blog.Domain;
using MediatR;

namespace Blog.Application.Comments.Commands
{
    public class DeleteCommentCommand : IRequest
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
    }

    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly IBlogDbContext _context;

        public DeleteCommentCommandHandler(IBlogDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments.FindAsync(new object[] {request.Id}, cancellationToken);

            if (comment is null || comment.UserId != request.UserId)
                throw new ResourceNotFoundException(nameof(Comment), request.Id);

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}