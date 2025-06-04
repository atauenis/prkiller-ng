using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace prkiller_ng
{
	class IniFile
	{
		string IniPath;
		string ExeName = Assembly.GetExecutingAssembly().GetName().Name;

		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

		public IniFile(string IniPath = null)
		{
			this.IniPath = new FileInfo(IniPath ?? @".\" + ExeName + ".ini").FullName;
			if (!File.Exists(this.IniPath)) throw new FileNotFoundException();
		}

		public string Read(string Key, string Section = null)
		{
			var RetVal = new StringBuilder(255);
			GetPrivateProfileString(Section ?? ExeName, Key, "", RetVal, 255, IniPath);
			return RetVal.ToString();
		}

		public void Write(string Key, string Value, string Section = null)
		{
			WritePrivateProfileString(Section ?? ExeName, Key, Value, IniPath);
		}

		public void DeleteKey(string Key, string Section = null)
		{
			Write(Key, null, Section ?? ExeName);
		}

		public void DeleteSection(string Section = null)
		{
			Write(null, null, Section ?? ExeName);
		}

		public bool KeyExists(string Key, string Section = null)
		{
			return Read(Key, Section).Length > 0;
		}
	}
}
