namespace News.Data.Repository
{
    using News.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class NewsRepository : INewsRepository
    {
        private readonly NewsDbContext dbContext;

        public NewsRepository(NewsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<NewsArticle> GetAll()
        {
            return this.dbContext.Articles.ToList();
        }

        public IEnumerable<NewsArticle> GetAll(string search)
        {
            return this.dbContext.Articles.Where(a => a.WebTitle.Contains(search)).ToList();
        }

        public void AddRange(IEnumerable<NewsArticle> newsArticles)
        {
            this.dbContext.Articles.AddRange(newsArticles);

            this.dbContext.SaveChanges();
        }
    }
}
