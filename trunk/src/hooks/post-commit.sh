#!/bin/sh

# POST-COMMIT HOOK
#
# The post-commit hook is invoked after a commit. Subversion runs
# this hook by invoking a program (script, executable, binary,
# etc.) named 'post-commit' (for which this file is a template)
# with the following ordered arguments:
#
#   [1] REPOS-PATH   (the path to this repository)
#   [2] REV          (the number of the revision just committed)
#
# The default working directory for the invocation is undefined, so
# the program should set one explicitly if it cares.
#
# Because the commit has already completed and cannot be undone,
# the exit code of the hook program is ignored.  The hook program
# can use the 'svnlook' utility to help it examine the
# newly-committed tree.
#
# On a Unix system, the normal procedure is to have 'pre-commit'
# invoke other programs to do the real work, though it may do the
# work itself too.
#
# Note that 'post-commit' must be executable by the user(s) who will
# invoke it (typically the user httpd runs as), and that user must
# have filesystem-level permission to access the repository.

# See http://subnant.berlios.de for complete list of possible targets

nant /f:${SUBNANT_HOME}/src/subnant.build commit-email -D:repos="$1" -D:rev="$2"