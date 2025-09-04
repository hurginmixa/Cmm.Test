using System.IO;
using System.Linq;
using CMM.Test.GUI.ViewModels;
using CMM.Test.GUI.Wrappers;
using CMM.Test.GUI.Wrappers.RealImplementations;
using NUnit.Framework;

namespace CMM.Test.GUI.UnitTests
{
    public class SelectFolderViewModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetJobListTest()
        {
            using (TemporaryFolder temporaryFolder = new TemporaryFolder())
            {
                string folderPath = temporaryFolder.FolderPath;

                IFileSystemWrapper fileSystem = new FileSystemWrapper();

                Assert.That(SelectFolderViewModel.GetJobList(folderPath, fileSystem).Any, Is.False);

                Directory.CreateDirectory(Path.Combine(folderPath, "JobFolder", "SetupFolder"));
                Assert.That(SelectFolderViewModel.GetJobList(folderPath, fileSystem).Any, Is.False);

                Directory.CreateDirectory(Path.Combine(folderPath, "JobFolder", "SetupFolder", "LotFolder"));
                Assert.That(SelectFolderViewModel.GetJobList(folderPath, fileSystem).Any, Is.False);

                Directory.CreateDirectory(Path.Combine(folderPath, "JobFolder", "SetupFolder", "LotFolder", "WaferFolder"));
                Assert.That(SelectFolderViewModel.GetJobList(folderPath, fileSystem).Any, Is.False);

                using (File.OpenWrite(Path.Combine(folderPath, "JobFolder", "SetupFolder", "LotFolder", "WaferFolder", "ScanLog.ini")))
                {
                }

                string[] jobList = SelectFolderViewModel.GetJobList(folderPath, fileSystem).ToArray();
                Assert.That(jobList.Any, Is.True);
                Assert.That(jobList[0], Is.EqualTo("JobFolder"));
            }
        }
    }
}