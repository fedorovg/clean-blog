using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.Application.Common.Mappings;
using Blog.Application.Interfaces;
using Blog.Application.Posts.Queries;
using Blog.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Comments.Queries
{
    public class GetCommentsQuery : IRequest<CommentListVm>
    {
        public Guid UserId { get; set; }
    }

    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, CommentListVm>
    {
        private readonly IBlogDbContext _context;
        private readonly IMapper _mapper;

        public GetCommentsQueryHandler(IMapper mapper, IBlogDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CommentListVm> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _context.Comments.Where(c => c.UserId == request.UserId)
                .ProjectTo<CommentListItemVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CommentListVm()
            {
                Comments = comments
            };
        }
    }

    public class CommentListVm
    {
        public List<CommentListItemVm> Comments { get; set; }
    }

    public class CommentListItemVm : IMapped<Comment>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int PostId { get; set; }
        public Guid UserId { get; set; }
    }
}