using System.Configuration;

namespace CMM.Test.GUI
{
    internal static class AppSettings
    {
        public static bool UseDummyWrappers => bool.TryParse(ConfigurationManager.AppSettings["UseDummyWrappers"], out var result) && result;
    }
}