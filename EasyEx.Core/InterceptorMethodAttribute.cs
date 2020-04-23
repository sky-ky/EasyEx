using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyEx.Core
{
    /// <summary>
    /// 贴在方法上的标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class InterceptorMethodAttribute : Attribute { }
}
