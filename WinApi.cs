using Accessibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WxMsgHelper
{
    class WinApi
    {
        public const int OBJID_CLIENT = -4;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("Oleacc.dll")]
        public static extern int AccessibleChildren(
        Accessibility.IAccessible paccContainer,
        int iChildStart,
        int cChildren,
        [Out] object[] rgvarChildren,
        out int pcObtained);

        [DllImport("Oleacc.dll")]
        public static extern int AccessibleObjectFromWindow(
        IntPtr hwnd,
        int dwObjectID,
        ref Guid refID,
        ref IAccessible ppvObject);

        [DllImport("Oleacc.dll")]
        public static extern int get_accChildCount(
         long pcountChildren
        );

    }
}
