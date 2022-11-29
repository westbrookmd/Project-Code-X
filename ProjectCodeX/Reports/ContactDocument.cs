using ProjectCodeX.Models;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ProjectCodeX.Reports;

public class ContactDocument : IDocument
{
    public List<Contact> Model { get; }
    public ContactDocument(List<Contact> model)
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
                column.Item().Text($"Contact List").Style(titleStyle);

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
                columns.RelativeColumn();
            });

            // step 2
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("Name");
                header.Cell().Element(CellStyle).AlignRight().Text("Company");
                header.Cell().Element(CellStyle).AlignRight().Text("Address");
                header.Cell().Element(CellStyle).AlignRight().Text("City");
                header.Cell().Element(CellStyle).AlignRight().Text("State");
                header.Cell().Element(CellStyle).AlignRight().Text("Phone");
                header.Cell().Element(CellStyle).AlignRight().Text("Email");

                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                }
            });

            // step 3
            foreach (var item in Model)
            {
                table.Cell().Element(CellStyle).Text($"{item.Fname} {item.Lname}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Company}");
                table.Cell().Element(CellStyle).AlignRight().Text(item.Address);
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.City}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.State}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Phone}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Email}");

                static IContainer CellStyle(IContainer container)
                {
                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5).PaddingHorizontal(5);
                }
            }
        });
    }
}
