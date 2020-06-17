using System.Threading;
using System.Threading.Tasks;

namespace Dreamland.Core.LinearWorkFlow
{
    /// <summary>
    ///     工作者的抽象
    /// </summary>
    public interface IWorker
    {
        /// <summary>
        ///     执行任务
        /// </summary>
        /// <returns></returns>
        Task<WorkResult> ExecuteWorkAsync(WorkArgs args, CancellationToken token);
    }
}