using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Common.Exceptions;
using Blog.Application.Posts.Commands;
using Blog.Tests.Common;
using Xunit;

namespace Blog.Tests.Posts
{
    public class PostCommandsTests : TestWithDbBase
    {
        [Fact]
        public async Task ShouldCreatePost()
        {
            var handler = new CreatePostCommandHandler(_context);
            var command = new CreatePostCommand()
            {
                Content = "CreateTestContent",
                Title = "CreateTestTitle",
                UserId = BlogDbContextFactory.User1Id
            };
            var postId = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(_context.Posts.SingleOrDefault(p => p.Id == postId));
        }

        [Fact]
        public async Task ShouldUpdatePost()
        {
            var handler = new UpdatePostCommandHandler(_context);
            var command = new UpdatePostCommand()
            {
                Id = BlogDbContextFactory.PostIdToUpdate,
                Content = "UpdateContent",
                Title = "UpdateTitle",
                UserId = BlogDbContextFactory.User2Id
            };
            await handler.Handle(command, CancellationToken.None);
            var updatedPost = _context.Posts.SingleOrDefault(p => p.Id == BlogDbContextFactory.PostIdToUpdate);
            Assert.Equal("UpdateContent", updatedPost.Content);
            Assert.Equal("UpdateTitle", updatedPost.Title);
        }

        [Fact]
        public async Task UpdateShouldThrowOnWrongUserId()
        {
            var handler = new UpdatePostCommandHandler(_context);
            var command = new UpdatePostCommand()
            {
                Id = BlogDbContextFactory.PostIdToUpdate,
                Content = "UpdateContent",
                Title = "UpdateTitle",
                UserId = BlogDbContextFactory.User1Id
            };
            await Assert.ThrowsAnyAsync<ResourceNotFoundException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });
        }
        
        [Fact]
        public async Task ShouldDeletePost()
        {
            var handler = new DeletePostCommandHandler(_context);
            var command = new DeletePostCommand()
            {
                Id = BlogDbContextFactory.PostIdToDelete,
                UserId = BlogDbContextFactory.User1Id
            };
            await handler.Handle(command, CancellationToken.None);
            var updatedPost = _context.Posts.SingleOrDefault(p => p.Id == BlogDbContextFactory.PostIdToDelete);
            Assert.Null(updatedPost);
        }

        [Fact]
        public async Task DeleteShouldThrowOnWrongUserId()
        {
            var handler = new DeletePostCommandHandler(_context);
            var command = new DeletePostCommand()
            {
                Id = BlogDbContextFactory.PostIdToDelete,
                UserId = BlogDbContextFactory.User2Id
            };
            await Assert.ThrowsAnyAsync<ResourceNotFoundException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });
        }
    }
}