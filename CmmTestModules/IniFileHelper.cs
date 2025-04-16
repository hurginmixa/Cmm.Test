using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Converters.Tools
{
    public struct IniFileValue
    {
        public IniFileValue(string section, string key, string value)
        {
            Section = section;
            Key = key;
            Value = value;
        }

        public readonly string Section;
        public readonly string Key;
        public readonly string Value;
    }

    public static class IniFileHelper
    {
        #region Dll's Imports

        [DllImport("kernel32.dll")]
        public static extern void OutputDebugString(string lpOutputString);

        [DllImport("kernel32", SetLastError = false)]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault,
                                                          StringBuilder lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll")]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString,
                                                             string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
        private static extern int GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, int nSize, string lpFileName);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, int nSize,
                                                           string lpFileName);

        [DllImport("kernel32.dll")]
        public static extern bool WritePrivateProfileSection(string lpAppName, string sectionText, string lpFileName);

        #endregion

        public static int GetIni(string sectionName, string variableName, string fileName, int defalutValue)
        {
            CheckIni(sectionName, variableName, fileName, ref defalutValue);

            return defalutValue;
        }
        
        public static bool CheckIni(string sectionName, string variableName, string fileName, ref int value)
        {
            try
            {
                string valueString = value.ToString();
                if(!CheckIni(sectionName, variableName, fileName, ref valueString))
                {
                    return false;
                }

                if (!int.TryParse(valueString, out value))
                {
                    throw new Exception("The '" + valueString ?? string.Empty + "' is not valid int value");
                }

                return true;
            }
            catch(Exception e)
            {
                throw new Exception(string.Format("Exception at {0}:[{1}]{2}", fileName, sectionName, variableName), e);
            }
        }

        public static double GetIni(string sectionName, string variableName, string fileName, double defalutValue)
        {
            CheckIni(sectionName, variableName, fileName, ref defalutValue);

            return defalutValue;
        }

        public static bool CheckIni(string sectionName, string variableName, string fileName, ref double value)
        {
            try
            {
                string valueString = value.ToString();
                if (!CheckIni(sectionName, variableName, fileName, ref valueString))
                {
                    return false;
                }

                if (!double.TryParse(valueString, out value))
                {
                    throw new Exception("The '" + valueString ?? string.Empty + "' is not valid int value");
                }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Exception at {0}:[{1}]{2}", fileName, sectionName, variableName), e);
            }
        }

        public static bool GetIni(string sectionName, string variableName, string fileName, bool defalutValue)
        {
            CheckIni(sectionName, variableName, fileName, ref defalutValue);

            return defalutValue;
        }

        public static bool CheckIni(string sectionName, string variableName, string fileName, ref bool value)
        {
            try
            {
                string valueString = value ? "1" : "0";
                if (!CheckIni(sectionName, variableName, fileName, ref valueString))
                {
                    return false;
                }
                if (valueString.Length == 1)
                {
                    value = valueString != "0";
                    return true;
                }

                if (!bool.TryParse(valueString, out value))
                {
                    throw new Exception("The '" + valueString ?? string.Empty + "' is not valid bool value");
                }
                return true;
            }
            catch(Exception e)
            {
                throw new Exception(string.Format("Exception at {0}:[{1}]{2}", fileName, sectionName, variableName), e);
            }
        }

        public static void PutIni<T>(string sectionName, string variableName, string fileName, T value)
        {
            try
            {
                PutIni(sectionName, variableName, fileName, value.ToString());
            }
            catch(Exception e)
            {
                throw new Exception(string.Format("Exception at {0}:[{1}]{2}", fileName, sectionName, variableName), e);
            }
        }

        public static void PutIni(string sectionName, string variableName, string fileName, string value)
        {
            try
            {
                WritePrivateProfileString(sectionName, variableName, value ?? string.Empty, fileName);
            }
            catch(Exception e)
            {
                throw new Exception(string.Format("Exception at {0}:[{1}]{2}", fileName, sectionName, variableName), e);
            }
        }

        public static void PutIni(string sectionName, string variableName, string fileName, bool value)
        {
            try
            {
                PutIni(sectionName, variableName, fileName, value ? "1" : "0");
            }
            catch(Exception e)
            {
                throw new Exception(string.Format("Exception at {0}:[{1}]{2}", fileName, sectionName, variableName), e);
            }
        }

        public static string GetIni(string sectionName, string variableName, string fileName, string defalutValue)
        {
            try
            {
                CheckIni(sectionName, variableName, fileName, ref defalutValue);

                return defalutValue;
            }
            catch(Exception e)
            {
                throw new Exception(string.Format("Exception at {0}:[{1}]{2}", fileName, sectionName, variableName), e);
            }
        }

        public static bool CheckIni(string sectionName, string variableName, string fileName, ref string value)
        {
            try
            {
                StringBuilder sbld = new StringBuilder(1024*4);
                int res = GetPrivateProfileString(sectionName, variableName, string.Empty, sbld, (uint)sbld.Capacity, fileName);
                if(res == 0)
                {
                    return false;
                }

                value = sbld.ToString();

                int semiCollonIndex = value.IndexOf(';');
                if (semiCollonIndex >= 0)
                {
                    value = value.Substring(0, semiCollonIndex);
                }

                value = value.Trim();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Exception at {0}:[{1}]{2}", fileName, sectionName, variableName), e);
            }
        }

        public static List<string> GetSectionNamesList(string fileName)
        {
            try
            {
                byte[] buffer = new byte[1024*3];
                int lenSectionNames;

                GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                try
                {
                    IntPtr buffPtr = handle.AddrOfPinnedObject();
                    lenSectionNames = GetPrivateProfileSectionNames(buffPtr, buffer.Length, fileName);
                }
                finally
                {
                    handle.Free();
                }

                return ParseZerrosStrings(buffer, lenSectionNames);
            }
            catch(Exception e)
            {
                throw new Exception(string.Format("Exception at {0} file", fileName), e);
            }
        }

        public static List<string> GetPrivateProfileSection(string sectionName, string fileName)
        {
            try
            {
                byte[] buffer = new byte[1024*3];
                int bytesReturned;

                GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                try
                {
                    IntPtr buffPtr = handle.AddrOfPinnedObject();
                    bytesReturned = GetPrivateProfileSection(sectionName, buffPtr, buffer.Length, fileName);
                }
                finally
                {
                    handle.Free();
                }

                return ParseZerrosStrings(buffer, bytesReturned);
            }
            catch(Exception e)
            {
                throw new Exception(string.Format("Exception at {0}:[{1}]", fileName, sectionName), e);
            }
        }

        private delegate string Parsing(string src);

        private static List<string> GetPrivateProfileSection(string sectionName, string fileName, Parsing parsing)
        {
            List<string> rawRows = GetPrivateProfileSection(sectionName, fileName);
            List<string> retValue = new List<string>();

            foreach (string row in rawRows)
            {
                string item = parsing(row);
                if (!string.IsNullOrEmpty(item))
                {
                    retValue.Add(item);
                }
            }

            return retValue;
        }

        private static List<string> ParseZerrosStrings(byte[] buffer, int len)
        {
            List<string> retValue = new List<string>();

            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < len; ++i)
            {
                if(buffer[i] == 0)
                {
                    if(sb.Length > 0)
                    {
                        retValue.Add(sb.ToString());
                        sb.Length = 0;
                    }
                }
                else
                {
                    sb.Append((char)buffer[i]);
                }
            }

            return retValue;
        }
    }
}