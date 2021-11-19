namespace SmartPlugins.Common.Abstractions
{
    /// <summary>
    /// Tekla macros
    /// </summary>
    public interface ITeklaMacro
    {
        /// <summary>
        /// Command for once start a macro
        /// </summary>
        void RunOnce();

        /// <summary>
        /// Command for start macro in a loop
        /// </summary>
        void RunLoop();
    }
}
