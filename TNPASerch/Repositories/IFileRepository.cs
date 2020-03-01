﻿namespace Repositories
{
    public interface IFileRepository
    {
        string MainFileRepositoryDirectory { get; }
        string AddFiles(string path);
        bool OpenFile(string path);
        bool RemoveFile(string path);
        int CalculateHashCodeFile(string path);
        int CalculateHashCodeDirectory();
    }
}