using Cmm.API;

namespace CMM.Test.GUI.Tools
{
    /// <summary>
    /// Helper class for working with eImportWaferMapKind enum
    /// </summary>
    public static class ImportWaferMapKindHelper
    {
        /// <summary>
        /// Converts eImportWaferMapKind enum value to its display string representation
        /// </summary>
        /// <param name="kind">The eImportWaferMapKind value to convert</param>
        /// <returns>String representation for display</returns>
        public static string GeteImportWaferMapKindDisplayName(eImportWaferMapKind kind)
        {
            switch (kind)
            {
                case eImportWaferMapKind.ForEnginiring:
                    return "Engineering";
                case eImportWaferMapKind.BeforeScan:
                    return "Before Scan";
                case eImportWaferMapKind.ForUpdate:
                    return "For Update";
                case eImportWaferMapKind.ForMapMatchBeforeScan:
                    return "Map Match Before Scan";
                case eImportWaferMapKind.ForMapMatchAfterScan:
                    return "Map Match After Scan";
                case eImportWaferMapKind.ForImportReferenceMapAfterScan:
                    return "Import Reference Map After Scan";
                case eImportWaferMapKind.ForMakeReferenceMap:
                    return "Make Reference Map";
                case eImportWaferMapKind.ForSavingMapMatchInformation:
                    return "Saving Map Match Information";
                default:
                    return kind.ToString();
            }
        }
    }
}
