
Subnant

Subversion administration using NAnt on .NET or Mono
http://subnant.berlios.de

Thanks to the Subversion scripts used as inspiration
http://svn.collab.net/viewcvs/svn/trunk/tools/hook-scripts/

Licensed under the GNU General Public License
http://www.gnu.org/copyleft/gpl.html

$Id$


Installation example:

  // Export, checkout or extract Subnant into local filesystem
  svn export svn://svn.berlios.de/subnant/trunk /subnant

  // Use NAnt to install 'subnant' wrapper script to execute:
  // nant -quiet -nologo -buildfile:"/subnant/src/subnant.build"
  cd /subnant/src
  nant install

  // Create config file by cloning example
  cd /subnant/conf
  copy subnant.config.example subnant.config
  [edit subnant.config]

  // Run from console or create scheduled task or cron job
  subnant -projecthelp
  subnant config
  subnant help test
  subnant test
  subnant verify dump -D:repos=repo1,repo2


Repository targets:

  * create

    Create one or more repositories using configuration in subnant.config
    and (optionally) setup hook scripts and configuration files


  * verify

    Verify some or all repositories under 'svn-root' with (optional)
    email sent detailing result and processing time


  * dump

    Dumps some or all repositories under 'svn-root' with (optional)
    compression and email sent detailing result and processing time.


  * load

    Loads some or all repositories from 'svn-dumps' to 'svn-root'


  * copy

    Copy one or more repositories from one location to another using
    Subnant targets: verify, dump, create, load and verify


Repository hook targets:

  * commit-email

    Generates email on post-commit event to addresses defined using
    Subversion property mail:post-commit from pre-defined path in
    repository structure


  * commit-access

    Provides granular access control to a repository for those
    using the ra_svn repository access layer (svnserve)


Working copy targets:

  * bugtraq

    Sets bugtraq: properties defined in subnant.config

    For information about bug tracking integration with Subversion:
    http://tortoisesvn.tigris.org/docs/TortoiseSVN_en/ch04s10.html


Other targets:

  * install

    Creates a 'subnant' wrapper script


  * config

    Display configuration of subnant.config


  * test

    Tests Subnant using the configuration in subnant.config



Using hook targets:

    // Turn on hooks in subnant.config
    [edit /subnant/conf/subnant.config]

    // Create hook script by cloning example
    cd /subnant/hooks
    copy post-commit.bat.example post-commit.bat

    // Create new repository (or copy hook script into repos/conf)
    subnant create -D:repos=hooktest
