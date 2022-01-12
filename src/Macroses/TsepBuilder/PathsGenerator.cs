namespace TsepBuilder
{
    public class PathsGenerator
    {
        private const string TepDeginitionFileFolder = "%TEPDEFINITIONFILEFOLDER%";

        IDictionary<string, string> SourcePathVariables => GetSourcePathVariables();

        IDictionary<string, string> TargetPathVariables => GetTargetPathVariables();

        public IDictionary<string, string> GetSourcePathVariables()
        {
            return new Dictionary<string, string>()
            {
                { "TepOutputFolder", $@"{TepDeginitionFileFolder}\output" },
                { "BinariesFolder", $@"{TepDeginitionFileFolder}\..\binaries" },
                { "BinariesFolderLibraries", $@"{TepDeginitionFileFolder}\..\binaries\Libraries" },
                { "BinariesFolderModelMacros", $@"{TepDeginitionFileFolder}\..\binaries\ModelMacros" },
                { "BinariesFolderDrawingMacros", $@"{TepDeginitionFileFolder}\..\binaries\DrawingMacros" },
                { "BinariesFolderOther", $@"{TepDeginitionFileFolder}\..\binaries\Other" },
                { "StandardFileFolder", $@"{TepDeginitionFileFolder}\output" },
                { "TepOutputFolder", $@"{TepDeginitionFileFolder}\..\standard file" },
                { "MessagesFolder", $@"{TepDeginitionFileFolder}\..\messages" },
                { "BitmapsFolder", $@"{TepDeginitionFileFolder}\..\bitmaps" },
            };
        }

        public IDictionary<string, string> GetTargetPathVariables()
        {
            return new Dictionary<string, string>()
            {
                { "TepOutputFolder", $@"{TepDeginitionFileFolder}\output" },
                { "BinariesFolder", $@"{TepDeginitionFileFolder}\..\binaries" },
                { "BinariesFolderLibraries", $@"{TepDeginitionFileFolder}\..\binaries\Libraries" },
                { "BinariesFolderModelMacros", $@"{TepDeginitionFileFolder}\..\binaries\ModelMacros" },
                { "BinariesFolderDrawingMacros", $@"{TepDeginitionFileFolder}\..\binaries\DrawingMacros" },
                { "BinariesFolderOther", $@"{TepDeginitionFileFolder}\..\binaries\Other" },
                { "StandardFileFolder", $@"{TepDeginitionFileFolder}\output" },
                { "TepOutputFolder", $@"{TepDeginitionFileFolder}\..\standard file" },
                { "MessagesFolder", $@"{TepDeginitionFileFolder}\..\messages" },
                { "BitmapsFolder", $@"{TepDeginitionFileFolder}\..\bitmaps" },
            };
        }
    }
}
