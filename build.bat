@echo Build script	10.06.2025
@echo.
@echo Clean up directories:
@rmdir bin\Release /S

@echo.
@echo Building Process Killer NG for Windows 64-bit...
@rem dotnet publish -c Release -r win-x64 --self-contained true
dotnet publish -c Release -r win-x64 --self-contained false

@echo.
@echo End of script.