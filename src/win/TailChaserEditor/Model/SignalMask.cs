using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.TailChaser.Editor.Model
{
    [Flags]
    public enum SignalMask
    {
        Tail = 0x01,
        Brake = 0x02,
        Reverse = 0x04,
        IndicatorFlash = 0x08,
        IndicatorSolid = 0x10,
    }
}
