using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.Application.Common.Mappings;
using Blog.Application.Interfaces;
using Blog.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.Queries
{
    public class GetPostListQuery : IRequest<PostListVm>
    {
        public int UserId { get; set; }
    }

    public class GetPostListQueryHandler : IRequestHandler<GetPostListQuery, PostListVm>
    {
        private readonly IBlogDbContext _context;
        private readonly IMapper _mapper;

        public GetPostListQueryHandler(IBlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PostListVm> Handle(GetPostListQuery request, CancellationToken cancellationToken)
        {
            var posts = await _context.Posts
                .ProjectTo<PostListItemVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new PostListVm()
            {
                Posts = posts
            };
        }
    }

    public class PostListVm
    {
        public List<PostListItemVm> Posts { get; set; }
    }

    public class PostListItemVm : IMapFrom<Post>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}