using Microsoft.Office.Interop.Word;
using System;

namespace TextDocumentReaders
{
    public class WordDocumentReader : ITextDocumentReader
    {
        public string GetContent(object filename) 
        {
            string text = string.Empty;
            Application wordApp = null;
            Document wordDoc = null;
            try
            {
                wordApp = new Application();
                wordDoc = wordApp.Documents.Open(ref filename);

                for (int i = 0; i < wordDoc.Paragraphs.Count; i++)
                {
                    text += wordDoc.Paragraphs[i + 1].Range.Text;
                }
            }
            catch (Exception)
            {
                text = string.Empty;
            }
            finally
            {
                if (wordDoc != null)
                {
                    wordDoc.Close();
                }

                if (wordApp != null)
                {
                    wordApp.Quit();
                }
            }
            
            return text;
        }
    }
}
