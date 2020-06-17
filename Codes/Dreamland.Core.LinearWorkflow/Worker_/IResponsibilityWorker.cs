namespace Dreamland.Core.LinearWorkFlow
{
    /// <summary>
    ///     工作者的抽象
    /// </summary>
    public interface IResponsibilityWorker : IWorker
    {
        /// <summary>
        ///     是否能够胜执行任务
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool CanExecuteWork(WorkArgs args);
    }
}