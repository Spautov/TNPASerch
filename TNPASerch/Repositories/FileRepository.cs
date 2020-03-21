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


        public string AddFile(string faleName)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(faleName);
                if (fileInfo.Exists)
                {
                    string newPath = Path.Combine(MainFileRepositoryDirectory, fileInfo.Name);
                    File.Copy(faleName, newPath, true);
                }
                return fileInfo.Name;
            }
            catch (Exception)
            {
                return null;
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

        public int CalculateHashCodeFile(string faleName)
        {
            FileInfo fileInfo = new FileInfo(faleName);
            int resolt = 0;
            if (fileInfo.Exists)
            {
                resolt += faleName.GetHashCode();
                resolt += fileInfo.Length.GetHashCode();
            }
            return resolt;
        }

        public bool RemoveFile(string faleName)
        {
            try
            {
                string pathWithDirectoty = Path.Combine(MainFileRepositoryDirectory, faleName);
                FileInfo fileInfo = new FileInfo(pathWithDirectoty);
                if (fileInfo.Exists)
                {
                    File.Delete(pathWithDirectoty);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool OpenFile(string faleName)
        {
            try
            {
                string pathWithDirectoty = Path.Combine(MainFileRepositoryDirectory, faleName);
                FileInfo fileInfo = new FileInfo(pathWithDirectoty);
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

        public string GetFullPath(string faleName)
        {
            string resoult = string.Empty;
            try
            {
                string pathWithDirectoty = Path.Combine(MainFileRepositoryDirectory, faleName);
                FileInfo fileInfo = new FileInfo(pathWithDirectoty);
                if (fileInfo.Exists)
                {
                    resoult = fileInfo.FullName;
                }
                return resoult;
            }
            catch (Exception)
            {
                return resoult;
            }
        }
    }
}
