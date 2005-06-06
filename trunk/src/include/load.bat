@echo off

REM Have yet to work out how to direct standard input using NAnt <exec> task

SET SVNADMIN=%1
SET REPOSITORY=%2
SET DUMPFILE=%3
SET REVISION=%4 %5

%SVNADMIN% load %REPOSITORY% < %DUMPFILE% %REVISION%
