using System;
using System.IO;
using Cmm.API;
using CMM.Test.GUI.Wrappers;
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
                    IniFileHelper.PutIni(CREATING_TAB_SECTION, "ExportFlatPosition", configPath, (int)model.CreatingTabModel.ExportFlatPosition.Value);
                }

                // Save ImportUpdateTabModel properties
                if (model.ImportUpdateTabModel != null)
                {
                    // Save ResultPath and UsingResultPath properties
                    IniFileHelper.PutIni(IMPORT_UPDATE_SECTION, "ResultPath", configPath, (string)model.ImportUpdateTabModel.ResultPath ?? string.Empty);
                    IniFileHelper.PutIni(IMPORT_UPDATE_SECTION, "UsingResultPath", configPath, model.ImportUpdateTabModel.UsingResultPath?.Value ?? false);
                    
                    // Save new properties
                    IniFileHelper.PutIni(IMPORT_UPDATE_SECTION, "LotId", configPath, (string)model.ImportUpdateTabModel.LotId ?? string.Empty);
                    IniFileHelper.PutIni(IMPORT_UPDATE_SECTION, "WaferId", configPath, (string)model.ImportUpdateTabModel.WaferId ?? string.Empty);
                    IniFileHelper.PutIni(IMPORT_UPDATE_SECTION, "WaferMapMask", configPath, (string)model.ImportUpdateTabModel.WaferMapMask ?? string.Empty);
                    IniFileHelper.PutIni(IMPORT_UPDATE_SECTION, "SubmapId", configPath, (string)model.ImportUpdateTabModel.SubmapId ?? string.Empty);
                    IniFileHelper.PutIni(IMPORT_UPDATE_SECTION, "DataInPath", configPath, (string)model.ImportUpdateTabModel.DataInPath ?? string.Empty);
                    IniFileHelper.PutIni(IMPORT_UPDATE_SECTION, "DataOutPath", configPath, (string)model.ImportUpdateTabModel.DataOutPath ?? string.Empty);
                    IniFileHelper.PutIni(IMPORT_UPDATE_SECTION, "InVerification", configPath, model.ImportUpdateTabModel.InVerification?.Value ?? false);
                    IniFileHelper.PutIni(IMPORT_UPDATE_SECTION, "NotShowMap", configPath, model.ImportUpdateTabModel.NotShowMap?.Value ?? false);
                    IniFileHelper.PutIni(IMPORT_UPDATE_SECTION, "SelectedConverterName", configPath, (string)model.ImportUpdateTabModel.SelectedConverterName ?? string.Empty);
                    IniFileHelper.PutIni(IMPORT_UPDATE_SECTION, "ImportKind", configPath, (int)model.ImportUpdateTabModel.ImportKind.Value);
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
        public static CmmTestModel CreateFromIni(ICmmWrapper cmmWrapper)
        {
            string configPath = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), CONFIG_FILENAME);

            try
            {
                // =================================================================================
                // Create new model
                CmmTestModel model = new CmmTestModel(cmmWrapper);

                // Check if file exists
                if (!File.Exists(configPath))
                {
                    // If file doesn't exist, return model with default settings
                    return new CmmTestModel(cmmWrapper);
                }

                // Load CreatingTabModel properties
                string converterName = IniFileHelper.GetIni(CREATING_TAB_SECTION, "ConverterName", configPath, string.Empty);
                if (!string.IsNullOrEmpty(converterName) && model.CreatingTabModel.ConverterName != null)
                {
                    model.CreatingTabModel.ConverterName = converterName;
                }

                // Load JobName property
                string jobName = IniFileHelper.GetIni(CREATING_TAB_SECTION, "JobName", configPath, string.Empty);
                if (!string.IsNullOrEmpty(jobName) && model.CreatingTabModel.JobName != null)
                {
                    model.CreatingTabModel.JobName = jobName;
                }

                // Load SetupName property
                string setupName = IniFileHelper.GetIni(CREATING_TAB_SECTION, "SetupName", configPath, string.Empty);
                if (!string.IsNullOrEmpty(setupName) && model.CreatingTabModel.SetupName != null)
                {
                    model.CreatingTabModel.SetupName = setupName;
                }

                // Load Lot property
                string lot = IniFileHelper.GetIni(CREATING_TAB_SECTION, "Lot", configPath, string.Empty);
                if (!string.IsNullOrEmpty(lot) && model.CreatingTabModel != null)
                {
                    model.CreatingTabModel.Lot = lot;
                }

                // Load WaferId property
                string waferId = IniFileHelper.GetIni(CREATING_TAB_SECTION, "WaferId", configPath, string.Empty);
                if (!string.IsNullOrEmpty(waferId) && model.CreatingTabModel != null)
                {
                    model.CreatingTabModel.WaferId = waferId;
                }

                model.CreatingTabModel.CreateOnInternalBins = IniFileHelper.GetIni(CREATING_TAB_SECTION, "CreateOnInternalBins", configPath, true);

                model.CreatingTabModel.AssumeAutoCycle = IniFileHelper.GetIni(CREATING_TAB_SECTION, "AssumeAutoCycle", configPath, false);

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

                if (model.CreatingTabModel.ExportFlatPosition != null)
                {
                    model.CreatingTabModel.ExportFlatPosition = (eExportFlatPosition) IniFileHelper.GetIni(CREATING_TAB_SECTION, "ExportFlatPosition", configPath, 0);
                }

                // Load ImportUpdateTabModel properties
                
                // Load ResultPath property
                string resultPath = IniFileHelper.GetIni(IMPORT_UPDATE_SECTION, "ResultPath", configPath, string.Empty);
                if (!string.IsNullOrEmpty(resultPath) && model.ImportUpdateTabModel.ResultPath != null)
                {
                    model.ImportUpdateTabModel.ResultPath = resultPath;
                }

                // Load UsingResultPath property
                if (model.ImportUpdateTabModel.UsingResultPath != null)
                {
                    model.ImportUpdateTabModel.UsingResultPath = IniFileHelper.GetIni(IMPORT_UPDATE_SECTION, "UsingResultPath", configPath, false);
                }
                    
                // Load new properties
                string lotId = IniFileHelper.GetIni(IMPORT_UPDATE_SECTION, "LotId", configPath, string.Empty);
                if (!string.IsNullOrEmpty(lotId) && model.ImportUpdateTabModel.LotId != null)
                {
                    model.ImportUpdateTabModel.LotId = lotId;
                }
                    
                string waferIdImport = IniFileHelper.GetIni(IMPORT_UPDATE_SECTION, "WaferId", configPath, string.Empty);
                if (!string.IsNullOrEmpty(waferIdImport) && model.ImportUpdateTabModel.WaferId != null)
                {
                    model.ImportUpdateTabModel.WaferId = waferIdImport;
                }
                    
                string waferMapMask = IniFileHelper.GetIni(IMPORT_UPDATE_SECTION, "WaferMapMask", configPath, string.Empty);
                if (!string.IsNullOrEmpty(waferMapMask) && model.ImportUpdateTabModel.WaferMapMask != null)
                {
                    model.ImportUpdateTabModel.WaferMapMask = waferMapMask;
                }
                    
                string submapId = IniFileHelper.GetIni(IMPORT_UPDATE_SECTION, "SubmapId", configPath, string.Empty);
                if (!string.IsNullOrEmpty(submapId) && model.ImportUpdateTabModel.SubmapId != null)
                {
                    model.ImportUpdateTabModel.SubmapId = submapId;
                }
                    
                // Load DataInPath property
                string dataInPath = IniFileHelper.GetIni(IMPORT_UPDATE_SECTION, "DataInPath", configPath, string.Empty);
                if (!string.IsNullOrEmpty(dataInPath) && model.ImportUpdateTabModel.DataInPath != null)
                {
                    model.ImportUpdateTabModel.DataInPath = dataInPath;
                }
                    
                // Load DataOutPath property
                string dataOutPath = IniFileHelper.GetIni(IMPORT_UPDATE_SECTION, "DataOutPath", configPath, string.Empty);
                if (!string.IsNullOrEmpty(dataOutPath) && model.ImportUpdateTabModel.DataOutPath != null)
                {
                    model.ImportUpdateTabModel.DataOutPath = dataOutPath;
                }
                    
                // Load InVerification property
                if (model.ImportUpdateTabModel.InVerification != null)
                {
                    model.ImportUpdateTabModel.InVerification = IniFileHelper.GetIni(IMPORT_UPDATE_SECTION, "InVerification", configPath, false);
                }
                    
                // Load NotShowMap property
                if (model.ImportUpdateTabModel.NotShowMap != null)
                {
                    model.ImportUpdateTabModel.NotShowMap = IniFileHelper.GetIni(IMPORT_UPDATE_SECTION, "NotShowMap", configPath, false);
                }
                    
                // Load SelectedConverterName property
                string selectedConverterName = IniFileHelper.GetIni(IMPORT_UPDATE_SECTION, "SelectedConverterName", configPath, string.Empty);
                if (!string.IsNullOrEmpty(selectedConverterName) && model.ImportUpdateTabModel.SelectedConverterName != null)
                {
                    model.ImportUpdateTabModel.SelectedConverterName = selectedConverterName;
                }
                    
                // Load ImportKind property
                if (model.ImportUpdateTabModel.ImportKind != null)
                {
                    model.ImportUpdateTabModel.ImportKind = (eImportWaferMapKind) IniFileHelper.GetIni(IMPORT_UPDATE_SECTION, "ImportKind", configPath, (int) eImportWaferMapKind.ForEnginiring);
                }

                return model;
            }
            catch (Exception ex)
            {
                // Handle load errors
                Console.Error.WriteLine($"Error loading data: {ex}");
                return null;
            }
        }
    }
}
