using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Text;

namespace TextDocumentReaders
{
    public class PDFDocumentReader : ITextDocumentReader
    {
        public string GetContent(object filename)
        {
            string text = string.Empty;
            PdfReader readerOut = null;
            PdfReader readerIn = null;
            try
            {
                readerOut = new PdfReader((string)filename);
                for (int page = 1; page <= readerOut.NumberOfPages; page++)
                {
                    ITextExtractionStrategy its = new SimpleTextExtractionStrategy();
                    readerIn = new PdfReader((string)filename);
                    string pdfString = PdfTextExtractor.GetTextFromPage(readerIn, page, its);
                    pdfString = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default,
                        Encoding.UTF8, Encoding.Default.GetBytes(pdfString)));
                    text += pdfString;
                }
            }
            catch (Exception)
            {
                text = string.Empty;
            }
            finally
            {
                if (readerIn != null)
                {
                    readerIn.Close();
                }
                if (readerOut != null)
                {
                    readerOut.Close();
                    readerOut.Dispose();
                }
            }

            return text;
        }
    }
}
