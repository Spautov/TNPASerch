using System.IO;

namespace Repositories
{
    public interface IFileRepository
    {
        string MainFileRepositoryDirectory { get; }
        string AddFile(string faleName);
        bool OpenFile(string faleName);
        bool RemoveFile(string faleName);
        int CalculateHashCodeFile(string faleName);
        int CalculateHashCodeDirectory();
        FileInfo GetFullPath(string faleName);
    }
}
