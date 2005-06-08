#!/bin/sh

# Have yet to work out how to direct standard input using NAnt <exec> task

SVNADMIN=$1
REPOSITORY=$2
DUMPFILE=$3
REVISION=$4 $5

$SVNADMIN load $REPOSITORY < $DUMPFILE $REVISION
