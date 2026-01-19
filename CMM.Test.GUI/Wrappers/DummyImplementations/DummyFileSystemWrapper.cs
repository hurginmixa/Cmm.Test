using CMM.Test.GUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CMM.Test.GUI.Wrappers.DummyImplementations
{
    public class DummyFileSystemWrapper : IFileSystemWrapper
    {
        #region private class VirtualFileSystemNode

        // Вспомогательный класс для представления файлов и директорий
        private class VirtualFileSystemNode
        {
            public bool IsDirectory { get; set; }
            public string Name { get; set; }
            public string Parent { get; set; }
        }

        #endregion

        // Виртуальная файловая система
        private const string BasePath = @"\\mixa7th\c$\Falcon\ScanResults";

        private readonly Dictionary<string, VirtualFileSystemNode> _fileSystem;

        private DummyFileSystemWrapper()
        {
            _fileSystem = new Dictionary<string, VirtualFileSystemNode>(StringComparer.OrdinalIgnoreCase);
            
            // Создаем корневую директорию
            _fileSystem[BasePath] = new VirtualFileSystemNode { IsDirectory = true, Name = BasePath };
        }

        public string BaseResultsPath => BasePath;

        public bool DirectoryExists(string path)
        {
            path = NormalizePath(path);
            return _fileSystem.TryGetValue(path, out var node) && node.IsDirectory;
        }

        public string[] GetDirectories(string path)
        {
            path = NormalizePath(path);
            
            if (!DirectoryExists(path))
                return Array.Empty<string>();
                
            return _fileSystem.Values
                .Where(node => node.IsDirectory && 
                               node.Parent == path)
                .Select(node => node.Name)
                .ToArray();
        }

        public bool FileExists(string fileName)
        {
            fileName = NormalizePath(fileName);
            return _fileSystem.TryGetValue(fileName, out var node) && !node.IsDirectory;
        }

        // Методы для настройки виртуальной файловой системы
        private DummyFileSystemWrapper AddDirectory(string path)
        {
            path = NormalizePath(path);
            string parentPath = Path.GetDirectoryName(path);
            string dirName = Path.GetFileName(path);
            
            // Создаем родительские директории, если их нет
            EnsureParentDirectoriesExist(parentPath);
            
            _fileSystem[path] = new VirtualFileSystemNode 
            { 
                IsDirectory = true, 
                Name = path,
                Parent = parentPath
            };
            
            return this;
        }

        private DummyFileSystemWrapper AddFile(string filePath)
        {
            filePath = NormalizePath(filePath);
            string parentPath = Path.GetDirectoryName(filePath);
            string fileName = Path.GetFileName(filePath);
            
            // Создаем родительские директории, если их нет
            EnsureParentDirectoriesExist(parentPath);
            
            _fileSystem[filePath] = new VirtualFileSystemNode 
            { 
                IsDirectory = false, 
                Name = filePath,
                Parent = parentPath
            };
            
            return this;
        }

        private void EnsureParentDirectoriesExist(string path)
        {
            if (string.IsNullOrEmpty(path) || path == BasePath || _fileSystem.ContainsKey(path))
                return;
                
            string parentPath = Path.GetDirectoryName(path);
            EnsureParentDirectoriesExist(parentPath);
            
            _fileSystem[path] = new VirtualFileSystemNode 
            { 
                IsDirectory = true, 
                Name = path,
                Parent = parentPath
            };
        }

        private static string NormalizePath(string path)
        {
            return Path.GetFullPath(path).TrimEnd('\\', '/');
        }
        
        // Статический метод для создания тестовой файловой системы
        public static DummyFileSystemWrapper CreateTestFileSystem()
        {
            return new DummyFileSystemWrapper()
                .AddDirectory(Path.Combine(BasePath, "Job1"))
                .AddDirectory(Path.Combine(BasePath, "Job1", "Setup1"))
                .AddDirectory(Path.Combine(BasePath, "Job1", "Setup1", "Lot1"))
                .AddDirectory(Path.Combine(BasePath, "Job1", "Setup1", "Lot1", "Wafer1"))
                .AddFile(Path.Combine(BasePath, "Job1", "Setup1", "Lot1", "Wafer1", "ScanLog.ini"))
                .AddDirectory(Path.Combine(BasePath, "Job1", "Setup1", "Lot1", "Wafer2"))
                .AddFile(Path.Combine(BasePath, "Job1", "Setup1", "Lot1", "Wafer2", "ScanLog.ini"))
 
                .AddDirectory(Path.Combine(BasePath, "Job2"))
                .AddDirectory(Path.Combine(BasePath, "Job2", "Setup2"))
                .AddFile(Path.Combine(BasePath, "Job2", "Setup2", "config.txt"));
        }
    }
}