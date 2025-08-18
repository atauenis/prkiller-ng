using System.Collections.Generic;

namespace prkiller_ng
{
	/// <summary>
	/// Common parts of Process Killer
	/// </summary>
	internal static class Killer
	{
		internal static IniFile Config;
		internal static IniFile Language;

		internal static Dictionary<int, ProcessInfo> ProcessCache = new();

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
			FindParent, Restart, SuspendResumeProcess, SuspendProcess, ResumeProcess,
			RestartExplorer
		}
	}
}
