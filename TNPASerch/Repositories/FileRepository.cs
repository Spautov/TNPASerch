using System;
using System.Diagnostics;
using System.IO;

namespace Repositories
{
    public class FileRepository : IFileRepository
    {
        public string MainFileRepositoryDirectory { get; private set; }


        public FileRepository(string directoryName)
        {
            if (string.IsNullOrEmpty(directoryName) || string.IsNullOrWhiteSpace(directoryName))
            {
                throw new ArgumentException(nameof(directoryName));
            }
            MainFileRepositoryDirectory = directoryName;
            if (!Directory.Exists(MainFileRepositoryDirectory))
            {
                Directory.CreateDirectory(MainFileRepositoryDirectory);
            }
        }


        public string AddFiles(string path)
        {
            string newPath = null;
            try
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    newPath = Path.Combine(MainFileRepositoryDirectory, fileInfo.Name);
                    File.Copy(path, newPath, true);
                }
                return newPath;
            }
            catch (Exception)
            {
                return newPath;
            }
        }

        public int CalculateHashCodeDirectory()
        {
            int resoult = 0;
            DirectoryInfo directoryInfo = new DirectoryInfo(MainFileRepositoryDirectory);
            if (directoryInfo.Exists)
            {
                var files = directoryInfo.GetFiles();
                foreach (var file in files)
                {
                    var filePath = Path.Combine(MainFileRepositoryDirectory, file.Name);
                    resoult += CalculateHashCodeFile(filePath);
                }

            }
            return resoult;
        }

        public int CalculateHashCodeFile(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            int resolt = 0;
            if (fileInfo.Exists)
            {
                resolt += path.GetHashCode();
                resolt += fileInfo.Length.GetHashCode();
            }
            return resolt;
        }

        public bool RemoveFile(string path)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool OpenFile(string path)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    Process.Start(fileInfo.FullName);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
