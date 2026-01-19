using System;
using System.IO;
using System.Windows;
using Cmm.API;
using CMM.Test.GUI.Tools;
using CMM.Test.GUI.Wrappers;

namespace CMM.Test.GUI.Models
{
    public class CreatingTabModel
    {
        public CreatingTabModel(CmmTestModel cmmTestModel, IWrappers wrappers)
        {
            CmmTestModel = cmmTestModel;
            Wrappers = wrappers;
            CmmWrapper = wrappers.GetCmmWrapper();

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

        private CmmTestModel CmmTestModel { get; }
        public IWrappers Wrappers { get; }

        public ICmmWrapper CmmWrapper { get; }

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

        public void DoCreate()
        {
            if (!CmmWrapper.DoCreate(ConverterName, Path.Combine(Wrappers.GetFileSystemWrapper().BaseResultsPath, JobName, SetupName, Lot, WaferId)))
            {

            }
        }

        public void OpenRtp() => CmmWrapper.OpenCreatingRtp(ConverterName);     
    }
}
