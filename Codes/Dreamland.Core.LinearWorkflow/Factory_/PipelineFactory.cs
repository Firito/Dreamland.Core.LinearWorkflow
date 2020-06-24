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
        /// <param name="args">工作参数</param>
        /// <param name="token"></param>
        /// <param name="isOutputAttachedResult">是否输出附加结果</param>
        /// <returns></returns>
        public async Task<WorkResult> ExecuteWorkAsync(WorkArgs args, CancellationToken token,
            bool isOutputAttachedResult = false)
        {
            var result = new WorkResult {IsSuccessful = true};
            var resultContents = new Dictionary<string, object>();

            //创建一个新的工作参数的副本用于工作流程中
            var innerArgs = new WorkArgs();
            foreach (var propertyName in args.PropertyNameList)
                if (args.GetProperty(propertyName, out object obj))
                    innerArgs.SetProperty(propertyName, obj);

            foreach (var worker in Workers)
            {
                result = await worker.ExecuteWorkAsync(innerArgs, token).ConfigureAwait(false);
                foreach (var propertyName in result.PropertyNameList)
                    if (result.GetProperty(propertyName, out object obj))
                    {
                        innerArgs.SetProperty(propertyName, obj);
                        resultContents.SetAttachedProperty(propertyName, obj);
                    }

                if (!result.IsSuccessful)
                {
                    break;
                }
            }

            if (!isOutputAttachedResult) return result;

            //如果输出时需要附加结果，则将执行过程中产生的结果附加到返回值中
            foreach (var (key, value) in resultContents)
                result.SetProperty(key, value);
            return result;
        }
    }
}