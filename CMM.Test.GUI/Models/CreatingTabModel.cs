using CMM.Test.GUI.Tools;

namespace CMM.Test.GUI.Models
{
    public class CreatingTabModel
    {
        public CreatingTabModel()
        {
            JobName = "Job Name1";

            SetupName = "Setup Name";
            
            Lot = "Lot Name";
            
            WaferId = "Wafer Id";

            CreateOnInternalBins = true;

            AssumeAutoCycle = false;

            AssumeVerification = true;

            NotShowMap = false;

            ImportAfterCreate = true;
        }

        public RefProperty<string> JobName { get; set; }
        
        public RefProperty<string> SetupName { get; set; }

        public RefProperty<string> Lot { get; set; }

        public RefProperty<string> WaferId { get; set; }

        public RefProperty<bool> CreateOnInternalBins { get; set; }

        public RefProperty<bool> AssumeAutoCycle { get; set; }
        
        public RefProperty<bool> AssumeVerification { get; set; }
        
        public RefProperty<bool> NotShowMap { get; set; }
        
        public RefProperty<bool> ImportAfterCreate { get; set; }
    }
}