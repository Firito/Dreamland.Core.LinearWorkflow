using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dreamland.Core.LinearWorkFlow
{
    /// <summary>
    ///     提供类似责任链模式的工作机制
    /// </summary>
    public class ResponsibilityFactory
    {
        public ResponsibilityFactory()
        {
            Workers = new LinkedList<IResponsibilityWorker>();
        }

        protected LinkedList<IResponsibilityWorker> Workers { get; }

        /// <summary>
        ///     添加<see cref="IResponsibilityWorker" />
        /// </summary>
        /// <param name="worker"></param>
        public void AddWorker(IResponsibilityWorker worker)
        {
            Workers.AddLast(worker);
        }

        /// <summary>
        ///     按顺序执行工作，如果某个<see cref="IResponsibilityWorker" />无法执行，则跳过该 <see cref="IResponsibilityWorker" />
        ///     前一个附加的执行信息会附加到下一个工作者的工作参数中
        /// </summary>
        /// <param name="args"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<WorkResult> ExecuteWorkAsync(WorkArgs args, CancellationToken token)
        {
            var result = new WorkResult {IsSuccessful = true};

            foreach (var worker in Workers)
            {
                if (worker.CanExecuteWork(args))
                {
                    result = await worker.ExecuteWorkAsync(args, token).ConfigureAwait(false);
                    foreach (var propertyName in result.PropertyNameList)
                        if (result.GetProperty(propertyName, out object obj))
                            args.SetProperty(propertyName, obj);
                }

                if (!result.IsSuccessful) return result;
            }

            return result;
        }
    }
}