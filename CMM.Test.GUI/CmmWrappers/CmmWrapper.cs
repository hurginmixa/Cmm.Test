using System.Collections.Generic;

namespace CMM.Test.GUI.CmmWrappers
{
    internal class CmmWrapper : ICmmWrapper
    {
        public IEnumerable<ICmmFormatProperty> CreatingConverters => throw new System.NotImplementedException();

        public bool DoCreate(string converterName) => throw new System.NotImplementedException();
        
        public void OpenCreatingRtp(string converterName) => throw new System.NotImplementedException();

        public void OpenCreatingRtp() => throw new System.NotImplementedException();
    }
}
