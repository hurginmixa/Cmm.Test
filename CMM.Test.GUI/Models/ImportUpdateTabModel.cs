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
        }

        public RefProperty<string> ResultPath { get; set; }

        public CmmTestModel CmmTestModel { get; }
        
        public ICmmWrapper CmmWrapper { get; }
    }
}