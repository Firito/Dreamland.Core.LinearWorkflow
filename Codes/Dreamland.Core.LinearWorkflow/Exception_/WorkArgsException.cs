using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Dreamland.Core.LinearWorkFlow
{
    /// <summary>
    ///     工作参数异常
    /// </summary>
    [Serializable]
    public class WorkArgsException : ArgumentException
    {
        private readonly WorkArgs _workArgs;

        /// <summary>
        ///     构造函数
        /// </summary>
        public WorkArgsException()
        {
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="args">触发异常的<see cref="WorkArgs" /></param>
        public WorkArgsException(WorkArgs args)
        {
            _workArgs = args;
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        public WorkArgsException(string message) : base(message)
        {
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="innerException">内部异常</param>
        public WorkArgsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="paramName">异常参数名称</param>
        /// <param name="innerException">内部异常</param>
        public WorkArgsException(string message, string paramName, Exception innerException)
            : base(message, paramName, innerException)
        {
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="paramName">异常参数名称</param>
        public WorkArgsException(string message, string paramName)
            : base(message, paramName)
        {
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        protected WorkArgsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        ///     异常信息
        /// </summary>
        public override string Message
        {
            get
            {
                var str = base.Message;
                if (_workArgs?.PropertyNameList != null)
                    str = str +
                          $"[The current additional parameter is {JsonConvert.SerializeObject(_workArgs.PropertyNameList)}]";
                return str;
            }
        }
    }
}