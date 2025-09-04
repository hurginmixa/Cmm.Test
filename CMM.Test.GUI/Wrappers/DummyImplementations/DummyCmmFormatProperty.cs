using System;

namespace CMM.Test.GUI.Wrappers.DummyImplementations
{
    public class DummyCmmFormatProperty : ICmmFormatProperty
    {
        public event Func<string> OnNameEvent;
        public event Func<string> OnDisplayNameEvent;

        string ICmmFormatProperty.Name 
        { 
            get 
            {
                if (OnNameEvent != null)
                {
                    return OnNameEvent();
                }

                throw new Exception("Property Name was not defined");
            }
        }

        string ICmmFormatProperty.DisplayName 
        { 
            get 
            {
                if (OnDisplayNameEvent != null)
                {
                    return OnDisplayNameEvent();
                }

                throw new Exception("Property DisplayName was not defined");
            }
        }

        public DummyCmmFormatProperty WithName(string value)
        {
            OnNameEvent += () => value;

            return this;
        }

        public DummyCmmFormatProperty WithDisplayName(string value)
        {
            OnDisplayNameEvent += () => value;

            return this;
        }
    }
}
