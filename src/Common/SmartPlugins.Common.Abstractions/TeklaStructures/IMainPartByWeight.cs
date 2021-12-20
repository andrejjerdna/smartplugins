namespace SmartPlugins.Common.Abstractions.TeklaStructures
{
    /// <summary>
    /// Main part by weight
    /// </summary>
    public interface IMainPartByWeight
    {
        /// <summary>
        /// Check all assemblies
        /// </summary>
        void CheckAllAssemblies();

        /// <summary>
        /// Check a assembly
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        void CheckAssembly<T>(T assembly) where T: class;
    }
}
