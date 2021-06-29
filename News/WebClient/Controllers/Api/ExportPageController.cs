namespace News.WebClient.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using News.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class ExportPageController : ControllerBase
    {
        private readonly IExportPageService _exportPageService;

        public ExportPageController(IExportPageService exportPageService)
        {
            this._exportPageService = exportPageService;
        }

        [HttpGet]
        public FileResult ExportPage(string format, string url)
        {
            if (format == "pdf")
            {
                var pdfFile = _exportPageService.ExportToPdf(url);

                return File(pdfFile, "application/octet-stream", "news.pdf");
            }
            else
            {
                var xlsFile = _exportPageService.ExportToXls(url);

                return File(xlsFile, "application/vnd.ms-excel", "news.xls");
            }
        }
    }
}
