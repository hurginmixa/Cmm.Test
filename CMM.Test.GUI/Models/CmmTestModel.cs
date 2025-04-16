using CMM.Test.GUI.CmmWrappers;
using Converters.Tools;
using System;
using System.IO;

namespace CMM.Test.GUI.Models
{
    public class CmmTestModel
    {
        public CmmTestModel(ICmmWrapper cmmWrapper)
        {
            CreatingTabModel = new CreatingTabModel(this, cmmWrapper);

            ImportUpdateTabModel = new ImportUpdateTabModel();
        }

        public string BaseResultsPath => @"\\mixa7th\c$\Falcon\ScanResults";

        public CreatingTabModel CreatingTabModel { get; }

        public ImportUpdateTabModel ImportUpdateTabModel { get; }
    }
}