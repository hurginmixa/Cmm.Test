using System.Windows;
using CMM.Test.GUI.Wrappers.DummyImplementations;

namespace CMM.Test.GUI.Wrappers
{
    internal static class WrappersFabric 
    {
        public static IWrappers MakeWrappers() => new DummyWrapperImplementations();
    }
}
