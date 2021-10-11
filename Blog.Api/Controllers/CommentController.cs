using System;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Api.Models.Comments;
using Blog.Application.Comments.Commands;
using Blog.Application.Comments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class CommentController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CommentController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<CommentListVm>> GetAllUserComments()
        {
            var query = new GetCommentsQuery() {UserId = UserId};
            var comments = await _mediator.Send(query);
            return Ok(comments);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CommentListVm>> GetCommentDetails(int id)
        {
            var query = new GetCommentDetailsQuery() {Id = id};
            var comment = await _mediator.Send(query);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto createCommentDto)
        {
            var command = _mapper.Map<CreateCommentCommand>(createCommentDto);
            command.UserId = UserId;
            var commentId = await _mediator.Send(command);
            return Created(Url.Action(nameof(GetCommentDetails), new {id = commentId}), commentId);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            var command = _mapper.Map<UpdateCommentCommand>(updateCommentDto);
            command.Id = id;
            command.UserId = UserId;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            await _mediator.Send(new DeleteCommentCommand() {Id = id, UserId = UserId});
            return NoContent();
        }
    }
}