@echo off
REM
REM POST-COMMIT HOOK
REM
REM The post-commit hook is invoked after a commit. Subversion runs
REM this hook by invoking a program (script, executable, binary,
REM etc.) named 'post-commit' (for which this file is a template)
REM with the following ordered arguments:
REM
REM   [1] REPOS-PATH   (the path to this repository)
REM   [2] REV          (the number of the revision just committed)
REM
REM The default working directory for the invocation is undefined, so
REM the program should set one explicitly if it cares.
REM
REM Because the commit has already completed and cannot be undone,
REM the exit code of the hook program is ignored.  The hook program
REM can use the 'svnlook' utility to help it examine the
REM newly-committed tree.
REM
REM On a Unix system, the normal procedure is to have 'pre-commit'
REM invoke other programs to do the real work, though it may do the
REM work itself too.
REM
REM Note that 'post-commit' must be executable by the user(s) who will
REM invoke it (typically the user httpd runs as), and that user must
REM have filesystem-level permission to access the repository.
REM
REM See http://subnant.berlios.de for complete list of possible targets

nant /f:%SUBNANT_HOME%/src/subnant.build commit-email -D:repos="%1" -D:rev="%2"
