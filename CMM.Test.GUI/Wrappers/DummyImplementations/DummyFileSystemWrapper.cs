using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CMM.Test.GUI.Wrappers.DummyImplementations
{
    public class DummyFileSystemWrapper : IFileSystemWrapper
    {
        // Вспомогательный класс для представления файлов и директорий
        private class VirtualFileSystemNode
        {
            public bool IsDirectory { get; set; }
            public string Name { get; set; }
            public string Parent { get; set; }
        }

        // Виртуальная файловая система
        private readonly Dictionary<string, VirtualFileSystemNode> _fileSystem;
        private readonly string _basePath;

        private DummyFileSystemWrapper(string basePath)
        {
            _basePath = NormalizePath(basePath);
            _fileSystem = new Dictionary<string, VirtualFileSystemNode>(StringComparer.OrdinalIgnoreCase);
            
            // Создаем корневую директорию
            _fileSystem[_basePath] = new VirtualFileSystemNode { IsDirectory = true, Name = _basePath };
        }

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
        public DummyFileSystemWrapper AddDirectory(string path)
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

        public DummyFileSystemWrapper AddFile(string filePath)
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
            if (string.IsNullOrEmpty(path) || path == _basePath || _fileSystem.ContainsKey(path))
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

        private string NormalizePath(string path)
        {
            return Path.GetFullPath(path).TrimEnd('\\', '/');
        }
        
        // Статический метод для создания тестовой файловой системы
        public static DummyFileSystemWrapper CreateTestFileSystem()
        {
            string basePath = FalconScanResultsPath;

            return new DummyFileSystemWrapper(basePath)
                .AddDirectory(Path.Combine(basePath, "Job1"))
                .AddDirectory(Path.Combine(basePath, "Job1", "Setup1"))
                .AddDirectory(Path.Combine(basePath, "Job1", "Setup1", "Lot1"))
                .AddDirectory(Path.Combine(basePath, "Job1", "Setup1", "Lot1", "Wafer1"))
                .AddFile(Path.Combine(basePath, "Job1", "Setup1", "Lot1", "Wafer1", "ScanLog.ini"))
                .AddDirectory(Path.Combine(basePath, "Job1", "Setup1", "Lot1", "Wafer2"))
                .AddFile(Path.Combine(basePath, "Job1", "Setup1", "Lot1", "Wafer2", "ScanLog.ini"))
 
                .AddDirectory(Path.Combine(basePath, "Job2"))
                .AddDirectory(Path.Combine(basePath, "Job2", "Setup2"))
                .AddFile(Path.Combine(basePath, "Job2", "Setup2", "config.txt"));
        }
    }
}