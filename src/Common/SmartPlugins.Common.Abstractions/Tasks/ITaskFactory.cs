using System.Threading.Tasks;

namespace SmartPlugins.Common.Abstractions.Tasks
{
    /// <summary>
    /// Task facrory interface
    /// </summary>
    public interface ITaskFactory
    {
        /// <summary>
        /// Add task to factory
        /// </summary>
        /// <param name="task"></param>
        void AddTask(Task task);

        /// <summary>
        /// Wait all tasks
        /// </summary>
        void WaitAllTasks();

        /// <summary>
        /// Creates a task that will complete when all of the System.Threading.Tasks.Task
        ///  objects in an enumerable collection have completed.
        /// </summary>
        /// <returns></returns>
        Task WhenAll();
    }
}
