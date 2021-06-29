namespace News.Data
{
    using Microsoft.EntityFrameworkCore;
    using News.Models;

    public class NewsDbContext : DbContext
    {
        public NewsDbContext()
        {
        }

        public NewsDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<NewsArticle> Articles { get; set; }
    }
}
