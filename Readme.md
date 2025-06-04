# Process Killer NG

An lightweight Task Manager replacement for Windows.

It is always resident in memory (when is added to autorun), opens momentally, very lightweight, marks unresponsive processes, displays information about CPU load, and may help you to reanimate even near frozen system.

Original idea of this program is going to *Process Killer 1.4.2* by `__alex` ([http://alex-home-pg.nm.ru/](https://web.archive.org/web/20080112112920/http://alex-home-pg.nm.ru/)). That program is great, but it is entirely abandoned by developer in 2005. So I have decided to made a new application, functionally identical to it, compatible with 64-bit systems.

The New Generation version is written using .NET 6.0, and supports Windows 7/8.1/10/11 and all server versions. You need [Microsoft .NET Desktop Runtime 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) to run PrKillerNG.

Currently the application is under development, so its stability is not guaranteed.

## Usage

Just run `prkiller-ng.exe`. You may put it into system autorun, to don't launch manually every time. To see main window, press `Alt`+`Shift`+`A` keys. By default it won't appear after launch - it's normal.

To hide window, click right mouse button on the KILL button (or press `Esc`). To exit Process Killer NG, press middle mouse button on the KILL button.

Main portion of the window is the list of running processes. Unlike many such applications, last started processes are appearing on the top of the list. If some application is experiencing a freeze, it immediately gets marked by `<!>` icon, so you will easily find it. To finish the unresponsive or unwanted process, just click the KILL button (or press `Del`). No questions will be asked, this differs this application from other.