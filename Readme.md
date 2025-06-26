# Process Killer NG

An lightweight Task Manager replacement for Windows.

It is always resident in memory (when is added to autorun), opens immediately, very lightweight, marks unresponsive processes, displays information about CPU load, and may help you to reanimate even near frozen system.

Original idea of this program is inspired by *Process Killer 1.4.2* by `__alex` ([http://alex-home-pg.nm.ru/](https://web.archive.org/web/20080112112920/http://alex-home-pg.nm.ru/)). That program is great, but it is entirely abandoned by developer in 2005. So I have decided to made a new application, functionally identical to it, compatible with 64-bit systems.

The New Generation version supports Windows 7/8.1/10/11 and all server versions. You will need [Microsoft .NET Desktop Runtime 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) to run PrKillerNG.

Currently the application is under development, so its stability is not guaranteed.

## Usage

Just run `prkiller-ng.exe`. You may put it into system autorun, to don't launch manually every time. To see main window, press `Ctrl`+`Shift`+`1` keys. By default it won't appear after launch - it's normal.

To hide window, click right mouse button on the KILL button (or press `Esc`). To exit Process Killer NG, press middle mouse button on the KILL button.

Main portion of the window is the list of running processes. Unlike many such applications, last started processes are appearing on the top of the list. If some application is experiencing a freeze, it immediately gets marked by `<!>` icon, so you will easily find it. To finish the unresponsive or unwanted process, just click the KILL button (or press `Del`). No questions will be asked, this differs this application from other.

It is possible to increase/decrease priority class of any process. To change priority, select the process and press `Ctrl`+`↑` to increase or `Ctrl`+`↓` to decrease the process's priority. Also you may select priority using context menu. If necessary, you can even suspend and later resume an running process (`Ctrl`+`←`). This may be useful, say, when copying a large file over network using FAR Manager, and you are need to temporarily free network bandwidth for other task. Suspended processes are marked by `<s>` icon.

Also at the top of the window you can see memory and processor statistics, selected process ID, thread count, and priority. To read more about the selected process, click on the Info button, or press `Ctrl`+`→`. Parent process which launched the selected process sometimes can be looked up by pressing `Ctrl`+`PgUp`. Click on the processor statistics using right button to choose update speed.

The program interface can be localized to any language. Currently only English is available, but other languages will be added in future.

## Configuring

Process Killer NG is portable program. It does not saving anything on your computer. The program's settings are stored in `prkiller-ng.ini` file in same directory with EXE file. 

To save main window size and position, open the Settings dialog box and click OK to save settings. This differs NG version from original Process Killer 1.4.2, which saved all without prompts.

You may also edit the INI file manually if want.

  - CpuGraphStyle - Disable, Bar, Graph or Label
  - DoubleClick - Disable, ProcessInfo or Kill
  - RightClick - Disable or ContextMenu
  - Hotkey buttons - [keyboard layout](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.keys?view=windowsdesktop-6.0)
  - StartupPriority - Normal, Idle, High, RealTime, BelowNormal or AboveNormal
  - Selfkill, KillTree, KillSystem - Disable, Prompt or Enable
  - ErrorSound - Disable, Beep, SpeakerBeep, Asterisk, Exclamination, Hand or Question
  - Other - integer, string or boolean values

## Not implemented features
These features, existing in original Process Killer, are planned to be included in the Process Killer NG, but at this moment are not implemented:

  - Restart Windows Shell
  - Find of heavy CPU-loading processes (`<*>`)
  - Run dialog box history
  - Hexadecimal values display
  - Cleanup of tray icons
  - Installer