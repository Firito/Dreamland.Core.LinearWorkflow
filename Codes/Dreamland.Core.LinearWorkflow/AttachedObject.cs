using System.Collections.Generic;

namespace Dreamland.Core.LinearWorkFlow
{
    public class AttachedObject
    {
        private readonly List<string> _propertyNameList = new List<string>();

        /// <summary>
        ///     参数名称列表
        /// </summary>
        public IReadOnlyCollection<string> PropertyNameList => _propertyNameList;

        /// <summary>
        ///     增加或更新参数
        /// </summary>
        /// <param name="argsName"></param>
        /// <param name="argsValue"></param>
        public void SetProperty(string argsName, object argsValue)
        {
            this.SetAttachedProperty(argsName, argsValue);
            if (!_propertyNameList.Contains(argsName)) _propertyNameList.Add(argsName);
        }

        /// <summary>
        ///     获取
        /// </summary>
        /// <typeparam name="TArgs"></typeparam>
        /// <param name="argsName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool GetProperty<TArgs>(string argsName, out TArgs args)
        {
            args = default;
            var argsValue = this.GetAttachedProperty(argsName);
            if (!(argsValue is TArgs value)) return false;
            args = value;
            return true;
        }
    }
}