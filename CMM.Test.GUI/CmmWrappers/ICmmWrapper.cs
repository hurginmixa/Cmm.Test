using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMM.Test.GUI.CmmWrappers
{
    public interface ICmmWrapper
    {
        IEnumerable<ICmmFormatProperty> CreatingConverters { get; }
        
        bool DoCreate(string converterName);
        
        void OpenCreatingRtp(string converterName);
    }
}
