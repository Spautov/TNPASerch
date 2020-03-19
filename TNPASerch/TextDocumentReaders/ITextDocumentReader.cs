namespace TextDocumentReaders
{
    public interface ITextDocumentReader
    {
        string GetContent(object filename);
    }
}
