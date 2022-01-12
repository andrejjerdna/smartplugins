using SmartPlugins.Common.Abstractions;

namespace SmartPlugins.Macros.Library
{
    /// <summary>
    /// Macro launcher
    /// </summary>
    public class MacroLauncher
    {
        private MacrosContainerConfigure _container = MacrosContainerConfigure.GetContainer();

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
