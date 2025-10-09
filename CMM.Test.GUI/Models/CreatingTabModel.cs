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
        public CreatingTabModel(CmmTestModel cmmTestModel, ICmmWrapper cmmWrapper)
        {
            CmmTestModel = cmmTestModel;
            CmmWrapper = cmmWrapper;

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
            try
            {
                if (!CmmWrapper.DoCreate(ConverterName, Path.Combine(CmmTestModel.BaseResultsPath, JobName, SetupName, Lot, WaferId)))
                {

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void OpenRtp() => CmmWrapper.OpenCreatingRtp(ConverterName);     
    }
}
