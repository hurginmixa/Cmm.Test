using System;
using System.Collections.Generic;
using System.Linq;

namespace CMM.Test.GUI.CmmWrappers.DummyImplementations
{
    public class DummyCmmWrapper : ICmmWrapper
    {
        public event Func<IEnumerable<ICmmFormatProperty>> OnGetCreateConvertersEvent;
        public event Func<string, bool> DoCreateEvent; 

        public IEnumerable<ICmmFormatProperty> CreatingConverters
        {
            get
            {
                if (OnGetCreateConvertersEvent != null)
                {
                    return OnGetCreateConvertersEvent();
                }

                throw new Exception($"Property {nameof(CreatingConverters)} was not defined");
            }
        }

        public bool DoCreate(string converterName)
        {
            if (DoCreateEvent != null)
            {
                return DoCreateEvent(converterName);
            }

            throw new Exception($"Property {nameof(DoCreateEvent)} was not defined");
        }

        public DummyCmmWrapper WithGetCreateConverters(IEnumerable<ICmmFormatProperty> converters)
        {
            OnGetCreateConvertersEvent += () => converters;
            return this;
        }

        public DummyCmmWrapper WithDoCreateEvent(bool converters)
        {
            DoCreateEvent += (s) => converters;
            return this;
        }

        public static DummyCmmWrapper CreateTestCmmWrapper()
        {
            DummyCmmWrapper cmmWrapper = new DummyCmmWrapper();

            List<DummyCmmFormatProperty> formatProperties = new List<DummyCmmFormatProperty>
            {
                new DummyCmmFormatProperty().WithName("Klarf").WithDisplayName("Klarf Converter"),
                new DummyCmmFormatProperty().WithName("Sinf").WithDisplayName("Sinf Converter"),
                new DummyCmmFormatProperty().WithName("Sinf3D").WithDisplayName("Sinf3D Converter"),
                new DummyCmmFormatProperty().WithName("Tdx").WithDisplayName("Tdx Converter")
            };

            cmmWrapper.WithGetCreateConverters(formatProperties).WithDoCreateEvent(true);

            return cmmWrapper;
        }
    }
}
