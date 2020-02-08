namespace Repositories
{
    public interface IFileRepository
    {
        bool AddFiles(string path);
        void ShowFiles(string path);
        bool RemoveFiles(string path);
        int CalculateHashCodeFile(string path);
        int CalculateHashCodeDirectory(string path);
    }
}
