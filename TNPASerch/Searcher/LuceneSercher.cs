using DAL;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Repositories;
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
        private readonly IFileRepository _fileRepository;
        private readonly IRepository _repository;
        private readonly Lucene.Net.Store.Directory _directory;
        private readonly Analyzer _analyzer;
        private readonly IndexWriter _writer;
        /// <summary>
        /// Словарь для хранения расширений файлов и соответстующих им ITextDocumentReader
        /// </summary>
        private readonly Dictionary<string, ITextDocumentReader> DocumentReaders;
        private readonly int _limit = 50;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="directoryName">Имя каталога где будет размещатся индекс</param>
        public LuceneSercher(string directoryName,
            ITextDocumentReader pdfReader,
            ITextDocumentReader wordReader,
            ITextDocumentReader txtReader,
            IFileRepository fileRepository,
            IRepository repository)
        {
            _directoryName = directoryName ?? throw new ArgumentNullException(nameof(directoryName));
            _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            if (pdfReader == null)
            {
                throw new ArgumentNullException(nameof(pdfReader));
            }

            if (wordReader == null)
            {
                throw new ArgumentNullException(nameof(wordReader));
            }

            if (txtReader == null)
            {
                throw new ArgumentNullException(nameof(txtReader));
            }

            DocumentReaders = new Dictionary<string, ITextDocumentReader>
            {
                {"PDF", pdfReader },
                {"DOC", wordReader },
                {"DOCX", wordReader },
                {"RTF", wordReader },
                {"TXT", txtReader }
            };

            _directory = GetDirectory();
            _analyzer = GetAnalyzer();
            _writer = new IndexWriter(_directory, _analyzer, IndexWriter.MaxFieldLength.UNLIMITED);
        }

        /// <summary>
        /// Получить ITextDocumentReader который соответсвует конкретному расширению файла
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        private ITextDocumentReader GetDocumentReader(string extension)
        {
            if (!string.IsNullOrEmpty(extension) || !string.IsNullOrWhiteSpace(extension))
            {
                return null;
            }
            var extUpper = extension.ToUpper();
            if (DocumentReaders.ContainsKey(extUpper))
            {
                return DocumentReaders[extUpper];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Получить содержимое конкретного файла
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        private string GetContent(FileInfo fileInfo)
        {
            string resoult = string.Empty;
            if (fileInfo != null)
            {
                var reader = GetDocumentReader(fileInfo.Extension);
                if (reader != null)
                {
                    try
                    {
                        resoult = reader.GetContent(fileInfo.FullName);
                    }
                    catch (Exception)
                    {
                        resoult = string.Empty;
                    }
                }
            }
            return resoult;
        }

        /// <summary>
        /// Получить весь контент файлов прикрепленных к ТНПА
        /// </summary>
        /// <param name="tnpa"></param>
        /// <returns></returns>
        private List<string> GetAllContent(Tnpa tnpa)
        {
            List<string> resoult = new List<string>();
            foreach (var file in tnpa.Files)
            {
                var fileInfo = _fileRepository.GetFullPath(file.Path);
                var content = GetContent(fileInfo);
                if (!string.IsNullOrEmpty(content))
                {
                    resoult.Add(content);
                }
            }
            return resoult;
        }

        /// <summary>
        /// Добавить ТНПА в индекс поисика
        /// </summary>
        /// <param name="tnpa"></param>
        public void Add(Tnpa tnpa)
        {
            var document = MapProduct(tnpa);
            _writer.AddDocument(document);
            _writer.Commit();
        }

        /// <summary>
        /// Удалить ТНПА из индекса поиска
        /// </summary>
        /// <param name="tnpa"></param>
        public void Remove(Tnpa tnpa)
        {
            _writer.DeleteDocuments(GetQueryId(tnpa.Id.ToString()));
        }

        /// <summary>
        /// Получить список ТНПА по запросу
        /// </summary>
        /// <param name="request">запрос поиска</param>
        /// <returns></returns>
        public List<Tnpa> Serch(string request)
        {
            var listIdByNumber = SerchNumber(request);
            var listIdByName = SerchName(request);
            var listIdByContent = SerchContent(request);
            foreach (var tnpaId in listIdByName)
            {
                if (!listIdByNumber.Contains(tnpaId))
                {
                    listIdByNumber.Add(tnpaId);
                }
            }

            foreach (var tnpaId in listIdByContent)
            {
                if (!listIdByNumber.Contains(tnpaId))
                {
                    listIdByNumber.Add(tnpaId);
                }
            }
            var resoult = new List<Tnpa>();

            foreach (var tnpaId in listIdByNumber)
            {
                try
                {
                    var tnpa = _repository.GetTnpa(tnpaId);
                    resoult.Add(tnpa);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return resoult;
        }

        private List<int> SerchNumber(string request)
        {
            var req = request.Trim(' ').Replace(" ", "") + "*";
            return SerchQuery(GetQueryNumber(req), _limit);
        }

        private List<int> SerchName(string request)
        {
            return SerchQuery(GetQueryName(request), _limit);
        }

        private List<int> SerchContent(string request)
        {
            return SerchQuery(GetQueryContent(request),10);
        }

        private List<int> SerchQuery(Query query, int limit)
        {
            using (var directory = GetDirectory())
            using (var searcher = new IndexSearcher(directory))
            {
                var docs = searcher.Search(query, limit);
                var count = docs.TotalHits;

                var products = new List<int>();
                foreach (var scoreDoc in docs.ScoreDocs)
                {
                    var doc = searcher.Doc(scoreDoc.Doc);
                    products.Add(int.Parse(doc.Get("Id")));
                }
                return products;
            }
        }


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
            document.Add(new Field("Id", tnpa.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("Name", tnpa.Name, Field.Store.NO, Field.Index.ANALYZED));
            document.Add(new Field("Number", $"{tnpa.Number}-{tnpa.Year}", Field.Store.NO, Field.Index.NOT_ANALYZED));
            string allCont = "";
            if (tnpa.Files.Count > 0)
            {
                var listContent = GetAllContent(tnpa);
                if (listContent.Count > 0)
                {
                    foreach (var cont in listContent)
                    {
                        allCont += cont;
                    }
                }
            }
            document.Add(new Field("Content", allCont, Field.Store.NO, Field.Index.ANALYZED));
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

        /// <summary>
        /// Запрос на получение документа с конкрентым ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Query GetQueryId(string id)
        {
            using (var analyzer = GetAnalyzer())
            {
                var parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "Id", analyzer);

                var query = new BooleanQuery();
                if (id != null)
                {
                    var keywordsQuery = parser.Parse(id);
                    query.Add(keywordsQuery, Occur.MUST);
                }

                return query;
            }
        }

        private Query GetQueryNumber(string requestNumber)
        {
            using (var analyzer = GetAnalyzer())
            {
                var parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "Number", analyzer);

                var query = new BooleanQuery();
                if (requestNumber != null)
                {
                    var keywordsQuery = parser.Parse(requestNumber);
                    query.Add(keywordsQuery, Occur.SHOULD);
                }

                return query;
            }
        }

        private Query GetQueryName(string requestName)
        {
            return GetPhraseQuery("Name", requestName);
        }

        private Query GetQueryContent(string requestContent)
        {
            return GetPhraseQuery("Content", requestContent);
        }


        private Query GetPhraseQuery(string field, string keywords)
        {
            using (var analyzer = GetAnalyzer())
            {
                var query = new BooleanQuery();

                var str = keywords.Trim(' ').Split(' ');
                var phraseQuery = new PhraseQuery();

                foreach (var item in str)
                {
                    phraseQuery.Add(new Term(field, item));
                }

                query.Add(phraseQuery, Occur.SHOULD);
                return query;
            }
        }

    }
}
