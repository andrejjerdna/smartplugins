using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsepBuilder
{
    public struct SourcePathVariablePath
    {
        public const string TepDeginitionFileFolder = "TEPDEFINITIONFILEFOLDER%";

        public const string TepOutputFolder = $@"%TepOutputFolder%\output";
        public const string BinariesFolder = $@"%BinariesFolder%\..\binaries";
        public const string BinariesFolderLibraries = $@"%BinariesFolderLibraries%\..\binaries\Libraries";
        public const string BinariesFolderModelMacros = $@"%BinariesFolderModelMacros%\..\binaries\ModelMacros";
        public const string BinariesFolderDrawingMacros = $@"%BinariesFolderDrawingMacros%\..\binaries\DrawingMacros";
        public const string BinariesFolderOther = $@"%BinariesFolderOther%\..\binaries\Other";
        public const string StandardFileFolder = $@"%StandardFileFolder%\..\standard file";
        public const string MessagesFolder = $@"%MessagesFolder%\..\messages";
        public const string BitmapsFolder = $@"%BitmapsFolder%\..\bitmaps";
    }
}
