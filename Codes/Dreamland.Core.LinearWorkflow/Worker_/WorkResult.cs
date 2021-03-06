﻿namespace Dreamland.Core.LinearWorkFlow
{
    /// <summary>
    ///     工作执行结果
    /// </summary>
    public class WorkResult : AttachedObject
    {
        /// <summary>
        ///     是否成功的结果
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        ///     结果码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        ///     消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     增加或更新参数
        /// </summary>
        /// <param name="argsName"></param>
        /// <param name="argsValue"></param>
        public new WorkResult SetProperty(string argsName, object argsValue)
        {
            base.SetProperty(argsName, argsValue);
            return this;
        }
    }
}