using System;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Interfaces;
using Blog.Domain;
using MediatR;

namespace Blog.Application.Comments.Commands
{
    public class CreateCommentCommand : IRequest<int>
    {
        public string Text { get; set; }
        public int PostId { get; set; }
        public Guid UserId { get; set; }
    }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
    {
        private readonly IBlogDbContext _context;

        public CreateCommentCommandHandler(IBlogDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Comment()
            {
                Text = request.Text,
                UserId = request.UserId,
                Created = DateTime.Now,
                Updated = null,
                PostId = request.PostId
            };
            await _context.Comments.AddAsync(comment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return comment.Id;
        }
    }
}