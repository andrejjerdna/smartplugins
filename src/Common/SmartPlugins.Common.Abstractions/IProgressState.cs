using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlugins.Common.Abstractions
{
    public interface IProgressState
    {
        /// <summary>
        /// Current value
        /// </summary>
        int CurrentValue { get; }

        /// <summary>
        /// Total count
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Message
        /// </summary>
        string Message { get; }
    }
}
