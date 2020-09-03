using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VS2019OfflineDownload
{
    public class MessageBoxEx
	{
		public static DialogResult Show(string text)
		{
			MessageBoxEx.Initialize();
			return MessageBox.Show(text);
		}

		public static DialogResult Show(string text, string caption)
		{
			MessageBoxEx.Initialize();
			return MessageBox.Show(text, caption);
		}

		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
		{
			MessageBoxEx.Initialize();
			return MessageBox.Show(text, caption, buttons);
		}

		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			MessageBoxEx.Initialize();
			return MessageBox.Show(text, caption, buttons, icon);
		}

		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton)
		{
			MessageBoxEx.Initialize();
			return MessageBox.Show(text, caption, buttons, icon, defButton);
		}

		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton, MessageBoxOptions options)
		{
			MessageBoxEx.Initialize();
			return MessageBox.Show(text, caption, buttons, icon, defButton, options);
		}

		public static DialogResult Show(IWin32Window owner, string text)
		{
			MessageBoxEx._owner = owner;
			MessageBoxEx.Initialize();
			return MessageBox.Show(owner, text);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption)
		{
			MessageBoxEx._owner = owner;
			MessageBoxEx.Initialize();
			return MessageBox.Show(owner, text, caption);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
		{
			MessageBoxEx._owner = owner;
			MessageBoxEx.Initialize();
			return MessageBox.Show(owner, text, caption, buttons);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			MessageBoxEx._owner = owner;
			MessageBoxEx.Initialize();
			return MessageBox.Show(owner, text, caption, buttons, icon);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton)
		{
			MessageBoxEx._owner = owner;
			MessageBoxEx.Initialize();
			return MessageBox.Show(owner, text, caption, buttons, icon, defButton);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton, MessageBoxOptions options)
		{
			MessageBoxEx._owner = owner;
			MessageBoxEx.Initialize();
			return MessageBox.Show(owner, text, caption, buttons, icon, defButton, options);
		}

		[DllImport("user32.dll")]
		private static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle lpRect);

		[DllImport("user32.dll")]
		private static extern int MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

		[DllImport("User32.dll")]
		public static extern UIntPtr SetTimer(IntPtr hWnd, UIntPtr nIDEvent, uint uElapse, MessageBoxEx.TimerProc lpTimerFunc);

		[DllImport("User32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern IntPtr SetWindowsHookEx(int idHook, MessageBoxEx.HookProc lpfn, IntPtr hInstance, int threadId);

		[DllImport("user32.dll")]
		public static extern int UnhookWindowsHookEx(IntPtr idHook);

		[DllImport("user32.dll")]
		public static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxLength);

		[DllImport("user32.dll")]
		public static extern int EndDialog(IntPtr hDlg, IntPtr nResult);

		private static void Initialize()
		{
			if (MessageBoxEx._hHook != IntPtr.Zero)
			{
				throw new NotSupportedException("multiple calls are not supported");
			}
			if (MessageBoxEx._owner != null)
			{
				MessageBoxEx._hHook = MessageBoxEx.SetWindowsHookEx(12, MessageBoxEx._hookProc, IntPtr.Zero, AppDomain.GetCurrentThreadId());
			}
		}

		private static IntPtr MessageBoxHookProc(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode < 0)
			{
				return MessageBoxEx.CallNextHookEx(MessageBoxEx._hHook, nCode, wParam, lParam);
			}
			MessageBoxEx.CWPRETSTRUCT cwpretstruct = (MessageBoxEx.CWPRETSTRUCT)Marshal.PtrToStructure(lParam, typeof(MessageBoxEx.CWPRETSTRUCT));
			IntPtr hHook = MessageBoxEx._hHook;
			if (cwpretstruct.message == 5U)
			{
				try
				{
					MessageBoxEx.CenterWindow(cwpretstruct.hwnd);
				}
				finally
				{
					MessageBoxEx.UnhookWindowsHookEx(MessageBoxEx._hHook);
					MessageBoxEx._hHook = IntPtr.Zero;
				}
			}
			return MessageBoxEx.CallNextHookEx(hHook, nCode, wParam, lParam);
		}

		private static void CenterWindow(IntPtr hChildWnd)
		{
			Rectangle rectangle = new Rectangle(0, 0, 0, 0);
			MessageBoxEx.GetWindowRect(hChildWnd, ref rectangle);
			int num = rectangle.Width - rectangle.X;
			int num2 = rectangle.Height - rectangle.Y;
			Rectangle rectangle2 = new Rectangle(0, 0, 0, 0);
			MessageBoxEx.GetWindowRect(MessageBoxEx._owner.Handle, ref rectangle2);
			Point point = new Point(0, 0);
			point.X = rectangle2.X + (rectangle2.Width - rectangle2.X) / 2;
			point.Y = rectangle2.Y + (rectangle2.Height - rectangle2.Y) / 2;
			Point point2 = new Point(0, 0);
			point2.X = point.X - num / 2;
			point2.Y = point.Y - num2 / 2;
			point2.X = ((point2.X < 0) ? 0 : point2.X);
			point2.Y = ((point2.Y < 0) ? 0 : point2.Y);
			MessageBoxEx.MoveWindow(hChildWnd, point2.X, point2.Y, num, num2, false);
		}

		private static IWin32Window _owner;

		private static MessageBoxEx.HookProc _hookProc = new MessageBoxEx.HookProc(MessageBoxEx.MessageBoxHookProc);

		private static IntPtr _hHook = IntPtr.Zero;

		public const int WH_CALLWNDPROCRET = 12;

		public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

		public delegate void TimerProc(IntPtr hWnd, uint uMsg, UIntPtr nIDEvent, uint dwTime);

		public enum CbtHookAction
		{
			HCBT_MOVESIZE,

			HCBT_MINMAX,

			HCBT_QS,

			HCBT_CREATEWND,

			HCBT_DESTROYWND,

			HCBT_ACTIVATE,

			HCBT_CLICKSKIPPED,

			HCBT_KEYSKIPPED,

			HCBT_SYSCOMMAND,

			HCBT_SETFOCUS
		}

		public struct CWPRETSTRUCT
		{
			public IntPtr lResult;

			public IntPtr lParam;

			public IntPtr wParam;

			public uint message;

			public IntPtr hwnd;
		}
	}
}
