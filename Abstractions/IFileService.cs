using System;
using System.Collections.Generic;
using System.Text;

namespace kr.bbon.Xamarin.Forms.Abstractions
{
    public interface IFileService
    {
        string GetLocalFilePath(string fileName);

        string GetLocalDatabaseFilePath(string fileName);

        string SaveBlob(string fileName, byte[] blob);

        byte[] LoadBlob(string fileName);

        bool Delete(string fileName);

        bool Exists(string fileName);
    }
}
