
SubNAnt

Subversion adminstration tasks handled using NAnt on .NET or Mono runtime

$Id$

Thanks to the Subversion scripts used as a basis for SubNAnt:
http://svn.collab.net/viewcvs/svn/trunk/tools/hook-scripts/

Checkout or extract SubNAnt into local filesystem, copy subnant.conf.default
to subnant.conf and configure to your environment, then run subnant.build
as per any normal NAnt build, or from a scheduled task or cron job.

example installation:

  svn checkout svn://svn.berlios.de/subnant/trunk c:\subnant
  cd c:\subnant\conf
  copy subnant.conf.default subnant.conf   [edit subnant.conf]
  cd ..\src
  nant /f:subnant.build test


List of supported tasks, use option -projecthelp for more information

Repository tasks:

  * create.build

    Creates a repository using properties defined in subnant.conf.
    Will also (if asked) copy appropriate hook scripts for your environment,
    as well as standard configuration files (eg. repos/conf/svnserve.conf)


  * verify.build

    Verifies all repositories found directly under <svnroot>, or
    verify single repository if <repos> property is set as parameter.
    An (optional) email is send detailing actions done and time taken.


  * dump.build

    Dumps all repositories found directly under <svnroot>, or
    dump single repository if <repos> property is set as parameter
    An (optional) email is send detailing actions done and time taken.


  * commit-email.build

    Generates Email(s) on post-commit event to addresses defined using
    Subversion property mail:post-commit from specified location in
    repository structure  (see subnant.conf <mail.post-commit> property)


  * commit-control-access.build

	Provides granular access control to a repository for those
	using the ra_svn repository access layer (svnserve)
	

Working copy tasks:

  * bugtraq.build

    Sets bugtraq: properties defined in subnant.conf on a working copy.

    For information about bug tracking integration with Subversion:
    http://tortoisesvn.tigris.org/docs/TortoiseSVN_en/ch04s10.html
