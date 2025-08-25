using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace prkiller_ng
{
	/// <summary>
	/// Common parts of Process Killer
	/// </summary>
	internal static class Killer
	{
		internal static IniFile Config;
		internal static IniFile Language;

		//internal static Dictionary<int, ProcessInfo> ProcessCache = new();
		internal static Dictionary<int, TimeSpan> LastCpuTime = new();

		static Killer()
		{
			try
			{
				Config = new();
				Language = new();
			}
			catch { }
		}

		/// <summary>
		/// Creant an ProcessStartInfo instance for specified <paramref name="commandLine"/>.
		/// </summary>
		/// <param name="commandLine">String like used in cmd.exe prompt.</param>
		/// <returns>An ProcessStartInfo instance.</returns>
		public static System.Diagnostics.ProcessStartInfo CreateProcessStartInfo(string commandLine)
		{
			string cmdLine = commandLine.Trim();
			string exe = "";
			string args = "";
			char exeEnd = ' ';
			bool inExe = true;

			for (int c = 0; c < cmdLine.Length; c++)
			{
				if (c == 0 && cmdLine[c] == '\"')
				{
					//start of quoted exe name
					exeEnd = cmdLine[c];
					continue;
				}
				if (cmdLine[c] == exeEnd && inExe) { inExe = false; continue; } //end of exe name

				if (inExe) exe += cmdLine[c];
				if (!inExe) args += cmdLine[c];
			}

			exe.TrimStart(' ');
			exe.TrimEnd(' ');
			args.TrimStart(' ');
			args.TrimEnd(' ');

			return new(exe, args) { UseShellExecute = true };
		}

		internal enum DoubleClickAction
		{
			Disable = 0, ProcessInfo = 1, Kill = 2
		}
		internal enum RightClickAction
		{
			Disable = 0, ContextMenu = 1
		}

		internal enum CpuGraphStyle
		{
			Disable = 0, Bar = 1, Graph = 2, Label = 3
		}

		internal enum KillPolicy
		{
			Disable = 0, Prompt = 1, Enable = 2
		}

		internal enum ErrorSound
		{
			Disable = 0, Beep = 1, SpeakerBeep = 2, Asterisk = 3, Exclamination = 4, Hand = 5, Question = 6
		}

		internal enum KeyboardCommand
		{
			Hide, Exit, MoveUp, MoveDown, ContextMenu,
			Kill, KillDontHide, KillProcessTree, KillProcessTreeDontHide,
			ProcessInfo, RunDialog,
			PriorityIncrease, PriorityDecrease, PriorityIdle, PriorityNormal, PriorityHigh, PriorityRealTime,
			FindParent, Restart, RestartAsAdmin, SuspendResumeProcess, SuspendProcess, ResumeProcess,
			RestartExplorer
		}


		//WinAPI interaction

		/// <summary>
		/// Locks the workstation's display. Locking a workstation protects it from unauthorized use.
		/// </summary>
		/// <returns>If the function succeeds, the return value is nonzero. Because the function executes asynchronously, a nonzero return value indicates that the operation has been initiated. It does not indicate whether the workstation has been successfully locked.
		///
		///If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
		[DllImport("user32.dll")]
		internal static extern bool LockWorkStation();

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct TokPriv1Luid
		{
			public int Count;
			public long Luid;
			public int Attr;
		}

		/// <summary>
		/// Retrieves a pseudo handle for the current process.
		/// </summary>
		/// <returns>The return value is a pseudo handle to the current process.</returns>
		[DllImport("kernel32.dll", ExactSpelling = true)]
		internal static extern IntPtr GetCurrentProcess();

		/// <summary>
		/// The OpenProcessToken function opens the access token associated with a process.
		/// </summary>
		/// <param name="h">A handle to the process whose access token is opened. The process must have the PROCESS_QUERY_LIMITED_INFORMATION access permission. See Process Security and Access Rights for more info.</param>
		/// <param name="acc">Specifies an access mask that specifies the requested types of access to the access token. These requested access types are compared with the discretionary access control list (DACL) of the token to determine which accesses are granted or denied.</param>
		/// <param name="phtok">A pointer to a handle that identifies the newly opened access token when the function returns.</param>
		/// <returns></returns>
		[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr
		phtok);

		/// <summary>
		/// The LookupPrivilegeValue function retrieves the locally unique identifier (LUID) used on a specified system to locally represent the specified privilege name.
		/// </summary>
		/// <param name="host">A pointer to a null-terminated string that specifies the name of the system on which the privilege name is retrieved. If a null string is specified, the function attempts to find the privilege name on the local system.</param>
		/// <param name="name">A pointer to a null-terminated string that specifies the name of the privilege, as defined in the Winnt.h header file. For example, this parameter could specify the constant, SE_SECURITY_NAME, or its corresponding string, "SeSecurityPrivilege".</param>
		/// <param name="pluid">A pointer to a variable that receives the LUID by which the privilege is known on the system specified by the lpSystemName parameter.</param>
		/// <returns></returns>
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern bool LookupPrivilegeValue(string host, string name,
		ref long pluid);

		/// <summary>
		/// The AdjustTokenPrivileges function enables or disables privileges in the specified access token. Enabling or disabling privileges in an access token requires TOKEN_ADJUST_PRIVILEGES access.
		/// </summary>
		/// <param name="htok">[in]            HANDLE            TokenHandle</param>
		/// <param name="disall">[in]            BOOL              DisableAllPrivileges</param>
		/// <param name="newst">[in, optional]  PTOKEN_PRIVILEGES NewState</param>
		/// <param name="len">[in]            DWORD             BufferLength</param>
		/// <param name="prev">[out, optional] PTOKEN_PRIVILEGES PreviousState</param>
		/// <param name="relen">[out, optional] PDWORD            ReturnLength</param>
		/// <returns></returns>
		[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
		ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

		/// <summary>
		/// Logs off the interactive user, shuts down the system, or shuts down and restarts the system. It sends the WM_QUERYENDSESSION message to all applications to determine if they can be terminated.
		/// </summary>
		/// <param name="flg">The shutdown type.</param>
		/// <param name="rea">The reason for initiating the shutdown. This parameter must be one of the system shutdown reason codes.</param>
		/// <returns></returns>
		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool ExitWindowsEx(int flg, int rea);

		/// <summary>
		/// The shutdown type (for ExitWindowsEx WinAPI function).
		/// </summary>
		[Flags]
		internal enum ShutdownFlags : int
		{
			/// <summary>
			/// Shuts down all processes running in the logon session of the process that called the ExitWindowsEx function. Then it logs the user off.
			/// </summary>
			EWX_LOGOFF = 0x00000000,
			/// <summary>
			/// Shuts down the system to a point at which it is safe to turn off the power. All file buffers have been flushed to disk, and all running processes have stopped.
			/// </summary>
			EWX_SHUTDOWN = 0x00000001,
			/// <summary>
			/// Shuts down the system and then restarts the system.
			/// </summary>
			EWX_REBOOT = 0x00000002,
			/// <summary>
			/// This flag has no effect if terminal services is enabled. Otherwise, the system does not send the WM_QUERYENDSESSION message. This can cause applications to lose data. Therefore, you should only use this flag in an emergency.
			/// </summary>
			EWX_FORCE = 0x00000004,
			/// <summary>
			/// Shuts down the system and turns off the power. The system must support the power-off feature.
			/// </summary>
			EWX_POWEROFF = 0x00000008,
			/// <summary>
			/// Forces processes to terminate if they do not respond to the WM_QUERYENDSESSION or WM_ENDSESSION message within the timeout interval. 
			/// </summary>
			EWX_FORCEIFHUNG = 0x00000010,
			/// <summary>
			/// Shuts down the system and then restarts it, as well as any applications that have been registered for restart using the RegisterApplicationRestart function. These application receive the WM_QUERYENDSESSION message with lParam set to the ENDSESSION_CLOSEAPP value. For more information, see Guidelines for Applications.
			/// </summary>
			EWX_RESTARTAPPS = 0x00000040,
			/// <summary>
			/// Beginning with Windows 8:  You can prepare the system for a faster startup by combining the EWX_HYBRID_SHUTDOWN flag with the EWX_SHUTDOWN flag.	
			/// </summary>
			EWX_HYBRID_SHUTDOWN = 0x00400000
		}

		/// <summary>
		/// Log off user session, shut down or reboot Windows
		/// </summary>
		/// <param name="flg">The shutdown type.</param>
		internal static bool DoExitWin(ShutdownFlags flg)
		{
			//https://stackoverflow.com/questions/102567/how-to-shut-down-the-computer-from-c-sharp
			//https://learn.microsoft.com/ru-ru/windows/win32/api/winuser/nf-winuser-exitwindowsex?redirectedfrom=MSDN

			const int SE_PRIVILEGE_ENABLED = 0x00000002;
			const int TOKEN_QUERY = 0x00000008;
			const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
			const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

			bool ok;
			TokPriv1Luid tp;
			IntPtr hproc = GetCurrentProcess();
			IntPtr htok = IntPtr.Zero;
			ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);
			tp.Count = 1;
			tp.Luid = 0;
			tp.Attr = SE_PRIVILEGE_ENABLED;
			ok = LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);
			ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
			ok = ExitWindowsEx((int)flg, 0);
			return ok;
		}


	}
}
