namespace CMM.Test.GUI.Models
{
    internal class CmmTestModel
    {
        public CmmTestModel()
        {
            CreatingTabModel = new CreatingTabModel();

            ImportUpdateTabModel = new ImportUpdateTabModel();
        }

        public CreatingTabModel CreatingTabModel { get; }

        public ImportUpdateTabModel ImportUpdateTabModel { get; }
    }
}