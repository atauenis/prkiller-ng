﻿using System;
using System.Diagnostics;
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
		/// Find if the process is running under WOW64.
		/// </summary>
		/// <param name="process">The process.</param>
		/// <returns><c>true</c> if the process is running under WOW64 on 64-bit OS.</returns>
		internal static bool IsUnderWow64(this Process process)
		{
			if (IntPtr.Size != 8) return false; // the PrKiller-NG is running on a 32-bit OS

			if ((Environment.OSVersion.Version.Major > 5)
				|| ((Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor >= 1)))
			{
				bool retVal;
				return NativeMethods.IsWow64Process(process.Handle, out retVal) && retVal;
			}
			return false; // not on 64-bit Windows Emulator
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
	}

	internal static class NativeMethods
	{
		[DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool IsWow64Process([In] IntPtr process, [Out] out bool wow64Process);
	}
}
