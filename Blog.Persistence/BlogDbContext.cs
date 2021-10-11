using System.Collections.Immutable;
using Blog.Application.Interfaces;
using Blog.Domain;
using Blog.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Blog.Persistence
{
    public class BlogDbContext : DbContext, IBlogDbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}