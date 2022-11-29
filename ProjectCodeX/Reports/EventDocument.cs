using ProjectCodeX.Models;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ProjectCodeX.Reports;

public class EventDocument : IDocument
{
    public List<Event> Model { get; }
    public EventDocument(List<Event> model)
    {
        Model = model;
    }
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container
        .Page(page =>
        {
            page.Margin(50);

            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeTable);


            page.Footer().AlignCenter().Text(x =>
            {
                x.CurrentPageNumber();
                x.Span(" / ");
                x.TotalPages();
            });
        });
    }
    void ComposeHeader(IContainer container)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text($"Event Calendar").Style(titleStyle);

                column.Item().Text(text =>
                {
                    text.Span("Issue date: ").SemiBold();
                    text.Span($"{DateTime.Now}");
                });
            });
        });
    }
    void ComposeTable(IContainer container)
    {
        container.Table(table =>
        {
            // step 1
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            // step 2
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("Name");
                header.Cell().Element(CellStyle).AlignRight().Text("Date");
                header.Cell().Element(CellStyle).AlignRight().Text("Location");
                header.Cell().Element(CellStyle).AlignRight().Text("Notes");

                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                }
            });

            // step 3
            foreach (var item in Model)
            {
                table.Cell().Element(CellStyle).Text($"{item.Name}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Date}");
                table.Cell().Element(CellStyle).AlignRight().Text(item.Location);
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Notes}");

                static IContainer CellStyle(IContainer container)
                {
                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5).PaddingHorizontal(5);
                }
            }
        });
    }
}
