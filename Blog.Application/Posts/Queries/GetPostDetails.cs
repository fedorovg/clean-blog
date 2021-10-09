using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Mappings;
using Blog.Application.Interfaces;
using Blog.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.Queries
{
    public class GetPostDetailsQuery : IRequest<PostDetailsVm>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }

    public class GetPostDetailsQueryHandler : IRequestHandler<GetPostDetailsQuery, PostDetailsVm>
    {
        private readonly IBlogDbContext _context;
        private readonly IMapper _mapper;

        public GetPostDetailsQueryHandler(IBlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PostDetailsVm> Handle(GetPostDetailsQuery request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p =>
                p.Id == request.Id && p.UserId == request.UserId, cancellationToken);

            if (post is null)
                throw new ResourceNotFoundException(nameof(Post), request.Id);

            return _mapper.Map<PostDetailsVm>(post);
        }
    }

    public class PostDetailsVm : IMapped<Post>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int UserId { get; set; }
    }
}