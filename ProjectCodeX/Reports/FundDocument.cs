using ProjectCodeX.Models;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ProjectCodeX.Reports;

public class FundDocument : IDocument
{
    public List<Purchase> Model { get; }
    public decimal TotalAmountSpent { get; }
    public int UniqueUsers { get; }
    public FundDocument(List<Purchase> model)
    {
        Model = model;
        decimal? total = model.Sum(d => d.Price).GetValueOrDefault();
        int? uniqueUsers = model.DistinctBy(d => d.UserId).Count();

        if (total is not null)
        {
            TotalAmountSpent = (decimal)total;
        }
        if (uniqueUsers is not null)
        {
            UniqueUsers = (int)uniqueUsers;
        }
    }
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container
        .Page(page =>
        {
            page.Margin(50);

            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);


            page.Footer().AlignCenter().Text(x =>
            {
                x.CurrentPageNumber();
                x.Span(" / ");
                x.TotalPages();
            });
        });
    }
    void ComposeContent(IContainer container)
    {
        container.PaddingVertical(40).Column(column =>
        {
            column.Spacing(5);

            column.Item().Element(ComposeTable);

            if (UniqueUsers > 0 || TotalAmountSpent > 0)
                column.Item().PaddingTop(25).Element(ComposeSummary);
        });
    }
    void ComposeHeader(IContainer container)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text($"Fund Management Report").Style(titleStyle);

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
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            // step 2
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("Purchase Id");
                header.Cell().Element(CellStyle).AlignRight().Text("Name");
                header.Cell().Element(CellStyle).AlignRight().Text("Quantity");
                header.Cell().Element(CellStyle).AlignRight().Text("Price");
                header.Cell().Element(CellStyle).AlignRight().Text("Total");
                header.Cell().Element(CellStyle).AlignRight().Text("Purchase Date");
                header.Cell().Element(CellStyle).AlignRight().Text("User");
                

                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                }
            });

            // step 3
            foreach (var item in Model)
            {
                table.Cell().Element(CellStyle).Text($"{item.PurchId}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.PurchName}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Qnty}");
                table.Cell().Element(CellStyle).AlignRight().Text(item.Price);
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Total}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.PurchDate}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.UserId.Substring(0, 8)}");
                

                static IContainer CellStyle(IContainer container)
                {
                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5).PaddingHorizontal(5);
                }
            }
        });
    }
    void ComposeSummary(IContainer container)
    {
        container.Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
        {
            column.Spacing(5);
            column.Item().Text("Summary").FontSize(14);
            column.Item().Text($"This fund management report has {UniqueUsers} unique users. The total amount spent is {TotalAmountSpent:C}");
        });
    }
}
