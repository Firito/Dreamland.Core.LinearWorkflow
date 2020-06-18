using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Dreamland.Core.LinearWorkFlow
{
    [Serializable]
    public class WorkArgsException : ArgumentException
    {
        private readonly WorkArgs _workArgs;

        public WorkArgsException() : base()
        {
        }

        public WorkArgsException(WorkArgs args) : base()
        {
            _workArgs = args;
        }

        public WorkArgsException(string message) : base(message)
        {
        }

        public WorkArgsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public WorkArgsException(string message, string paramName, Exception innerException)
            : base(message, paramName, innerException)
        {
        }

        public WorkArgsException(string? message, string paramName)
            : base(message, paramName)
        {
        }

        protected WorkArgsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override string Message
        {
            get
            {
                var str = base.Message;
                if (_workArgs?.PropertyNameList != null)
                    str = str + $"[The current additional parameter is {JsonConvert.SerializeObject(_workArgs.PropertyNameList)}]";
                return str;
            }
        }
    }
}