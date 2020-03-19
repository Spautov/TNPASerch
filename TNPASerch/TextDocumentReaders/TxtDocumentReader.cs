using System;
using System.IO;

namespace TextDocumentReaders
{
    public class TxtDocumentReader : ITextDocumentReader
    {
        public string GetContent(object filename)
        {
            string path = filename as string;
            string text = string.Empty;
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(path);
                text = reader.ReadToEnd();
            }
            catch(Exception)
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
