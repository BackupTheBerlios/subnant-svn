#!/bin/sh

# Due to some issues with NAnt <exec> task and svnadmin dump,
# an intermediary script is used with NAnt supplied parameters.
# This dumps faster and overcomes a NAnt issue whereby stdout
# and stderr apprear to get lumped together.

SVNADMIN=$1
REPOS=$2
DUMPFILE=$3
INCREMENTAL=$4
DELTAS=$5

$SVNADMIN dump $REPOS > $DUMPFILE $INCREMENTAL $DELTAS
