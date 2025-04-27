using CMM.Test.GUI.CmmWrappers;
using CMM.Test.GUI.Tools;

namespace CMM.Test.GUI.Models
{
    public class ImportUpdateTabModel
    {
        public ImportUpdateTabModel(CmmTestModel cmmTestModel, ICmmWrapper cmmWrapper)
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

        public CmmTestModel CmmTestModel { get; }
        
        public ICmmWrapper CmmWrapper { get; }
    }
}
