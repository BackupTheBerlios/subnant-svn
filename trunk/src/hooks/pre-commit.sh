#!/bin/sh

# PRE-COMMIT HOOK
#
# The pre-commit hook is invoked before a Subversion txn is
# committed.  Subversion runs this hook by invoking a program
# (script, executable, binary, etc.) named 'pre-commit' (for which
# this file is a template), with the following ordered arguments:
#
#   [1] REPOS-PATH   (the path to this repository)
#   [2] TXN-NAME     (the name of the txn about to be committed)
#
# The default working directory for the invocation is undefined, so
# the program should set one explicitly if it cares.
#
# If the hook program exits with success, the txn is committed; but
# if it exits with failure (non-zero), the txn is aborted, no commit
# takes place, and STDERR is returned to the client.   The hook
# program can use the 'svnlook' utility to help it examine the txn.
#
# On a Unix system, the normal procedure is to have 'pre-commit'
# invoke other programs to do the real work, though it may do the
# work itself too.
#
#   ***  NOTE: THE HOOK PROGRAM MUST NOT MODIFY THE TXN, EXCEPT  ***
#   ***  FOR REVISION PROPERTIES (like svn:log or svn:author).   ***
#
#   This is why we recommend using the read-only 'svnlook' utility.
#   In the future, Subversion may enforce the rule that pre-commit
#   hooks should not modify the versioned data in txns, or else come
#   up with a mechanism to make it safe to do so (by informing the
#   committing client of the changes).  However, right now neither
#   mechanism is implemented, so hook writers just have to be careful.
#
# Note that 'pre-commit' must be executable by the user(s) who will
# invoke it (typically the user httpd runs as), and that user must
# have filesystem-level permission to access the repository.
#
# See http://subnant.berlios.de for complete list of possible targets

nant /f:"${SUBNANT_HOME}/src/subnant.build" commit-access -D:repos="$1" -D:txn="$2"