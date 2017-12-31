using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSDeskBand.Win;
using CSDeskBand;
using System.Runtime.InteropServices;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Sample.Win
{
    /// <summary>
    /// Example winforms deskband. This deskband shows the title of the current focused window.
    /// </summary>
    [ComVisible(true)]
    [Guid("5731FC61-8530-404C-86C1-86CCB8738D06")]
    [CSDeskBandRegistration(Name = "Sample Winforms Deskband")]
    public partial class UserControl1 : CSDeskBandWin
    {
        private readonly WinEventDelegate _delegate;
        private readonly IntPtr _hookId;
        private const uint EVENT_SYSTEM_FOREGROUND = 3;
        private const uint WINEVENT_OUTOFCONTEXT = 0;

        private IEnumerable<CSDeskBandMenuItem> ContextMenuItems
        {
            get
            {
                var _1 = new CSDeskBandMenuAction("1")
                {

                };
                var _2 = new CSDeskBandMenuAction("2")
                {

                };
                var _4 = new CSDeskBandMenuAction("4");
                _4.Clicked += (sender, args) => { _4.Checked = true; };
                var _3 = new CSDeskBandMenu("3")
                {
                    Items = { _4 }
                };

                return new CSDeskBandMenuItem[] {_1, _2, _3};
            }
        }

        public UserControl1()
        {
            var config = new LoggingConfiguration();

            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);
            string path = System.Environment.GetEnvironmentVariable("TEMP");
            if (!path.EndsWith("\\")) path += "\\";
            fileTarget.FileName = path + "templog.txt";
            fileTarget.Layout = "${logger} - ${message}";

            var rule2 = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule2);
            LogManager.Configuration = config;

            InitializeComponent();
            Options.Horizontal.Width = Options.MinHorizontal.Width = 300;
            UpdateLabel();
            _delegate = CallBack;
            _hookId = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, _delegate, 0, 0, WINEVENT_OUTOFCONTEXT);

            Options.ContextMenuItems = ContextMenuItems;
        }

        protected override void OnClose()
        {
            base.OnClose();
            UnhookWinEvent(_hookId);
        }

        private void UpdateLabel()
        {
            label1.Text = GetActiveWindowTitle() ?? label1.Text;
        }

        private void CallBack(IntPtr hWinEventHook, uint eventType, IntPtr hWnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            UpdateLabel();
        }

        public delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hWnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        //Get active window title code from stack overflow
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }
    }
}
