﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hasodfnjcaiucniouawhefoiasnviujansviuahfgiuojasnvdf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var thread = new Thread(() =>
              {
                  ThreadProc();
              })
            {
                IsBackground = false,
            };

            thread.SetApartmentState(ApartmentState.STA);

            thread.Start();
        }

        internal void ThreadProc()
        {
            int evt;
            int stylusPointerId;
            int cPackets;
            int cbPacket;
            IntPtr pPackets;

            int iHandle;

            CreateResetEvent(out var handle);

            if (GetPenEventMultiple(0, new IntPtr[0], handle, out iHandle, out evt, out stylusPointerId, out cPackets, out cbPacket, out pPackets))
            {

            }
        }


        [SuppressUnmanagedCodeSecurity]
        [SecurityCritical]
        [DllImport("penimc2_v0400.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CreateResetEvent(out IntPtr handle);

        [SuppressUnmanagedCodeSecurity]
        [SecurityCritical]
        [DllImport("penimc2_v0400.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetPenEvent(IntPtr commHandle, IntPtr handleReset, out int evt, out int stylusPointerId, out int cPackets, out int cbPacket, out IntPtr pPackets);

        [SuppressUnmanagedCodeSecurity]
        [SecurityCritical]
        [DllImport("penimc2_v0400.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetPenEventMultiple(int cCommHandles, IntPtr[] commHandles, IntPtr handleReset, out int iHandle, out int evt, out int stylusPointerId, out int cPackets, out int cbPacket, out IntPtr pPackets);

    }
}
