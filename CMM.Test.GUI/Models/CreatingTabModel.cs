using Cmm.API;
using CMM.Test.GUI.Tools;
using CMM.Test.GUI.Wrappers;

namespace CMM.Test.GUI.Models
{
    public class CreatingTabModel
    {
        public CreatingTabModel(CmmTestModel cmmTestModel, ICmmWrapper cmmWrapper, IFileSystemWrapper fileSystemWrapper)
        {
            CmmTestModel = cmmTestModel;
            CmmWrapper = cmmWrapper;
            FileSystemWrapper = fileSystemWrapper;

            JobName = "";
            SetupName = "";
            Lot = "";
            WaferId = "";

            ConverterName = "";

            CreateOnInternalBins = true;
            AssumeAutoCycle = false;
            AssumeVerification = true;
            NotShowMap = false;
            ImportAfterCreate = true;
            ExportFlatPosition = eExportFlatPosition.Botom;
        }

        public CmmTestModel CmmTestModel { get; }

        public ICmmWrapper CmmWrapper { get; }

        public IFileSystemWrapper FileSystemWrapper { get; }

        public RefProperty<string> ConverterName { get; set; }

        public RefProperty<string> JobName { get; set; }
        
        public RefProperty<string> SetupName { get; set; }

        public RefProperty<string> Lot { get; set; }

        public RefProperty<string> WaferId { get; set; }

        public RefProperty<bool> CreateOnInternalBins { get; set; }

        public RefProperty<bool> AssumeAutoCycle { get; set; }
        
        public RefProperty<bool> AssumeVerification { get; set; }
        
        public RefProperty<bool> NotShowMap { get; set; }
        
        public RefProperty<bool> ImportAfterCreate { get; set; }

        public RefProperty<eExportFlatPosition> ExportFlatPosition { get; set; }

        public void DoCreate() => CmmWrapper.DoCreate(ConverterName);

        public void OpenRtp() => CmmWrapper.OpenCreatingRtp(ConverterName);     
    }
}
