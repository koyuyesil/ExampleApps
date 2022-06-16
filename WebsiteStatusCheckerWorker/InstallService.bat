@echo off
SET path=%~dp0
sc stop NebulaQrService 1>nul && echo DURDU || ECHO ERROR
sc delete NebulaQrService 1>nul && echo SILDI || ECHO ERROR
sc create NebulaQrService binPath="%path%NebulaQrService.exe" 1>nul && echo KURDU || ECHO ERROR
sc start NebulaQrService 1>nul && echo BASLADI || ECHO ERROR
::sc pause NebulaQrService
::sc contiune NebulaQrService
echo DURDUR?
pause
sc stop NebulaQrService 1>nul && echo DURDU || ECHO ERROR
pause
sc stop NebulaQrService 1>nul && echo DURDU || ECHO ERROR
pause