@echo off

REM Due to some issues with NAnt <exec> task and svnadmin dump,
REM an intermediary script is used with NAnt supplied parameters.
REM This dumps faster and overcomes a NAnt issue whereby stdout
REM and stderr apprear to get lumped together.

SET SVNADMIN=%1
SET REPOS=%2
SET DUMPFILE=%3
SET INCREMENTAL=%4
SET DELTAS=%5

%SVNADMIN% dump %REPOS% > %DUMPFILE% %INCREMENTAL% %DELTAS%
