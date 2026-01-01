using CMM.Test.GUI.Wrappers;

namespace CMM.Test.GUI.ViewModels
{
    public class CmmFormatPropertyViewModel
    {
        public static CmmFormatPropertyViewModel NewModel(ICmmFormatProperty cmmFormatProperty) => new CmmFormatPropertyViewModel(name: cmmFormatProperty.Name, displayName: cmmFormatProperty.DisplayName, doHaveCreatingRTP:cmmFormatProperty.DoHaseCreatingRtp);

        public static CmmFormatPropertyViewModel SystemDefaultModel() => new CmmFormatPropertyViewModel(name: "", displayName: "System Default", false);

        private CmmFormatPropertyViewModel(string name, string displayName, bool doHaveCreatingRTP)
        {
            Name = name;
            DisplayName = displayName;
            DoHaveCreatingRTP = doHaveCreatingRTP;
        }

        public string Name { get; }

        public string DisplayName { get; }

        public bool DoHaveCreatingRTP { get; }
    }
}