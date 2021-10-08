using System.Threading;
using System.Threading.Tasks;
using Blog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Interfaces
{
    public interface IBlogDbContext
    {
        DbSet< Post> Posts { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}