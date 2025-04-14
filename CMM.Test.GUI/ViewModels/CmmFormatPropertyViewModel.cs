using CMM.Test.GUI.CmmWrappers;

namespace CMM.Test.GUI.ViewModels
{
    public class CmmFormatPropertyViewModel
    {
        public CmmFormatPropertyViewModel(ICmmFormatProperty cmmFormatProperty)
        {
            Name = cmmFormatProperty.Name;
            DisplayName = cmmFormatProperty.DisplayName;
        }

        public string Name { get; }

        public string DisplayName { get; }
    }
}