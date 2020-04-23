using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace EasyEx.Core
{
    /// <summary>
    /// AOP方法处理类，实现了IMessageSink接口，以便返回给IContributeObjectSink接口的GetObjectSink方法
    /// 使用特性的方法不能为静态
    /// </summary>
    public sealed class AopHandler : IMessageSink
    {
        //下一个接收器
        private IMessageSink nextSink;

        /// <summary>
        /// 属性
        /// </summary>
        public IMessageSink NextSink
        {
            get { return nextSink; }
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="nextSink"></param>
        public AopHandler(IMessageSink nextSink)
        {
            this.nextSink = nextSink;
        }

        /// <summary>
        /// 同步处理方法
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public IMessage SyncProcessMessage(IMessage msg)
        {
            IMessage retMsg = null;
            var list = GetPropertiesList(msg);//集合取值
            var typeName = !string.IsNullOrEmpty(list[3].ToString()) && list[3].ToString().Contains(",") ? list[3].ToString().Split(',')[0] : "";
            try
            {
                //方法调用消息接口
                IMethodCallMessage call = msg as IMethodCallMessage;

                //如果被调用的方法没打InterceptorMethodAttribute标签
                if (call == null || (Attribute.GetCustomAttribute(call.MethodBase, typeof(InterceptorMethodAttribute))) == null)
                {
                    retMsg = nextSink.SyncProcessMessage(msg);
                }
                //如果打了InterceptorMethodAttribute标签
                else
                {
                    //IMessage.Properties 参数字典
                    //IMessage.Properties
                    //0 __Uri
                    //1 "__MethodName"
                    //2 "__MethodSignature"
                    //3 "__TypeName"
                    //4 "__Args"
                    //5 "__CallContext"

                    Console.WriteLine($"[{typeName}.{list[1]}]开始执行，请求参数：[{JsonConvert.SerializeObject(list[4])}]");

                    retMsg = nextSink.SyncProcessMessage(msg);
                    var reMes = retMsg as ReturnMessage;//returnMessage 取值

                    //有异常
                    if (reMes != null && reMes.Exception != null)
                    {
                        Console.WriteLine($"[{typeName}.{list[1]}]执行异常：[{reMes.Exception.Message}]", reMes.Exception);
                    }
                    else
                    //无异常
                    {
                        var ret = GetPropertiesList(retMsg);
                        Console.WriteLine($"[{typeName}.{list[1]}]执行结束，返回参数：[{JsonConvert.SerializeObject(ret[4])}]");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Aop异常：[{typeName}.{list[1]}]执行异常：[{ex.Message}]", ex);
            }

            return retMsg;
        }

        /// <summary>
        /// 异步处理方法（不需要）
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="replySink"></param>
        /// <returns></returns>
        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            replySink.AsyncProcessMessage(msg, replySink);
            return null;
        }

        /// <summary>
        /// Properties 转集合
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static ArrayList GetPropertiesList(IMessage msg)
        {
            return msg.Properties.Values as ArrayList;
        }
    }
}
