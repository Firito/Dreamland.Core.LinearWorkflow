using Dreamland.Core.LinearWorkFlow;

namespace Dreamland.Core.LinearWorkflow.Extensions
{
    /// <summary>
    ///     带数据的<see cref="WorkResult" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WorkResult<T> : WorkResult
    {
        /// <summary>
        ///     数据
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    ///     提供<see cref="WorkResult" />的拓展方法
    /// </summary>
    public static class WorkResultExtension
    {
        /// <summary>
        ///     对<see cref="WorkResult" />对象进行深拷贝，会同时拷贝附加内容
        /// </summary>
        /// <param name="workResult"></param>
        /// <returns></returns>
        public static WorkResult DeepCopy(this WorkResult workResult)
        {
            var result = new WorkResult();

            foreach (var propertyName in workResult.PropertyNameList)
                if (workResult.GetProperty(propertyName, out object obj))
                    result.SetProperty(propertyName, obj);

            return result;
        }

        /// <summary>
        ///     对泛型<see cref="WorkResult" />对象进行深拷贝，会同时拷贝附加内容
        /// </summary>
        /// <param name="workResult"></param>
        /// <returns></returns>
        public static WorkResult<T> DeepCopy<T>(this WorkResult<T> workResult)
        {
            var result = new WorkResult<T>
            {
                Data = workResult.Data
            };

            foreach (var propertyName in workResult.PropertyNameList)
                if (workResult.GetProperty(propertyName, out object obj))
                    result.SetProperty(propertyName, obj);

            return result;
        }

        /// <summary>
        ///     将<see cref="WorkResult" />转换为泛型对象
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="workResult">待转换的<see cref="WorkResult" />对象</param>
        /// <param name="copyAttachedContent">是否拷贝附加内容</param>
        /// <returns></returns>
        public static WorkResult<T> ToWorkResult<T>(this WorkResult workResult, bool copyAttachedContent = true)
        {
            return workResult.ToWorkResult<T>(default, copyAttachedContent);
        }

        /// <summary>
        ///     将<see cref="WorkResult" />转换为泛型对象
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="workResult">待转换的<see cref="WorkResult" />对象</param>
        /// <param name="copyAttachedContent">是否拷贝附加内容</param>
        /// <param name="data">泛型数据</param>
        /// <returns></returns>
        public static WorkResult<T> ToWorkResult<T>(this WorkResult workResult, T data, bool copyAttachedContent = true)
        {
            var result = new WorkResult<T>
            {
                IsSuccessful = workResult.IsSuccessful,
                Code = workResult.Code,
                Message = workResult.Message,
                Data = data
            };

            foreach (var propertyName in workResult.PropertyNameList)
                if (workResult.GetProperty(propertyName, out object obj))
                    result.SetProperty(propertyName, obj);

            return result;
        }
    }
}