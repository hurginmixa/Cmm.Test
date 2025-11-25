using Converters.Tools;
using System;
using System.IO;
using CMM.Test.GUI.Wrappers;

namespace CMM.Test.GUI.Models
{
    public class CmmTestModel
    {
        public const string FalconScanResultsPath = @"\\mixa7th\c$\Falcon\ScanResults";

        public CmmTestModel(ICmmWrapper cmmWrapper)
        {
            CreatingTabModel = new CreatingTabModel(this, cmmWrapper);

            ImportUpdateTabModel = new ImportUpdateTabModel(this, cmmWrapper);
        }

        public CreatingTabModel CreatingTabModel { get; }

        public ImportUpdateTabModel ImportUpdateTabModel { get; }

        public string BaseResultsPath { get; } = FalconScanResultsPath;
    }
}