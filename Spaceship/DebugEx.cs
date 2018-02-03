using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    [DebuggerStepThrough]
    public static class DebugEx
    {
        public static void Assert(bool condition, string message = "")
        {
            if (!condition)
            {
                Debugger.Break();
            }
        }

        public static void Fail(string message = "") => Debugger.Break();
    }
}
