namespace WebClient.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using News.Data.Repository;
    using News.Models;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using WebClient.Models;
    using X.PagedList;

    public class HomeController : Controller
    {
        private readonly INewsRepository _newsRepository;

        public HomeController(INewsRepository newsRepository)
        {
            this._newsRepository = newsRepository;
        }

        public IActionResult Index(string search, int? page, string orderBy)
        {
            IEnumerable<NewsArticle> news;

            var pageNumber = page ?? 1;
            int pageSize = 10;

            if (!string.IsNullOrEmpty(search))
            {
                news = this._newsRepository.GetAll(search);
            }
            else
            {
                news = this._newsRepository.GetAll();
            }

            switch (orderBy)
            {
                case "Title":
                    news = news.OrderBy(n => n.WebTitle);
                    break;
                case "Title Descending":
                    news = news.OrderByDescending(n => n.WebTitle);
                    break;
                case "Date":
                    news = news.OrderBy(n => DateTime.Parse(n.WebPublicationDate));
                    break;
                case "Date Descending":
                    news = news.OrderByDescending(n => DateTime.Parse(n.WebPublicationDate));
                    break;
                default:
                    news = news.OrderBy(n => n.Id);
                    break;
            }

            return View(news.ToPagedList(pageNumber, pageSize));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
