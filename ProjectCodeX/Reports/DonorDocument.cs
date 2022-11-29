using ProjectCodeX.Models;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ProjectCodeX.Reports;

public class DonorDocument : IDocument
{
    public List<Donation> Model { get; }
    public decimal TotalAmountDonated { get; }
    public int UniqueDonors { get; }

    public DonorDocument(List<Donation> model)
    {
        Model = model;
        decimal? total = model.Sum(d => d.Amount).GetValueOrDefault();
        int? uniqueDonors = model.DistinctBy(d => d.UserId).Count();
        if (total is not null)
        {
            TotalAmountDonated = (decimal)total;
        }
        if (uniqueDonors is not null)
        {
            UniqueDonors = (int)uniqueDonors;
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

            if (UniqueDonors > 0 || TotalAmountDonated > 0)
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
                column.Item().Text($"Donor List").Style(titleStyle);

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
            });

            // step 2
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("Donation Id");
                header.Cell().Element(CellStyle).Text("User Id");
                header.Cell().Element(CellStyle).AlignRight().Text("Amount");
                header.Cell().Element(CellStyle).AlignRight().Text("Donation Date");
                header.Cell().Element(CellStyle).AlignRight().Text("Notes");

                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                }
            });

            // step 3
            foreach (var item in Model)
            {
                table.Cell().Element(CellStyle).Text(item.DonationId);
                table.Cell().Element(CellStyle).Text($"{item.UserId.Substring(0, 8)}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Amount:C}");
                table.Cell().Element(CellStyle).AlignRight().Text(item.DonationDate);
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Notes}");

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
            column.Item().Text($"This donor list has {UniqueDonors} unique donors. The total amount donated is {TotalAmountDonated:C}");
        });
    }
}
