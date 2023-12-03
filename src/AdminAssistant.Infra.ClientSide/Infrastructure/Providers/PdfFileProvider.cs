#pragma warning disable S125 // Sections of code should not be commented out
using PdfSharp.Fonts;
//using PdfSharp.Snippets.Font;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Content.Objects;
using PdfSharp.Pdf.Content;

namespace AdminAssistant.Infrastructure.Providers;

public interface IPdfFileProvider
{
    Task<IEnumerable<string>> ReadAllLinesAsync(byte[] fileContent);
}
public class PdfFileProvider : IPdfFileProvider
{
    //public PdfFileProvider() // see https://docs.pdfsharp.net/General/Overview/Port-to-v6.0.html
    //    => GlobalFontSettings.FontResolver = new FailsafeFontResolver();

    public Task<IEnumerable<string>> ReadAllLinesAsync(byte[] fileContent)
    {
        // https://docs.pdfsharp.net/General/Overview/About.html
        // https://github.com/empira/PDFsharp
        // https://github.com/empira/pdfsharp.Samples

        var result = new List<string>();

        //using var stream = new MemoryStream(fileContent);
        var pdfFilePath = Path.Combine(Directory.GetCurrentDirectory(), "_TestData", "ConfidentialTestData", "BankAccountStatement.pdf");

        using var pdfDocument = PdfReader.Open(pdfFilePath);

        foreach (var page in pdfDocument.Pages)
        {
            var content = page.ExtractText();
            result.AddRange(content);
        }
        return Task.FromResult<IEnumerable<string>>(result);
    }
}

public static class PdfSharpExtensions
{
    public static IEnumerable<string> ExtractText(this PdfPage page)
    {
        var content = ContentReader.ReadContent(page);
        var text = content.ExtractText();
        return text;
    }

    public static IEnumerable<string> ExtractText(this CObject cObject)
    {
        if (cObject is COperator cOperator)
        {
            if (cOperator.OpCode?.Name == OpCodeName.Tj.ToString() ||
                cOperator.OpCode?.Name == OpCodeName.TJ.ToString())
            {
                foreach (var cOperand in cOperator.Operands)
                {
                    foreach (var txt in ExtractText(cOperand))
                        yield return txt;
                }
            }
        }
        else if (cObject is CSequence cSequence)
        {
            foreach (var element in cSequence)
            {
                foreach (var txt in ExtractText(element))
                    yield return txt;
            }
        }
        else if (cObject is CString cString)
        {
            yield return cString.Value;
        }
    }
}
#pragma warning restore S125 // Sections of code should not be commented out
