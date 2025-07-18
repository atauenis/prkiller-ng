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
}
