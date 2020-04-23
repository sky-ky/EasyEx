using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace EasyEx.Core
{
    /// <summary>
    /// 接收器
    /// </summary>
     [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class InterceptorAttribute : ContextAttribute, IContributeObjectSink
    {
        public InterceptorAttribute()
    : base("Interceptor")
        { }

        /// <summary>
        /// 实现IContributeObjectSink接口当中的消息接收器接口
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink next)
        {
            return new AopHandler(next);
        }
    }
}
