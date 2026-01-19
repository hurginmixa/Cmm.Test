using System;
using CMM.Test.GUI.Wrappers.DummyImplementations;

namespace CMM.Test.GUI.Wrappers
{
    internal static class WrappersFabric 
    {
        public static IWrappers MakeWrappers()
        {
            if (!AppSettings.UseDummyWrappers)
            {
                throw new NotImplementedException("Real wrappers are not implemented yet.");
            }

            return new DummyWrapperImplementations();
        }
    }
}
