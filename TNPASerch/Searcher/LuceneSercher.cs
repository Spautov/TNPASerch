using DAL;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextDocumentReaders;

namespace Searcher
{
    public class LuceneSercher : ISearcher, IDisposable
    {
        private readonly string _directoryName;
        private readonly Lucene.Net.Store.Directory _directory;
        private readonly Analyzer _analyzer;
        private readonly IndexWriter _writer;
        private readonly ITextDocumentReader _pdfReader;
        private readonly ITextDocumentReader _wordReader;
        private readonly ITextDocumentReader _txtReader;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="directoryName">Имя каталога где будет размещатся индекс</param>
        public LuceneSercher(string directoryName, 
            ITextDocumentReader pdfReader, 
            ITextDocumentReader wordReader, 
            ITextDocumentReader txtReader)
        {
            _directoryName = directoryName ?? throw new ArgumentNullException(nameof(directoryName));
            _pdfReader = pdfReader ?? throw new ArgumentNullException(nameof(pdfReader));
            _wordReader = wordReader ?? throw new ArgumentNullException(nameof(wordReader));
            _txtReader = txtReader ?? throw new ArgumentNullException(nameof(txtReader));

            _directory = GetDirectory();
            _analyzer = GetAnalyzer();
            _writer = new IndexWriter(_directory, _analyzer, IndexWriter.MaxFieldLength.UNLIMITED);
        }

        ////////////////////////////////////////////////////////
        public void Add(Tnpa tnpa)
        {
            var document = MapProduct(tnpa);
            _writer.AddDocument(document);
            _writer.Commit();
        }

        public void Remove(Tnpa tnpa)
        {
            throw new NotImplementedException();
        }

        public List<Tnpa> Serch()
        {
            throw new NotImplementedException();
        }

        ///////////////////////////////////////////////////////
        
        /// <summary>
        /// Метод возвращает хранилище индекса
        /// </summary>
        /// <returns></returns>
        private Lucene.Net.Store.Directory GetDirectory()
        {
            return new SimpleFSDirectory(new DirectoryInfo(_directoryName));

        }

        /// <summary>
        /// Метод возвращает анализатор
        /// </summary>
        /// <returns></returns>
        private Analyzer GetAnalyzer()
        {
            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            return analyzer;
        }

        /// <summary>
        /// Метод для преобразования объекта типа Tnpa в Document
        /// </summary>
        /// <param name="tnpa"></param>
        /// <returns></returns>
        private Document MapProduct(Tnpa tnpa)
        {
            var document = new Document();
            document.Add(new NumericField("Id", Field.Store.YES, true).SetIntValue(tnpa.Id));
            document.Add(new Field("Name", tnpa.Name, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Number", tnpa.Number, Field.Store.YES, Field.Index.ANALYZED));
            //to do
            return document;
        }

        public void Dispose()
        {
            if (_writer != null)
            {
                _writer.Dispose();
            }
            if (_analyzer != null)
            {
                _analyzer.Dispose();
            }
            if (_directory != null)
            {
                _directory.Dispose();
            }
        }
    }
}
