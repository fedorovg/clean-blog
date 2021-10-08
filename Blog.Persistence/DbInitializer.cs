namespace Blog.Persistence
{
    public class DbInitializer
    {
        public static void Initializer(BlogDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}