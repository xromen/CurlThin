using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static NetUV.Core.Common.ThreadLocalPool;

namespace CurlThin.SafeHandles
{
    public class SafeMimeHandle : SafeHandle
    {
        private SafeMimeHandle() : base(IntPtr.Zero, false)
        {
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            CurlNative.Mime.Free(handle);
            return true;
        }
    }
}
