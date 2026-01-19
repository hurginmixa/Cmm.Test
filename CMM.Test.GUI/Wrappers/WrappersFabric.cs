using CMM.Test.GUI.Wrappers.DummyImplementations;
using CMM.Test.GUI.Wrappers.RealImplementations;

namespace CMM.Test.GUI.Wrappers
{
    internal static class WrappersFabric 
    {
        public static IWrappers MakeWrappers()
        {
            return !AppSettings.UseDummyWrappers 
                ? (IWrappers) new RealWrapperImplementation() 
                : new DummyWrapperImplementations();
        }
    }
}
