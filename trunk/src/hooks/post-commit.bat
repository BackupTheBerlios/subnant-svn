@echo off

REM  POST-COMMIT HOOK

REM  The post-commit hook is invoked after a commit. Subversion runs
REM  this hook by invoking a program (script, executable, binary,
REM  etc.) named 'post-commit' (for which this file is a template)
REM  with the following ordered arguments:

REM    [1] REPOS-PATH   (the path to this repository)
REM    [2] REV          (the number of the revision just committed)

nant /f:subnant.build commit-email.build -D:repos="%1" -D:rev="%2"
