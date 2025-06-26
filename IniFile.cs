using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace prkiller_ng
{
	/// <summary>
	/// Processor of initialization (*.ini) files
	/// </summary>
	class IniFile
	{
		public string IniPath { get; private set; }
		string ExeName = Assembly.GetExecutingAssembly().GetName().Name;

		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

		/// <summary>
		/// Load an initialization file
		/// </summary>
		/// <param name="IniPath">Initialization file path</param>
		public IniFile(string IniPath = null)
		{
			this.IniPath = new FileInfo(IniPath ?? @".\" + ExeName + ".ini").FullName;
			if (!File.Exists(this.IniPath)) throw new FileNotFoundException("The specified initialization file not found.", IniPath);
		}

		/// <summary>
		/// Read key value from initialization file
		/// </summary>
		/// <param name="Key">Key name</param>
		/// <param name="Section">Section name</param>
		/// <returns>Key value</returns>
		public string Read(string Key, string Section = null)
		{
			var RetVal = new StringBuilder(255);
			GetPrivateProfileString(Section ?? ExeName, Key, "", RetVal, 255, IniPath);
			return RetVal.ToString();
		}
		
		/// <summary>
		/// Read key value from initialization file
		/// </summary>
		/// /// <param name="Default">Default key value</param>
		/// <param name="Key">Key name</param>
		/// <param name="Section">Section name</param>
		/// <returns>Key value</returns>
		public string Read(string Default, string Key, string Section = null)
		{
			if (KeyExists(Key, Section)) return Read(Key, Section);
			return Default;
		}

		/// <summary>
		/// Read key value from initialization file as human-readable string
		/// </summary>
		/// <param name="Key">Key name</param>
		/// <param name="Section">Section name</param>
		/// <returns>Key value (ready for message boxes, labels, etc)</returns>
		public string ReadString(string Key, string Section = null)
		{
			return Read(Key, Section).Replace(@"\r", "").Replace(@"\n", "\n").Replace(@"\t", "\t").ReplaceLineEndings().Trim();
		}

		/// <summary>
		/// Read key value from initialization file as boolean value
		/// </summary>
		/// <param name="Default">Default key value</param>
		/// <param name="Key">Key name</param>
		/// <param name="Section">Section name</param>
		/// <returns>Key value or (if it's unknown) <paramref name="Default"/> value</returns>
		public bool ReadBool(bool Default, string Key, string Section = null)
		{
			if (Default == true) { if (Read(Key, Section).Trim().ToLowerInvariant() == "false") return false; else return true; }
			if (Default == false) { if (Read(Key, Section).Trim().ToLowerInvariant() == "true") return true; else return false; }
			return Default;
		}

		/// <summary>
		/// Read key value from initialization file as integer
		/// </summary>
		/// <param name="Default">Default key value</param>
		/// <param name="Key">Key name</param>
		/// <param name="Section">Section name</param>
		/// <returns>Key value or (if it's unknown) <paramref name="Default"/> value</returns>
		/// <exception cref="FormatException">Incorrect string format</exception>
		/// <exception cref="OverflowException">Integer is out of Int32 size</exception>
		public int ReadInt(int? Default, string Key, string Section = null)
		{
			if (Default == null) return int.Parse(Read(Key, Section));

			int value = Default.GetValueOrDefault();
			int.TryParse(Read(Key, Section), out value);
			return value;
		}

		/// <summary>
		/// Read key value from initialization file as integer
		/// </summary>
		/// <param name="Key">Key name</param>
		/// <param name="Section">Section name</param>
		/// <returns>Key value</returns>
		public int ReadInt(string Key, string Section = null)
		{
			return ReadInt(null, Key, Section);
		}

		/// <summary>
		/// Read key value from initialization file as Enum value
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="Default">Default key value</param>
		/// <param name="Key">Key name</param>
		/// <param name="Section">Section name</param>
		/// <returns>Key value, or (if it's undefined or incorrect) <paramref name="Default"/> value</returns>
		public T ReadEnum<T>(T Default, string Key, string Section = null)
		{
			if (Default != null)
			{
				if (Enum.TryParse(typeof(T), Killer.Config.Read(Key, Section), out object value))
					return (T)value;
				else
					return Default;
			}
			else
			{
				return (T)Enum.Parse(typeof(T), Killer.Config.Read(Key, Section));
			}
		}

		/// <summary>
		/// Read key value from initialization file as Enum value
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="Key">Key name</param>
		/// <param name="Section">Section name</param>
		/// <returns>Key value</returns>
		/// <exception cref="ArgumentException">In case of incorrect value</exception>
		/// <exception cref="ArgumentNullException">In case of null value</exception>
		public T ReadEnum<T>(string Key, string Section = null)
		{
			return (T)Enum.Parse(typeof(T), Killer.Config.Read(Key, Section));
		}

		/// <summary>
		/// Write key value to initialization file
		/// </summary>
		/// <param name="Key">Key name</param>
		/// <param name="Value">Key value</param>
		/// <param name="Section">Section name</param>
		public void Write(string Key, string Value, string Section = null)
		{
			WritePrivateProfileString(Section ?? ExeName, Key, Value, IniPath);
		}

		/// <summary>
		/// Delete the specified <paramref name="Key"/> from initialization file
		/// </summary>
		/// <param name="Key">Key name</param>
		/// <param name="Section">Section name</param>
		public void DeleteKey(string Key, string Section = null)
		{
			Write(Key, null, Section ?? ExeName);
		}

		/// <summary>
		/// Delete entire <paramref name="Section"/> from initialization file
		/// </summary>
		/// <param name="Section">Section name</param>
		public void DeleteSection(string Section = null)
		{
			Write(null, null, Section ?? ExeName);
		}

		/// <summary>
		/// Check is the specified <paramref name="Key"/> exists in specified <paramref name="Section"/> in initialization file
		/// </summary>
		/// <param name="Key">Key name</param>
		/// <param name="Section">Section name</param>
		/// <returns>Does the key exist</returns>
		public bool KeyExists(string Key, string Section = null)
		{
			return Read(Key, Section).Length > 0;
		}
	}
}
