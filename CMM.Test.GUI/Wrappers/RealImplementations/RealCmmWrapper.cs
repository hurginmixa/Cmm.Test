using System.Collections.Generic;

namespace CMM.Test.GUI.Wrappers.RealImplementations
{
    internal class RealCmmWrapper : ICmmWrapper
    {
        public IEnumerable<ICmmFormatProperty> CreatingConverters => throw new System.NotImplementedException();

        public bool DoCreate(string converterName, string resultPath) => throw new System.NotImplementedException();
        
        public void OpenCreatingRtp(string converterName) => throw new System.NotImplementedException();
        
        public bool DoImport(string converterName, string sourcePath) => throw new System.NotImplementedException();

        public IEnumerable<ICmmFormatProperty> ImportUpdateConverters => throw new System.NotImplementedException();
    }
}
