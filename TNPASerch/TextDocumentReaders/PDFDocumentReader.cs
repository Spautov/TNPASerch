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
            PdfReader reader = null;
            try
            {
                reader = new PdfReader((string)filename);
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy its = new SimpleTextExtractionStrategy();
                    string pdfString = PdfTextExtractor.GetTextFromPage(reader, page, its);
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
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
            return text;
        }
    }
}
