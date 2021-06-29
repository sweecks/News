namespace News.Data.Repository
{
    using News.Models;
    using System.Collections.Generic;

    public interface INewsRepository
    {
        IEnumerable<NewsArticle> GetAll();

        IEnumerable<NewsArticle> GetAll(string search);

        void AddRange(IEnumerable<NewsArticle> newsArticles);
    }
}
