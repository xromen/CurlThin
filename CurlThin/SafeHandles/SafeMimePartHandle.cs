using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static NetUV.Core.Common.ThreadLocalPool;

namespace CurlThin.SafeHandles
{
    public class SafeMimePartHandle : SafeHandle
    {
        private SafeMimePartHandle() : base(IntPtr.Zero, false)
        {
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            throw new NotImplementedException();
        }
    }
}
