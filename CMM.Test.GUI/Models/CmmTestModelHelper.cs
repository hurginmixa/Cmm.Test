using System;
using System.IO;
using CMM.Test.GUI.CmmWrappers;
using Converters.Tools;

namespace CMM.Test.GUI.Models
{
    /// <summary>
    /// Helper class for saving and loading CmmTestModel data
    /// </summary>
    public static class CmmTestModelHelper
    {
        private const string CREATING_TAB_SECTION = "CreatingTab";
        private const string IMPORT_UPDATE_SECTION = "ImportUpdateTab";
        private const string CONFIG_FILENAME = "CmmTest.ini";

        /// <summary>
        /// Saves model data to INI file
        /// </summary>
        /// <param name="model">Model to save</param>
        /// <param name="filePath">Path to INI file (if not specified, default file is used)</param>
        public static void SaveToIni(CmmTestModel model, string filePath = null)
        {
            if (model == null)
            {
                return;
            }

            string configPath = filePath ?? Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), CONFIG_FILENAME);

            try
            {
                // Save CreatingTabModel properties
                if (model.CreatingTabModel != null)
                {
                    // Save ConverterName property
                    IniFileHelper.PutIni(CREATING_TAB_SECTION, "ConverterName", configPath, (string)model.CreatingTabModel.ConverterName ?? string.Empty);
                    IniFileHelper.PutIni(CREATING_TAB_SECTION, "JobName", configPath, (string)model.CreatingTabModel.JobName ?? string.Empty);
                    IniFileHelper.PutIni(CREATING_TAB_SECTION, "SetupName", configPath, (string)model.CreatingTabModel.SetupName ?? string.Empty);
                    IniFileHelper.PutIni(CREATING_TAB_SECTION, "Lot", configPath, (string)model.CreatingTabModel.Lot ?? string.Empty);
                    IniFileHelper.PutIni(CREATING_TAB_SECTION, "WaferId", configPath, (string)model.CreatingTabModel.WaferId ?? string.Empty);
                    IniFileHelper.PutIni(CREATING_TAB_SECTION, "CreateOnInternalBins", configPath, model.CreatingTabModel.CreateOnInternalBins?.Value ?? false);
                    IniFileHelper.PutIni(CREATING_TAB_SECTION, "AssumeAutoCycle", configPath, model.CreatingTabModel.AssumeAutoCycle?.Value ?? false);
                    IniFileHelper.PutIni(CREATING_TAB_SECTION, "AssumeVerification", configPath, model.CreatingTabModel.AssumeVerification?.Value ?? false);
                    IniFileHelper.PutIni(CREATING_TAB_SECTION, "NotShowMap", configPath, model.CreatingTabModel.NotShowMap?.Value ?? false);
                    IniFileHelper.PutIni(CREATING_TAB_SECTION, "ImportAfterCreate", configPath, model.CreatingTabModel.ImportAfterCreate?.Value ?? false);
                }

                // Save ImportUpdateTabModel properties
                if (model.ImportUpdateTabModel != null)
                {
                    // Here you can add code to save ImportUpdateTabModel properties
                    // Example:
                    // IniFileHelper.PutIni(IMPORT_UPDATE_SECTION, "PropertyName", configPath, model.ImportUpdateTabModel.PropertyName);
                }
            }
            catch (Exception ex)
            {
                // Handle save errors
                Console.WriteLine($"Error saving data: {ex}");
            }
        }

        /// <summary>
        /// Creates and fills CmmTestModel object from INI file
        /// </summary>
        /// <param name="cmmWrapper">ICmmWrapper instance</param>
        /// <param name="filePath">Path to INI file (if not specified, default file is used)</param>
        /// <returns>Filled CmmTestModel instance or null in case of error</returns>
        public static CmmTestModel CreateFromIni(ICmmWrapper cmmWrapper, string filePath = null)
        {
            string configPath = filePath ?? Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), CONFIG_FILENAME);

            try
            {
                // Check if file exists
                if (!File.Exists(configPath))
                {
                    // If file doesn't exist, return model with default settings
                    return new CmmTestModel(cmmWrapper);
                }

                // Create new model
                CmmTestModel model = new CmmTestModel(cmmWrapper);

                // Load CreatingTabModel properties
                if (model.CreatingTabModel != null)
                {
                    // Load ConverterName property
                    string converterName = IniFileHelper.GetIni(CREATING_TAB_SECTION, "ConverterName", configPath, string.Empty);
                    if (!string.IsNullOrEmpty(converterName) && model.CreatingTabModel.ConverterName != null)
                    {
                        model.CreatingTabModel.ConverterName = converterName;
                    }
                    
                    string jobName = IniFileHelper.GetIni(CREATING_TAB_SECTION, "JobName", configPath, string.Empty);
                    if (!string.IsNullOrEmpty(jobName) && model.CreatingTabModel.JobName != null)
                    {
                        model.CreatingTabModel.JobName = jobName;
                    }

                    string setupName = IniFileHelper.GetIni(CREATING_TAB_SECTION, "SetupName", configPath, string.Empty);
                    if (!string.IsNullOrEmpty(setupName) && model.CreatingTabModel.SetupName != null)
                    {
                        model.CreatingTabModel.SetupName = setupName;
                    }

                    string lot = IniFileHelper.GetIni(CREATING_TAB_SECTION, "Lot", configPath, string.Empty);
                    if (!string.IsNullOrEmpty(lot) && model.CreatingTabModel.Lot != null)
                    {
                        model.CreatingTabModel.Lot = lot;
                    }

                    string waferId = IniFileHelper.GetIni(CREATING_TAB_SECTION, "WaferId", configPath, string.Empty);
                    if (!string.IsNullOrEmpty(waferId) && model.CreatingTabModel.WaferId != null)
                    {
                        model.CreatingTabModel.WaferId = waferId;
                    }

                    if (model.CreatingTabModel.CreateOnInternalBins != null)
                    {
                        model.CreatingTabModel.CreateOnInternalBins = IniFileHelper.GetIni(CREATING_TAB_SECTION, "CreateOnInternalBins", configPath, true);
                    }

                    if (model.CreatingTabModel.AssumeAutoCycle != null)
                    {
                        model.CreatingTabModel.AssumeAutoCycle = IniFileHelper.GetIni(CREATING_TAB_SECTION, "AssumeAutoCycle", configPath, false);
                    }

                    if (model.CreatingTabModel.AssumeVerification != null)
                    {
                        model.CreatingTabModel.AssumeVerification = IniFileHelper.GetIni(CREATING_TAB_SECTION, "AssumeVerification", configPath, true);
                    }

                    if (model.CreatingTabModel.NotShowMap != null)
                    {
                        model.CreatingTabModel.NotShowMap = IniFileHelper.GetIni(CREATING_TAB_SECTION, "NotShowMap", configPath, false);
                    }

                    if (model.CreatingTabModel.ImportAfterCreate != null)
                    {
                        model.CreatingTabModel.ImportAfterCreate = IniFileHelper.GetIni(CREATING_TAB_SECTION, "ImportAfterCreate", configPath, true);
                    }
                }

                // Load ImportUpdateTabModel properties
                if (model.ImportUpdateTabModel != null)
                {
                    // Here you can add code to load ImportUpdateTabModel properties
                    // Example:
                    // string propertyValue = IniFileHelper.GetIni(IMPORT_UPDATE_SECTION, "PropertyName", configPath, defaultValue);
                    // if (propertyValue != null)
                    // {
                    //     model.ImportUpdateTabModel.PropertyName = propertyValue;
                    // }
                }

                return model;
            }
            catch (Exception ex)
            {
                // Handle load errors
                Console.WriteLine($"Error loading data: {ex}");
                return null;
            }
        }
    }
}
