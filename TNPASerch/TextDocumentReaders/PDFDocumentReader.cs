using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;

namespace TextDocumentReaders
{
    public class PDFDocumentReader : ITextDocumentReader
    {
        public string GetContent(object filename)
        {
            PdfReader readerOut = new PdfReader((string)filename);
            string text = string.Empty;

            for (int page = 1; page <= readerOut.NumberOfPages; page++)
            {
                ITextExtractionStrategy its = new SimpleTextExtractionStrategy();
                PdfReader readerIn = new PdfReader((string)filename);
                string pdfString = PdfTextExtractor.GetTextFromPage(readerIn, page, its);

                pdfString = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, 
                    Encoding.UTF8, Encoding.Default.GetBytes(pdfString)));
                text += pdfString;
                readerIn.Close();
            }
            readerOut.Close();
            return text;
        }
    }
}
