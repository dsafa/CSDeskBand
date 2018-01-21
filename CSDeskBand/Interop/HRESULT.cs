namespace CSDeskBand.Interop
{
    internal class HRESULT
    {
        public static readonly int S_OK = 0;
        public static readonly int S_FALSE = 1;
        public static readonly int E_NOTIMPL = unchecked((int)0x80004001);
        public static readonly int E_FAIL = unchecked((int)0x80004005);

        public static int MakeHResult(uint sev, uint facility, uint errorNo)
        {
            uint result = sev << 31 | facility << 16 | errorNo;
            return unchecked((int)result);
        }
    }
}