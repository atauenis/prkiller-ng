﻿# Process Killer NG

An lightweight Task Manager replacement for Windows.

It is always resident in memory (when is added to autorun), opens momentally, very lightweight, marks unresponsive processes, displays information about CPU load, and may help you to reanimate even near frozen system.

Original idea of this program is going to *Process Killer 1.4.2* by `__alex` ([http://alex-home-pg.nm.ru/](https://web.archive.org/web/20080112112920/http://alex-home-pg.nm.ru/)). That program is great, but it is entirely abandoned by developer in 2005. So I have decided to made a new application, functionally identical to it, compatible with 64-bit systems.

The New Generation version is written using .NET 6.0, and supports Windows 7/8.1/10/11 and all server versions. You need [Microsoft .NET Desktop Runtime 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) to run PrKillerNG.

Currently the application is under development, so its stability is not guaranteed.

## Usage

Just run `prkiller-ng.exe`. You may put it into system autorun, to don't launch manually every time. To see main window, press `Alt`+`Shift`+`A` keys. By default it won't appear after launch - it's normal.

To hide window, click right mouse button on the KILL button (or press `Esc`). To exit Process Killer NG, press middle mouse button on the KILL button.

Main portion of the window is the list of running processes. Unlike many such applications, last started processes are appearing on the top of the list. If some application is experiencing a freeze, it immediately gets marked by `<!>` icon, so you will easily find it. To finish the unresponsive or unwanted process, just click the KILL button (or press `Del`). No questions will be asked, this differs this application from other.

It is possible to increase/decrease priority class of any process. To change priority, select the process and press `Ctrl`+`↑` to increase or `Ctrl`+`↓` to decrease the process's priority. Also you may select priority using context menu.

Also at the top of the window you can see memory and processor statistics, selected process ID, thread count, and priority. To read more about the selected process, click on the Info button, or press `Ctrl`+`→`. Click on the processor statistics using right button to choose update speed.

The program interface can be localized to any language. Currently only English is available, but other languages will be added in future.

## Configuring

Process Killer NG is portable program. It does not saving anything on your computer. The program's settings are stored in `prkiller-ng.ini` file in same directory with EXE file. At this moment, the settings dialog box is not implemented, so edit the INI file manually if you want.

## Not implememted features
These features, existing in original Process Killer, are planned to be included in the Process Killer NG, but at this moment are not implemented:

  - Suspend and resume process
  - Find parent process
  - Restart process
  - Restart Windows Shell
  - Find of heavy CPU-loading processes (`<*>`)
  - Autosave of settings (including window size)
  - Settings dialog box
  - Run dialog box history
  - Hexadecimal values display
  - Cleanup of tray icons
  - Installer