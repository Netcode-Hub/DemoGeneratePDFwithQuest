using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace DemoGeneratePDFwithQuest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PdfGeneratorController : ControllerBase
    {
        

        [HttpGet]
        public IActionResult GeneratePDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var document = CreateDocument();
            var pdf = document.GeneratePdf();
            return File(pdf, "application/pdf", "netcode-hub");
        }

        private static IDocument CreateDocument()
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header().Text("This is Page Header")
                    .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content().PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(1, Unit.Centimetre);
                        x.Item().Text(Placeholders.LoremIpsum());
                        x.Item().Image(Placeholders.Image(200, 100));
                        x.Item().Text(Placeholders.Question());
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
                });
            });
        }
    }
}
