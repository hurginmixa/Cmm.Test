using CMM.Test.GUI.Wrappers;

namespace CMM.Test.GUI.ViewModels
{
    public class CmmFormatPropertyViewModel
    {
        public static CmmFormatPropertyViewModel NewModel(ICmmFormatProperty cmmFormatProperty) => new CmmFormatPropertyViewModel(name: cmmFormatProperty.Name, displayName: cmmFormatProperty.DisplayName);

        public static CmmFormatPropertyViewModel SystemDefaultModel() => new CmmFormatPropertyViewModel(name: "", displayName: "System Default");

        private CmmFormatPropertyViewModel(string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
        }

        public string Name { get; }

        public string DisplayName { get; }
    }
}