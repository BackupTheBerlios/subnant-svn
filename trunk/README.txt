
SubNAnt - http://subnant.berlios.de

Subversion adminstration tasks handled using NAnt on .NET or Mono runtime

Thanks to the Subversion scripts used as inspiration:
http://svn.collab.net/viewcvs/svn/trunk/tools/hook-scripts/

$Id$


Installation example:

  // Export, checkout or extract Subnant into local filesystem
  svn export svn://svn.berlios.de/subnant/trunk /subnant

  // Create subnant shortcut
  cd /subnant/src
  nant install

  // Create config file by cloning example
  cd /subnant/conf
  copy subnant.config.example subnant.config
  [edit subnant.config]

  // Run as NAnt build (or create scheduled task/cron job)
  subnant -projecthelp
  subnant config


Subnant repository targets:

  * create

    Create one or more repositories using configuration in subnant.config,
    and (optionally) setup hook scripts and configuration files.


  * verify

    Verify some or all repositories under <svn-root> with (optional)
    email sent detailing result and processing time.


  * dump

    Dumps some or all repositories under <svn-root> with (optional)
    email sent detailing result and processing time.


  * commit-email

    Generates email on post-commit event to addresses defined using
    Subversion property mail:post-commit from pre-defined path in
    repository structure.


  * commit-access

    Provides granular access control to a repository for those
    using the ra_svn repository access layer (svnserve).


Subnant working copy targets:

  * bugtraq

    Sets bugtraq: properties defined in subnant.config.

    For information about bug tracking integration with Subversion:
    http://tortoisesvn.tigris.org/docs/TortoiseSVN_en/ch04s10.html


Other Subnant targets:

  * install

    Creates a wrapper script.


  * config

    Display subnant.config configuration.


  * test

    Tests Subnant using the configuration in subnant.config.



Using hooks:

    // Turn on hooks in subnant.config
    [edit /subnant/conf/subnant.config]

    // Create hook script by cloning example
    cd /subnant/hooks
    copy post-commit.bat.example post-commit.bat

    // Create new repository (or copy into existing repository)
    cd /subnant/src
	subnant create -D:repos=hooktest
