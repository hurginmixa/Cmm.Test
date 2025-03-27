using System.IO;
using System.Linq;
using CMM.Test.GUI.ViewModels;
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

                Assert.That(SelectFolderViewModel.GetJobList(folderPath).Any, Is.False);

                Directory.CreateDirectory(Path.Combine(folderPath, @"JobFolder\SetupFolder"));
                Assert.That(SelectFolderViewModel.GetJobList(folderPath).Any, Is.False);

                Directory.CreateDirectory(Path.Combine(folderPath, @"JobFolder\SetupFolder\LotFolder"));
                Assert.That(SelectFolderViewModel.GetJobList(folderPath).Any, Is.False);

                Directory.CreateDirectory(Path.Combine(folderPath, @"JobFolder\SetupFolder\LotFolder\WaferFolder"));
                string[] jobList = SelectFolderViewModel.GetJobList(folderPath).ToArray();
                Assert.That(jobList.Any, Is.True);
                Assert.That(jobList[0], Is.EqualTo("JobFolder"));
            }
        }
    }
}