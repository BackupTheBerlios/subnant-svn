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

nant /f:subnant.build commit-email.build -D:repos="$1" -D:rev="$2"