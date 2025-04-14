using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMM.Test.GUI.CmmWrappers
{
    public interface IFileSystemWrapper
    {
        bool DirectoryExists(string basePath);
        
        string[] GetDirectories(string basePath);
        
        bool FileExists(string fileName);
    }
}
