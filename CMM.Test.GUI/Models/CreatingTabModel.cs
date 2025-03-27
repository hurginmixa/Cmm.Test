namespace CMM.Test.GUI.Models
{
    public class CreatingTabModel
    {
        public CreatingTabModel()
        {
            JobName = "Job Name";

            SetupName = "Setup Name";
            
            Lot = "Lot Name";
            
            WaferId = "Wafer Id";
        }

        public string JobName { get; set; }
        
        public string SetupName { get; set; }

        public string Lot { get; set; }

        public string WaferId { get; set; }
    }
}