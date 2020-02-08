using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FileRepository : IFileRepository
    {
        public string MainDirectory { get; private set; }


        public FileRepository(string directoryName)
        {
            if (string.IsNullOrEmpty(directoryName) || string.IsNullOrWhiteSpace(directoryName))
            {
                throw new ArgumentException(nameof(directoryName));
            }
            MainDirectory = directoryName;
            if (!Directory.Exists(MainDirectory))
            {
                Directory.CreateDirectory(MainDirectory);
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
                    newPath = Path.Combine(MainDirectory, fileInfo.Name);
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
            DirectoryInfo directoryInfo = new DirectoryInfo(MainDirectory);
            if (directoryInfo.Exists)
            {
                var files = directoryInfo.GetFiles();
                foreach (var file in files)
                {
                    var filePath = Path.Combine(MainDirectory, file.Name);
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

        public bool ShowFiles(string path)
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
