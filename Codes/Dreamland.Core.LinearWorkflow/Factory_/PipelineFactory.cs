using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dreamland.Core.LinearWorkFlow
{
    /// <summary>
    ///     提供类似流水线的工作机制
    /// </summary>
    public class PipelineFactory
    {
        public PipelineFactory()
        {
            Workers = new LinkedList<IWorker>();
        }

        protected LinkedList<IWorker> Workers { get; }

        /// <summary>
        ///     添加<see cref="IWorker" />。
        /// </summary>
        /// <param name="worker"></param>
        public void AddWorker(IWorker worker)
        {
            Workers.AddLast(worker);
        }

        /// <summary>
        ///     按顺序执行工作
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
                result = await worker.ExecuteWorkAsync(args, token).ConfigureAwait(false);
                foreach (var propertyName in result.PropertyNameList)
                    if (result.GetProperty(propertyName, out object obj))
                        args.SetProperty(propertyName, obj);
                if (!result.IsSuccessful) return result;
            }

            return result;
        }
    }
}