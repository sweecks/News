namespace News.Services
{
    using DinkToPdf;
    using DinkToPdf.Contracts;
    using System.Net;
    using System.Text;

    public class ExportPageService : IExportPageService
    {
        private readonly IConverter _converter;
        public ExportPageService(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] ExportToPdf(string url)
        {
            string html = GetHtmlStringFromUrl(url);

            GlobalSettings globalSettings = new GlobalSettings();
            globalSettings.ColorMode = ColorMode.Color;
            globalSettings.Orientation = Orientation.Portrait;
            globalSettings.PaperSize = PaperKind.A4;
            globalSettings.Margins = new MarginSettings { Top = 25, Bottom = 25 };
            ObjectSettings objectSettings = new ObjectSettings();
            objectSettings.PagesCount = true;
            objectSettings.HtmlContent = html;
            WebSettings webSettings = new WebSettings();
            webSettings.DefaultEncoding = "utf-8";
            HeaderSettings headerSettings = new HeaderSettings();
            headerSettings.FontSize = 15;
            headerSettings.FontName = "Ariel";
            headerSettings.Right = "Page [page] of [toPage]";
            headerSettings.Line = true;
            FooterSettings footerSettings = new FooterSettings();
            footerSettings.FontSize = 12;
            footerSettings.FontName = "Ariel";
            footerSettings.Line = true;
            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;

            HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };

            return _converter.Convert(htmlToPdfDocument);
        }

        public byte[] ExportToXls(string url)
        {
            var html = GetHtmlStringFromUrl(url);

            return Encoding.ASCII.GetBytes(html);
        }

        private string GetHtmlStringFromUrl(string url)
        {
            using WebClient webClient = new WebClient();
            string htmlString = webClient.DownloadString(url);

            return htmlString;
        }
    }
}
