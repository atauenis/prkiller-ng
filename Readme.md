# Process Killer NG

An lightweight Task Manager replacement for Windows.

It is always resident in memory (when is added to autorun), opens immediately, very lightweight, marks unresponsive processes, displays information about CPU load, and may help you to reanimate even near frozen system.

Original idea of this program is inspired by *Process Killer 1.4.2* by `__alex` ([http://alex-home-pg.nm.ru/](https://web.archive.org/web/20080112112920/http://alex-home-pg.nm.ru/)). That program is great, but it is entirely abandoned by developer in 2005. So I have decided to made a new application, functionally identical to it, compatible with 64-bit systems.

The New Generation version supports Windows 7/8.1/10/11 and all server counterpart versions. You will need [Microsoft .NET Desktop Runtime 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) to run PrKillerNG.

Report about found bugs at [GitHub](https://github.com/atauenis/prkiller-ng). Also you can find here source code of the application, including latest pre-release versions.

![](https://github.com/user-attachments/assets/5eaf0dc3-0ab0-47f0-9c95-d4797b2317d9)


## Usage

Just run `prkiller-ng.exe`. You may put it into system autorun, to don't launch manually every time. To see main window, press `Ctrl`+`Shift`+`1` keys. By default it won't appear after launch - it's normal.

To hide window, click right mouse button on the KILL button (or press `Esc`). To exit Process Killer NG, press middle mouse button on the KILL button.

Main portion of the window is the list of running processes. Unlike many such applications, last started processes are appearing on the top of the list. If some application is experiencing a freeze, it immediately gets marked by `<!>` icon, so you will easily find it. To finish the unresponsive or unwanted process, just click the KILL button (or press `Del`). No questions will be asked, this differs this application from other.

It is possible to increase/decrease priority class of any process. To change priority, select the process and press `Ctrl`+`↑` to increase or `Ctrl`+`↓` to decrease the process's priority. Also you may select priority using context menu. If necessary, you can even suspend and later resume an running process (`Ctrl`+`←`). This may be useful, say, when copying a large file over network using FAR Manager, and you are need to temporarily free network bandwidth for other task. Suspended processes are marked by `<s>` icon.

Also at the top of the window you can see memory and processor statistics, selected process ID, thread count, and priority. To read more about the selected process, click on the Info button, or press `Ctrl`+`→`. Parent process which launched the selected process sometimes can be looked up by pressing `Ctrl`+`PgUp`. Click on the processor statistics using right button to choose update speed.

Sometimes it is necessary to restart Windows shell process (`explorer.exe`). Say, after manual tweaking its settings in registry, or if it became unstable. So you may press "Restart shell" button (or `Ctrl`+`S`). This will close all Explorer processes, and then it gets restarted as just after logon. Click on "Run" button (`Ctrl`+`R`) to launch any other program (identical to `Win`+`R`).

The program supports changing user interface language. Out of the default package there are 2 languages bundled: English and Russian.

**Tip**: consider run Process Killer NG with Administrator rights. This will allow reading information about all processes, including system, and killing them. 

To add the application to system autorun with Administrator rights, set _Autorun at logon=Disabled_ in Process Killer NG settings, then open Command Prompt with Administrator rights, and execute the command (with correct path to `prkiller-ng.exe` file): 

```
schtasks.exe /create  /RL HIGHEST /SC ONLOGON /TN "PrKiller-NG" /TR "\"C:\Program Files\PrKiller-NG\PrKiller-NG.exe\""
```

If you want to remove Process Killer NG from autorun, use Windows Task Scheduler (`taskschd.msc`).

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

## Differences from version 1.4.2 by \_\_alex:

* Supports only Windows 7 and up (32-bit, 64-bit). Optimized for Windows 8.1 (64-bit) and up.
* No support for Windows 98/XP/Vista. SP1 is required to run on Windows 7 or Windows Server 2008 R2.
* Default hot key is `Ctrl`+`Shift`+`1` , however you may set up `Ctrl`+`Shift`+`~` via Settings window.
* You may set up a confirmation for "Restart Shell" button action.
* The "Restart Shell" button is always killing the shell process, and there is no option to disable this (it were actual only on Windows 9x).
* Unlimited history in Run dialog box. `Ctrl`+`[OK]` in Run dialog is starting an application without save to history.
* Multilanguage support, new configuration file syntax, a bit different dialog boxes.
* Total threads count does not displaying in title bar.
* Currently does not displaying processes with big CPU load (the `<*>` icon).
* Currently does not removing "dead" icons from taskbar notification area after killing a process with such icons. Just point over them mouse, and they will disappear.
* Currently can't display data in hexadecimal mode.
* Currently have no installer.
* Probably some other changes, as the NG version is written from scratch by other human.

Also other new features are planned to future updates of the application.
