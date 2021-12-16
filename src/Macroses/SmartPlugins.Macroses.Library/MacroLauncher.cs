using SmartPlugins.Common.Abstractions;

namespace SmartPlugins.Macroses.Library
{
    /// <summary>
    /// Macro launcher
    /// </summary>
    public class MacroLauncher
    {
        private MacrosesContainerConfigure _container = MacrosesContainerConfigure.GetContainer();

        /// <summary>
        /// Macro in once run mode
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void MacroRunOnce<T>() where T : class, ITeklaMacro
        {
            _container.GetRequiredService<T>().RunOnce();
        }

        /// <summary>
        /// Macro in loop run mode
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void MacroRunLoop<T>() where T : class, ITeklaMacro
        {
            _container.GetRequiredService<T>().RunLoop();
        }
    }
}
