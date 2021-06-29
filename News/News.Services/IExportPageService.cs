namespace News.Services
{
    public interface IExportPageService
    {
        public byte[] ExportToPdf(string url);

        public byte[] ExportToXls(string url);
    }
}
