using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Comments.Queries
{
    public class GetPostCommentsQuery : IRequest<CommentListVm>
    {
        public int PostId { get; set; }
    }

    public class GetPostCommentsQueryHandler : IRequestHandler<GetPostCommentsQuery, CommentListVm>
    {
        private readonly IBlogDbContext _context;
        private readonly IMapper _mapper;

        public GetPostCommentsQueryHandler(IBlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommentListVm> Handle(GetPostCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _context.Comments.Where(c => c.PostId == request.PostId)
                .ProjectTo<CommentListItemVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CommentListVm()
            {
                Comments = comments
            };
        }
    }
}