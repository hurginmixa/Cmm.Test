using Cmm.API;
using CMM.Test.GUI.Tools;

namespace CMM.Test.GUI.ViewModels
{
    /// <summary>
    /// View model for eImportWaferMapKind values with display names
    /// </summary>
    public class ImportWaferMapKindViewModel
    {
        /// <summary>
        /// Creates a new instance of ImportWaferMapKindViewModel
        /// </summary>
        /// <param name="kind">The import wafer map kind</param>
        /// <param name="displayName">The display name for the kind</param>
        public ImportWaferMapKindViewModel(eImportWaferMapKind kind, string displayName)
        {
            Kind = kind;
            DisplayName = displayName;
        }

        /// <summary>
        /// Gets the import wafer map kind
        /// </summary>
        public eImportWaferMapKind Kind { get; }

        /// <summary>
        /// Gets the display name for the import wafer map kind
        /// </summary>
        public string DisplayName { get; }
    }
}
