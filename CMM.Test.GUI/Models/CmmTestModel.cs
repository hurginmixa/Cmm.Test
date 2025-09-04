using Converters.Tools;
using System;
using System.IO;
using CMM.Test.GUI.Wrappers;

namespace CMM.Test.GUI.Models
{
    public class CmmTestModel
    {
        public CmmTestModel(ICmmWrapper cmmWrapper, IFileSystemWrapper fileSystemWrapper)
        {
            CreatingTabModel = new CreatingTabModel(this, cmmWrapper, fileSystemWrapper);

            ImportUpdateTabModel = new ImportUpdateTabModel(this, cmmWrapper, fileSystemWrapper);
        }

        public CreatingTabModel CreatingTabModel { get; }

        public ImportUpdateTabModel ImportUpdateTabModel { get; }
    }
}