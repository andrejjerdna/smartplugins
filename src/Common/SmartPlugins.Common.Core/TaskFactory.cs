using SmartPlugins.Common.Abstractions.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartPlugins.Common.Core
{
    /// <summary>
    /// Task facrory
    /// </summary>
    public class TaskFactory : ITaskFactory
    {
        private readonly List<Task> _tasks = new List<Task>();

        /// <inheritdoc/>
        public void AddTask(Task task) => _tasks.Add(task);

        /// <inheritdoc/>
        public void WaitAllTasks() => Task.WaitAll(_tasks.ToArray());

        /// <inheritdoc/>
        public Task WhenAll() => Task.WhenAll(_tasks);
    }
}
