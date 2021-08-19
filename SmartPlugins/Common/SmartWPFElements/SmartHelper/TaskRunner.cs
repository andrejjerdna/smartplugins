using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlugins.Common.SmartWPFElements.SmartHelper
{
    public class TaskRunner
    {
        public TaskRunner(BaseViewModel owner)
        {
            Owner = owner;
        }

        public BaseViewModel Owner { get; }


        public void Run(Action action)
        {
            Run(() =>
            {
                action();
                return Task.CompletedTask;
            });
        }

        public void Run(Func<Task> action)
        {
            Task.Run(() =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    Owner.ShowMsg(e.Message);
                }
            });
        }
    }
}
