using TsepBuilder;

var fileLoader = new FilesLoader();
var libraries = fileLoader.GetAllDlls();
ConsoleWriter.WriteData("Libraries:", libraries, ConsoleColor.Cyan);
var icons = fileLoader.GetAllIcons();
ConsoleWriter.WriteData("Icons:", icons, ConsoleColor.Yellow);
var macros = fileLoader.GetModelMacros();
ConsoleWriter.WriteData("Macros:", macros, ConsoleColor.Magenta);

var xmlWriter = new XmlWriter();

xmlWriter.WriteObjects(libraries,
                       nameof(SourcePathVariablePath.BinariesFolderLibraries),
                       TargetPathVariables.CommonMacroDirectoryCommonLibraries,
                       ComponentType.Libraries);

xmlWriter.WriteObjects(icons,
                       nameof(SourcePathVariablePath.BitmapsFolder),
                       TargetPathVariables.BitmapsDirectoryTabs,
                       ComponentType.RibbonImages);

xmlWriter.WriteObjects(macros,
                       nameof(SourcePathVariablePath.BinariesFolderModelMacros),
                       TargetPathVariables.CommonMacroDirectoryModel,
                       ComponentType.ModelMacros);

