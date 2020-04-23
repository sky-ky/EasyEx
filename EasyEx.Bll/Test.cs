using EasyEx.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyEx.Bll
{
    /// <summary>
    /// 类特性
    /// </summary>
    [Interceptor]
    public class Test: ContextBoundObject //继承上下文绑定类基类
    {
        /// <summary>
        /// 测试方法
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        /// 
        [InterceptorMethod]  // 方法特性
        public string GetMessage(string a,string b,string c)
        {
            var msg = new StringBuilder();
            msg.Append(a).Append(" ").Append(b).Append(" ").Append(c);
            return msg.ToString();
        }
    }
}
