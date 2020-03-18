using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDocumentReaders
{
    public interface ITextDocumentReader
    {
        string GetContent(object filename);
    }
}
