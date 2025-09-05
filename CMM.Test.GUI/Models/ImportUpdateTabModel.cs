using CMM.Test.GUI.Tools;
using CMM.Test.GUI.Wrappers;
using Cmm.API;

namespace CMM.Test.GUI.Models
{
    public class ImportUpdateTabModel
    {
        public ImportUpdateTabModel(CmmTestModel cmmTestModel, ICmmWrapper cmmWrapper, IFileSystemWrapper fileSystemWrapper)
        {
            CmmTestModel = cmmTestModel;
            CmmWrapper = cmmWrapper;
            ResultPath = "";
            UsingResultPath = false;
            LotId = "";
            WaferId = "";
            WaferMapMask = "";
            SubmapId = "";
            DataInPath = "";
            DataOutPath = "";
            InVerification = false;
            NotShowMap = false;
            SelectedConverterName = "";
            FileSystemWrapper = fileSystemWrapper;
            ImportKind = eImportWaferMapKind.ForEnginiring;
        }

        public RefProperty<string> ResultPath { get; set; }

        public RefProperty<bool> UsingResultPath { get; set; }

        public RefProperty<string> LotId { get; set; }

        public RefProperty<string> WaferId { get; set; }

        public RefProperty<string> WaferMapMask { get; set; }

        public RefProperty<string> SubmapId { get; set; }

        public RefProperty<string> DataInPath { get; set; }

        public RefProperty<string> DataOutPath { get; set; }

        public RefProperty<bool> InVerification { get; set; }

        public RefProperty<bool> NotShowMap { get; set; }

        public RefProperty<string> SelectedConverterName { get; set; }

        public RefProperty<eImportWaferMapKind> ImportKind { get; set; }

        public CmmTestModel CmmTestModel { get; }
        
        public ICmmWrapper CmmWrapper { get; }

        public IFileSystemWrapper FileSystemWrapper { get; }
    }
}
