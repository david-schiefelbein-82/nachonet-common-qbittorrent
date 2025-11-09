@setlocal
powershell -file .\update-version.ps1 -version "increment"
@set /p VER=<version.txt
pushd Nachonet.Common.Qbittorrent
dotnet pack /p:AssemblyVersion=%VER% /p:Version=%VER%

move ".\bin\Release\Nachonet.Common.Qbittorrent.%VER%.nupkg" "%DEV_PACKAGE_DIR%"
popd
@endlocal