using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WxMsgHelper.WinApi;
using Accessibility;
using System.Diagnostics;
using System.Windows;

namespace WxMsgHelper
{
    public class WXMsg
    {
        public  static string WXName = "微信测试版";
        public  static string WXClassName = "WeChatMainWndForPC";
        public  static IntPtr WXHandle = IntPtr.Zero;
        private Accessibility.IAccessible IACurrent = null;
        /// <summary>
        /// 初始化并绑定窗口
        /// </summary>
        /// <returns></returns>
        public  void  Init() {
            WXHandle = FindWindow(WXClassName, WXName);
            Guid guidCOM = new Guid(0x618736E0, 0x3C3D, 0x11CF, 0x81, 0xC, 0x0, 0xAA, 0x0, 0x38, 0x9B, 0x71);
            AccessibleObjectFromWindow(WXHandle, (int)WinApi.OBJID_CLIENT, ref guidCOM, ref IACurrent);
        }

        /// <summary>
        /// 获取微信名称
        /// </summary>
        /// <returns></returns>
        public string GetWxName() {
            IACurrent = (IAccessible)IACurrent.accParent;
            IAccessible inputBox = GetAccessibleChild(IACurrent, new int[] { 3, 0, 1, 0, 0 });

            return inputBox.accName[0];
        }

        public List<WxMsgInfo> GetWxMsg() {
            List<WxMsgInfo> ts = new List<WxMsgInfo>();

            IACurrent = (IAccessible)IACurrent.accParent;
            IAccessible inputBox = GetAccessibleChild(IACurrent, new int[] { 3,   0, 1, 2,   0 , 0 , 0 ,0 ,  1,0,0,0});

            for (int i = 0; i < inputBox.accChildCount; i++)
            {
                WxMsgInfo tinfo = new WxMsgInfo();

                IAccessible tmpObj = GetAccessibleChild(inputBox, new int[] { i , 0});

                if (tmpObj.accChildCount >= 3)//判断是否为微信的消息体结构 
                {
                    

                    if (tmpObj.accName[3] == null) //如果读消息时候 不是自己发送的消息
                    {

                        tinfo.name = tmpObj.accName[1];
                    }
                    else 
                    {
                        tinfo.name = tmpObj.accName[3];

                    }

                    if (tinfo.name == null)//是否为空
                    {
                        continue;
                    }


                    IAccessible Msg = GetAccessibleChild(tmpObj, new int[] { 1 });
                    
                    if (Msg.accChildCount == 2)  //消息数量为1是自己发的 为2是别人发的
                    {

                        IAccessible UserMsg = GetAccessibleChild(Msg, new int[] { 1, 0, 0 });
                        tinfo.value = UserMsg.accName[UserMsg.accChildCount];
                       
                    }
                    else if(Msg.accChildCount ==1 )
                    {
                        IAccessible UserMsg = GetAccessibleChild(Msg, new int[] { 0 , 0 , 0 });
                        tinfo.value = UserMsg.accName[UserMsg.accChildCount];
                       ;
                    }
                    
                    ts.Add(tinfo);//加入列表
                    
                    Debug.WriteLine($"Name:{tinfo.name},Value:{tinfo.value}");
                    
                }
                
               

            }
            
            return ts;
        }


        private IAccessible[] GetAccessibleChildren(IAccessible paccContainer)
        {
            IAccessible[] rgvarChildren = new IAccessible[paccContainer.accChildCount];
            int pcObtained;
            WinApi.AccessibleChildren(paccContainer, 0, paccContainer.accChildCount, rgvarChildren, out pcObtained);
            return rgvarChildren;
        }

        public IAccessible GetAccessibleChild(IAccessible paccContainer, int[] array)
        {
            if (array.Length > 0)
            {
                IAccessible result = GetAccessibleChildren(paccContainer)[array[0]];

                int[] array_1 = new int[array.Length - 1];
                for (int i = 0; i < array.Length - 1; i++)
                {
                    array_1[i] = array[i + 1];
                }
                return GetAccessibleChild(result, array_1);
            }
            else
            {
                return paccContainer;
            }
        }

    }

    public struct WxMsgInfo {
       public string name;
       public string value;
        
    };
}
