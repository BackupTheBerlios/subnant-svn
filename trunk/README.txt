
SubNAnt - http://subnant.berlios.de

Subversion adminstration tasks handled using NAnt on .NET or Mono runtime

Thanks to the Subversion scripts used as a basis for Subnant:
http://svn.collab.net/viewcvs/svn/trunk/tools/hook-scripts/

$Id$


Installation example:

  // Export or checkout Subnant into local filesystem
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

    Create a repository using properties defined in subnant.conf.
    Will also (optionally) copy appropriate hook scripts for your environment,
    as well as standard configuration files (eg. repos/conf/svnserve.conf)


  * verify

    Verifies all repositories found directly under <svn-root> or
    verify single repository if <repos> property is set as parameter.
    An (optional) email is sent detailing actions and processing time.


  * dump

    Dumps all repositories found directly under <svn-root> or
    dump single repository if <repos> property is set as parameter.
    An (optional) email is sent detailing actions and processing time.


  * commit-email

    Generates Email(s) on post-commit event to addresses defined using
    Subversion property mail:post-commit from specified path in
    repository structure.


  * commit-access

    Provides granular access control to a repository for those
    using the ra_svn repository access layer (svnserve)


Subnant working copy targets:

  * bugtraq

    Sets bugtraq: properties defined in subnant.config.

    For information about bug tracking integration with Subversion:
    http://tortoisesvn.tigris.org/docs/TortoiseSVN_en/ch04s10.html


Other Subnant targets:

  * install

    Creates a wrapper script.


  * config

    Display configuration of subnant.config or supplied file.


  * test

    Tests Subnant by calling targets: create, verify and dump on a temporary
    repository using the configuration defined in subnant.config.



Using hooks:

    // Turn on hooks in subnant.config
    [edit /subnant/conf/subnant.config]

    // Create hook script by cloning example
    cd /subnant/hooks
    copy post-commit.bat.example post-commit.bat

    // Create new repository (or copy into existing repository)
    cd /subnant/src
	subnant create -D:repos=hooktest

