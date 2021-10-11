using System;
using System.Data.Common;
using Blog.Domain;
using Blog.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Blog.Tests.Common
{
    public static class BlogDbContextFactory
    {
        public static Guid User1Id = Guid.NewGuid();
        public static Guid User2Id = Guid.NewGuid();

        public static int PostIdToDelete = 1;
        public static int PostIdToUpdate = 2;

        public static BlogDbContext Create()
        {
            var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseSqlite(CreateInMemoryDatabase())
                .Options;

            var context = new BlogDbContext(options);
            context.Database.EnsureCreated();
            context.Posts.AddRange(
                new Post()
                {
                    Title = "Title1",
                    Content = "Content1",
                    Created = DateTime.Now,
                    UserId = User1Id
                },
                new Post()
                {
                    Title = "Title2",
                    Content = "Content2",
                    Created = DateTime.Now,
                    UserId = User2Id
                },
                new Post()
                {
                    Title = "Title3",
                    Content = "Content3",
                    Created = DateTime.Now,
                    UserId = User1Id
                }
            );
            context.Comments.AddRange(
                new Comment()
                {
                    Text = "comment1",
                    Created = DateTime.Now,
                    PostId = 1,
                    UserId = User2Id
                },
                new Comment()
                {
                    Text = "comment2",
                    Created = DateTime.Now,
                    PostId = 1,
                    UserId = User2Id
                },
                new Comment()
                {
                    Text = "comment3",
                    Created = DateTime.Now,
                    PostId = 1,
                    UserId = User2Id
                }
            );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(BlogDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            return connection;
        }
    }
}