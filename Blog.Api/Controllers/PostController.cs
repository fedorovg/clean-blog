using System;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Api.Models.Posts;
using Blog.Application.Comments.Queries;
using Blog.Application.Posts.Commands;
using Blog.Application.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class PostController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PostController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PostListVm>> GetAllPosts()
        {
            var query = new GetPostListQuery();
            var posts = await _mediator.Send(query);
            return Ok(posts);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PostDetailsVm>> GetPostDetails(int id)
        {
            var query = new GetPostDetailsQuery() {Id = id, UserId = UserId};
            var postDetails = await _mediator.Send(query);
            return Ok(postDetails);
        }

        [HttpGet("{id:int}/comments")]
        public async Task<ActionResult> GetPostComments(int id)
        {
            var query = new GetPostCommentsQuery()
            {
                PostId = id
            };
            var comments = await _mediator.Send(query);
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            var command = _mapper.Map<CreatePostDto, CreatePostCommand>(createPostDto);
            command.UserId = UserId;
            var createdPostId = await _mediator.Send(command);
            return Created(Url.Action(nameof(GetPostDetails), new {id = createdPostId}), createdPostId);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostDto updatePostDto)
        {
            var command = _mapper.Map<UpdatePostDto, UpdatePostCommand>(updatePostDto);
            command.Id = id;
            command.UserId = UserId;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var command = new DeletePostCommand() {Id = id, UserId = UserId};
            await _mediator.Send(command);
            return NoContent();
        }
    }
}