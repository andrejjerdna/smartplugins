namespace TsepBuilder
{
    public class FilesLoader
    {
        private readonly string _librariesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "binaries", "Libraries");
        private readonly string _iconsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bitmaps");
        private readonly string _modelMacrosPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "binaries", "ModelMacros");
        private readonly string _drawingMacrosPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "binaries", "DrawingMacros");

        public IEnumerable<string> GetAllDlls() => GetFiles(_librariesPath, ".dll");

        public IEnumerable<string> GetAllIcons() => GetFiles(_iconsPath, ".png");

        public IEnumerable<string> GetModelMacros() => GetFiles(_modelMacrosPath, ".cs");

        public IEnumerable<string> GetDrawingMacros() => GetFiles(_drawingMacrosPath, ".cs");

        private IEnumerable<string> GetFiles(string path, string extension)
        {
            return Directory.GetFiles(path).Where(file => Path.GetExtension(file) == extension)
                                    .Select(file => Path.GetFileName(file));
        }
    }
}
