using System.Collections.Generic;

namespace CMM.Test.GUI.Wrappers
{
    public interface ICmmWrapper
    {
        IEnumerable<ICmmFormatProperty> CreatingConverters { get; }

        IEnumerable<ICmmFormatProperty> ImportUpdateConverters { get; }
        
        bool DoCreate(string converterName);
        
        void OpenCreatingRtp(string converterName);
    }
}
