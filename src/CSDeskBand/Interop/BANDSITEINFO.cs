using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    struct BANDSITEINFO
    {
        public BSIM dwMask;
        public BSSF dwState;
        public BSIS dwStyle;

        [Flags]
        public enum BSIM : uint
        {
            BSIM_STATE = 0x00000001,
            BSIM_STYLE = 0x00000002,
        }

        [Flags]
        public enum BSSF : uint
        {
            BSSF_VISIBLE = 0x00000001,
            BSSF_NOTITLE = 0x00000002,
            BSSF_UNDELETEABLE = 0x00001000,
        }

        [Flags]
        public enum BSIS : uint
        {
            BSIS_AUTOGRIPPER = 0x00000000,
            BSIS_NOGRIPPER = 0x00000001,
            BSIS_ALWAYSGRIPPER = 0x00000002,
            BSIS_LEFTALIGN = 0x00000004,
            BSIS_SINGLECLICK = 0x00000008,
            BSIS_NOCONTEXTMENU = 0x00000010,
            BSIS_NODROPTARGET = 0x00000020,
            BSIS_NOCAPTION = 0x00000040,
            BSIS_PREFERNOLINEBREAK = 0x00000080,
            BSIS_LOCKED = 0x00000100,
            BSIS_PRESERVEORDERDURINGLAYOUT = 0x00000200,
            BSIS_FIXEDORDER = 0x00000400,
        }
    }
}
