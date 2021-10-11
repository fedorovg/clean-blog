using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Comments.Commands;
using Blog.Application.Common.Exceptions;
using Blog.Tests.Common;
using Xunit;

namespace Blog.Tests.Comments
{
    public class CommentCommandsTests : TestWithDbBase
    {
        [Fact]
        public async Task ShouldCreateComment()
        {
            var handler = new CreateCommentCommandHandler(_context);
            var command = new CreateCommentCommand()
            {
                Text = "CreateTestText",
                UserId = BlogDbContextFactory.User1Id
            };
            var commentId = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(_context.Comments.SingleOrDefault(c => c.Id == commentId));
        }

        [Fact]
        public async Task ShouldUpdateComment()
        {
            var handler = new UpdateCommentCommandHandler(_context);
            var command = new UpdateCommentCommand()
            {
                Id = 1,
                Text = "updated",
                UserId = BlogDbContextFactory.User2Id
            };
            await handler.Handle(command, CancellationToken.None);
            var updatedComment = _context.Comments.SingleOrDefault(c => c.Id == 1);
            Assert.Equal("updated", updatedComment.Text);
        }

        [Fact]
        public async Task UpdateShouldThrowOnWrongUserId()
        {
            var handler = new UpdateCommentCommandHandler(_context);
            var command = new UpdateCommentCommand()
            {
                Id = 1,
                Text = "updated",
                UserId = BlogDbContextFactory.User1Id
            };
            await Assert.ThrowsAnyAsync<ResourceNotFoundException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });
        }

        [Fact]
        public async Task ShouldDeleteComment()
        {
            var handler = new DeleteCommentCommandHandler(_context);
            var command = new DeleteCommentCommand()
            {
                Id = 1,
                UserId = BlogDbContextFactory.User2Id
            };
            await handler.Handle(command, CancellationToken.None);
            var deletedComment = _context.Comments.SingleOrDefault(c => c.Id == 1);
            Assert.Null(deletedComment);
        }

        [Fact]
        public async Task DeleteShouldThrowOnWrongUserId()
        {
            var handler = new DeleteCommentCommandHandler(_context);
            var command = new DeleteCommentCommand()
            {
                Id = 1,
                UserId = BlogDbContextFactory.User1Id
            };
            await Assert.ThrowsAnyAsync<ResourceNotFoundException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });
        }
    }
}