﻿namespace Repositories
{
    public interface IFileRepository
    {
        string MainDirectory { get; }
        string AddFiles(string path);
        bool ShowFiles(string path);
        bool RemoveFile(string path);
        int CalculateHashCodeFile(string path);
        int CalculateHashCodeDirectory();
    }
}
