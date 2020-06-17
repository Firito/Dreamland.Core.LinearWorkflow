namespace Dreamland.Core.LinearWorkFlow
{
    /// <summary>
    ///     执行工作所需要的参数
    /// </summary>
    public class WorkArgs : AttachedObject
    {
        /// <summary>
        ///     增加或更新参数
        /// </summary>
        /// <param name="argsName"></param>
        /// <param name="argsValue"></param>
        public new WorkArgs SetProperty(string argsName, object argsValue)
        {
            base.SetProperty(argsName, argsValue);
            return this;
        }
    }
}