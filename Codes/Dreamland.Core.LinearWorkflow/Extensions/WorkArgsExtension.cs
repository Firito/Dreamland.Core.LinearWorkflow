using Dreamland.Core.LinearWorkFlow;

namespace Dreamland.Core.LinearWorkflow.Extensions
{
    /// <summary>
    ///     带数据的<see cref="WorkArgs" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WorkArgs<T> : WorkArgs
    {
        /// <summary>
        ///     数据
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    ///     提供<see cref="WorkArgs" />的拓展方法
    /// </summary>
    public static class WorkArgsExtension
    {
        /// <summary>
        ///     对<see cref="WorkArgs" />对象进行深拷贝，会同时拷贝附加内容
        /// </summary>
        /// <param name="workArgs"></param>
        /// <returns></returns>
        public static WorkArgs DeepCopy(this WorkArgs workArgs)
        {
            var result = new WorkArgs();

            foreach (var propertyName in workArgs.PropertyNameList)
                if (workArgs.GetProperty(propertyName, out object obj))
                    result.SetProperty(propertyName, obj);

            return result;
        }

        /// <summary>
        ///     对泛型<see cref="WorkArgs" />对象进行深拷贝，会同时拷贝附加内容
        /// </summary>
        /// <param name="workArgs"></param>
        /// <returns></returns>
        public static WorkArgs<T> DeepCopy<T>(this WorkArgs<T> workArgs)
        {
            var result = new WorkArgs<T>
            {
                Data = workArgs.Data
            };

            foreach (var propertyName in workArgs.PropertyNameList)
                if (workArgs.GetProperty(propertyName, out object obj))
                    result.SetProperty(propertyName, obj);

            return result;
        }

        /// <summary>
        ///     将<see cref="WorkArgs" />转换为泛型对象
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="workArgs">待转换的<see cref="WorkArgs" />对象</param>
        /// <param name="copyAttachedContent">是否拷贝附加内容</param>
        /// <returns></returns>
        public static WorkArgs<T> ToWorkArgs<T>(this WorkArgs workArgs, bool copyAttachedContent = true)
        {
            return workArgs.ToWorkArgs<T>(default, copyAttachedContent);
        }

        /// <summary>
        ///     将<see cref="WorkArgs" />转换为泛型对象
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="workArgs">待转换的<see cref="WorkArgs" />对象</param>
        /// <param name="copyAttachedContent">是否拷贝附加内容</param>
        /// <param name="data">泛型数据</param>
        /// <returns></returns>
        public static WorkArgs<T> ToWorkArgs<T>(this WorkArgs workArgs, T data, bool copyAttachedContent = true)
        {
            var result = new WorkArgs<T>
            {
                Data = data
            };

            foreach (var propertyName in workArgs.PropertyNameList)
                if (workArgs.GetProperty(propertyName, out object obj))
                    result.SetProperty(propertyName, obj);

            return result;
        }
    }
}