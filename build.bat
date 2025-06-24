@echo Build script	24.06.2025
@echo.
@echo Clean up directories:
@rmdir bin\Release /S
@rmdir bin\Release-sc /S

@echo.
@echo Building Process Killer NG for Windows 64-bit...
dotnet publish -c Release -r win-x64 --self-contained false -t:CreateZip,Clean
dotnet publish -c Release -r win-x64 --self-contained true -t:CreateZip,Clean

@echo Building Process Killer NG for Windows 32-bit...
dotnet publish -c Release -r win-x86 --self-contained false -t:CreateZip,Clean
dotnet publish -c Release -r win-x86 --self-contained true -t:CreateZip,Clean

@echo.
@echo End of script.