using Microsoft.Office.Interop.Word;

namespace TextDocumentReaders
{
    public class WordDocumentReader : ITextDocumentReader
    {
        public string GetContent(object filename) 
        {
            var wordApp = new Application();
            var wordDoc = wordApp.Documents.Open(ref filename);

            string text = string.Empty;
            for (int i = 0; i < wordDoc.Paragraphs.Count; i++)
            {
                text += wordDoc.Paragraphs[i + 1].Range.Text;
            }
            wordDoc.Close();
            wordApp.Quit();
            return text;
        }
    }
}
