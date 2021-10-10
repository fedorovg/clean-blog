using System;
using Blog.Persistence;

namespace Blog.Tests.Common
{
    public abstract class TestWithDbBase : IDisposable
    {
        protected readonly BlogDbContext _context;

        protected TestWithDbBase()
        {
            _context = BlogDbContextFactory.Create();
        }

        public void Dispose()
        {
            BlogDbContextFactory.Destroy(_context);
        }
    }
}