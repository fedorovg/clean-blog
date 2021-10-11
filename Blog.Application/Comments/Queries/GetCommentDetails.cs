using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Mappings;
using Blog.Application.Interfaces;
using Blog.Application.Posts.Queries;
using Blog.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Comments.Queries
{
    public class GetCommentDetailsQuery : IRequest<CommentDetailsVm>
    {
        public int Id { get; set; }
    }

    public class GetCommentDetailsQueryHandler : IRequestHandler<GetCommentDetailsQuery, CommentDetailsVm>
    {
        private readonly IBlogDbContext _context;
        private readonly IMapper _mapper;

        public GetCommentDetailsQueryHandler(IMapper mapper, IBlogDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CommentDetailsVm> Handle(GetCommentDetailsQuery request, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == request.Id,
                cancellationToken);

            if (comment is null)
                throw new ResourceNotFoundException(nameof(Comment), request.Id);

            return _mapper.Map<CommentDetailsVm>(comment);
        }
    }

    public class CommentDetailsVm : IMapped<Comment>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int PostId { get; set; }
        public Guid UserId { get; set; }
    }
}