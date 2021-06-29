namespace News.Services
{
    using News.Data;
    using News.Data.Repository;
    using News.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class InitialDataSeedService : IInitialDataSeedService
    {
        private readonly NewsDbContext dbContext;
        private readonly INewsRepository _newsRepository;
        private const string API_KEY = "a03a6d26-e429-4591-b5d1-887d80736fb7";

        public InitialDataSeedService(NewsDbContext dbContext, INewsRepository newsRepository)
        {
            this.dbContext = dbContext;
            this._newsRepository = newsRepository;
        }

        public async Task SeedData()
        {
            this.dbContext.Database.EnsureCreated();

            if (this._newsRepository.GetAll().Count() <= 0)
            {
                var newRaw = await GetDataFromAPI();

                this._newsRepository.AddRange(newRaw);
            }
        }

        private async Task<List<NewsArticle>> GetDataFromAPI()
        {
            List<NewsArticle> newsRaw = new List<NewsArticle>();

            using var httpClient = new HttpClient();

            for (int i = 1; i <= 10; i++)
            {
                using var response = await httpClient.GetAsync($"https://content.guardianapis.com/search?page={i}&api-key={API_KEY}");

                string apiResponse = await response.Content.ReadAsStringAsync();

                var parsedObject = JObject.Parse(apiResponse);

                var result = parsedObject["response"]["results"].ToString();

                newsRaw.AddRange(JsonConvert.DeserializeObject<List<NewsArticle>>(result));
            }

            return newsRaw;
        }
    }
}
