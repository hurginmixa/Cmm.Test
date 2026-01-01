using System;
using System.Collections.Generic;
using System.Windows;

namespace CMM.Test.GUI.Wrappers.DummyImplementations
{
    public class DummyCmmWrapper : ICmmWrapper
    {
        public event Func<IEnumerable<ICmmFormatProperty>> OnGetCreateConvertersEvent;
        public event Func<IEnumerable<ICmmFormatProperty>> OnGetImportUpdateConvertersEvent;
        public event Func<string, string, bool> DoCreateEvent;
        public event Action<string> OpenCreatingRtpEvent;
        public event Func<string, bool> DoHaveCreatingRtpEvent;

        public static DummyCmmWrapper CreateTestCmmWrapper()
        {
            DummyCmmWrapper cmmWrapper = new DummyCmmWrapper();

            List<DummyCmmFormatProperty> formatProperties = new List<DummyCmmFormatProperty>
            {
                new DummyCmmFormatProperty().WithName("Klarf").WithDisplayName("Klarf Converter").WithDoHaveCreatingRtp(true),
                new DummyCmmFormatProperty().WithName("Sinf").WithDisplayName("Sinf Converter").WithDoHaveCreatingRtp(false),
                new DummyCmmFormatProperty().WithName("Sinf3D").WithDisplayName("Sinf3D Converter").WithDoHaveCreatingRtp(false),
                new DummyCmmFormatProperty().WithName("Tdx").WithDisplayName("Tdx Converter").WithDoHaveCreatingRtp(true),
                new DummyCmmFormatProperty().WithName("KlarfNew").WithDisplayName("Klarf New Converter").WithDoHaveCreatingRtp(false),
                new DummyCmmFormatProperty().WithName("IPO").WithDisplayName("IPO Converter").WithDoHaveCreatingRtp(false),
            };

            cmmWrapper
                .WithGetCreateConverters(formatProperties)
                .WithGetImportUpdateConverters(formatProperties)
                .WithDoCreate((converter, resultPath) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (Application.Current.MainWindow != null)
                        {
                            MessageBox.Show(Application.Current.MainWindow, $" Creating {converter} on result {resultPath}");
                        }
                    });

                    return true;
                })
                .WithOpenCreatingRtp(s =>
                {
                    if (Application.Current.MainWindow != null)
                    {
                        MessageBox.Show(Application.Current.MainWindow, $" Creating Rrp {s}");
                    }
                })
                .WithDoHaveCreatingRtpEvent(s =>
                {
                    switch (s)
                    {
                        case "Tdx":
                        case "Klarf":
                        {
                            return true;
                        }
                    }

                    return false;
                });

            return cmmWrapper;
        }

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

        public bool DoCreate(string converterName, string resultPath)
        {
            if (DoCreateEvent != null)
            {
                return DoCreateEvent(converterName, resultPath);
            }

            throw new Exception($"Property {nameof(DoCreateEvent)} was not defined");
        }

        public void OpenCreatingRtp(string converterName)
        {
            if (OpenCreatingRtpEvent != null)
            {
                OpenCreatingRtpEvent(converterName);
            }
            else
            {
                throw new Exception($"Method {nameof(OpenCreatingRtp)} was not defined");
            }
        }

        public IEnumerable<ICmmFormatProperty> ImportUpdateConverters
        {
            get
            {
                if (OnGetImportUpdateConvertersEvent != null)
                {
                    return OnGetImportUpdateConvertersEvent();
                }

                throw new Exception($"Property {nameof(OnGetImportUpdateConvertersEvent)} was not defined");
            }
        }

        public DummyCmmWrapper WithGetCreateConverters(IEnumerable<ICmmFormatProperty> converters)
        {
            OnGetCreateConvertersEvent += () => converters;
            return this;
        }

        public DummyCmmWrapper WithGetImportUpdateConverters(IEnumerable<ICmmFormatProperty> converters)
        {
            OnGetImportUpdateConvertersEvent += () => converters;
            return this;
        }

        public DummyCmmWrapper WithDoCreate(bool creatingResult)
        {
            DoCreateEvent += (s, r) => creatingResult;
            return this;
        }

        public DummyCmmWrapper WithDoCreate(Func<string, string, bool> action)
        {
            DoCreateEvent += action;
            return this;
        }

        public DummyCmmWrapper WithOpenCreatingRtp(Action<string> action)
        {
            OpenCreatingRtpEvent += action;
            return this;
        }

        public DummyCmmWrapper WithDoHaveCreatingRtpEvent(Func<string, bool> action)
        {
            DoHaveCreatingRtpEvent += action;
            return this;
        }
    }
}
