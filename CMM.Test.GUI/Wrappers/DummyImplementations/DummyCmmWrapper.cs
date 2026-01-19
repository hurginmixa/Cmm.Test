using System;
using System.Collections.Generic;
using System.Windows;
using static System.Resources.ResXFileRef;

namespace CMM.Test.GUI.Wrappers.DummyImplementations
{
    public class DummyCmmWrapper : ICmmWrapper
    {
        public event Func<IEnumerable<ICmmFormatProperty>> OnGetCreateConvertersEvent;
        public event Func<IEnumerable<ICmmFormatProperty>> OnGetImportUpdateConvertersEvent;
        public event Func<string, string, bool> DoCreateEvent;
        public event Func<string, string, bool> DoImportEvent;
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
                    bool result = false;

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (Application.Current.MainWindow != null)
                        {
                            result = MessageBox.Show(Application.Current.MainWindow, $" Creating {converter} on result {resultPath}", "Creating", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
                        }
                    });

                    return result;
                })
                .WithOpenCreatingRtp(formatName =>
                {
                    if (Application.Current.MainWindow != null)
                    {
                        MessageBox.Show(Application.Current.MainWindow, $" Creating Rrp {formatName}");
                    }
                })
                .WithDoHaveCreatingRtpEvent(formatName =>
                {
                    switch (formatName)
                    {
                        case "Tdx":
                        case "Klarf":
                        {
                            return true;
                        }
                    }

                    return false;
                })
                .WithDoImport((converter, importPath) =>
                {
                    bool result = false;

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (Application.Current.MainWindow != null)
                        {
                            result = MessageBox.Show(Application.Current.MainWindow, $" Import {converter} on result {importPath}", "Import", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
                        }
                    });

                    return result;
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

        public bool DoImport(string converterName, string sourcePath)
        {
            if (DoImportEvent != null)
            {
                return DoImportEvent(converterName, sourcePath);
            }

            throw new Exception($"Property {nameof(DoImportEvent)} was not defined");
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

        public DummyCmmWrapper WithDoImport(Func<string, string, bool> action)
        {
            DoImportEvent += action;
            return this;
        }
    }
}
