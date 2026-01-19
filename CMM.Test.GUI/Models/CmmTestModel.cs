using Converters.Tools;
using System;
using System.IO;
using CMM.Test.GUI.Wrappers;

namespace CMM.Test.GUI.Models
{
    public class CmmTestModel
    {
        public CmmTestModel(IWrappers wrapper)
        {
            CreatingTabModel = new CreatingTabModel(this, wrapper);

            ImportUpdateTabModel = new ImportUpdateTabModel(this, wrapper);
        }

        public CreatingTabModel CreatingTabModel { get; }

        public ImportUpdateTabModel ImportUpdateTabModel { get; }
    }
}