namespace News.Services
{
    using System.Threading.Tasks;

    public interface IInitialDataSeedService
    {
        Task SeedData();
    }
}
