using System;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Common.Exceptions;
using Blog.Application.Interfaces;
using Blog.Domain;
using MediatR;

namespace Blog.Application.Comments.Commands
{
    public class UpdateCommentCommand : IRequest
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
    }

    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
    {
        private readonly IBlogDbContext _context;

        public UpdateCommentCommandHandler(IBlogDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments.FindAsync(new object[] {request.Id}, cancellationToken);

            if (comment is null || comment.UserId != request.UserId)
                throw new ResourceNotFoundException(nameof(Comment), request.Id);

            comment.Text = request.Text;
            comment.Updated = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}