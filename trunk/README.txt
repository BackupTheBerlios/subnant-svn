Subnant

Subversion administration using NAnt on .NET or Mono runtime
http://subnant.berlios.de

Thanks to the Subversion scripts used as inspiration
http://svn.collab.net/viewcvs/svn/trunk/tools/hook-scripts/

Licensed under the GNU General Public License
http://www.gnu.org/copyleft/gpl.html

$Id$

All installations currently require NAnt and NAntContrib installed along
with the .NET or Mono runtime.  Tested on NAnt and NAntContrib 0.85rc2
on .NET 1.1 and Mono 1.1.4 under Windows XP SP1 and SuSE 9.2


Windows Installation:

  // Export, checkout or extract Subnant into local filesystem
  svn export svn://svn.berlios.de/subnant/trunk "C:\Program Files\Subnant"

  // Use NAnt to install subnant wrapper script
  cd "C:\Program Files\Subnant\src"
  nant install


Linux Installation:

  // Export, checkout or extract Subnant into local filesystem
  svn export svn://svn.berlios.de/subnant/trunk /usr/local/share/subnant

  // Use NAnt to install subnant wrapper script
  cd /usr/local/share/subnant/src
  nant install


Common setup:

  // Create config file by cloning example
  cd ../conf
  copy subnant.config.example subnant.config
  [edit subnant.config]

  // Run from console or create scheduled task or cron job
  subnant -projecthelp
  subnant config
  subnant help test
  subnant test
  subnant verify dump -D:repos=repo1,repo2 -D:sendmail=true


Repository targets:

  * create

    Create one or more repositories using configuration in subnant.config and
    (optionally) setup hook scripts and configuration files


  * verify

    Verify some or all repositories under svn-root


  * dump

    Dump some or all repositories under svn-root and (optionally) compress


  * load

    Loads some or all repositories from svn-dumps to svn-root


  * copy

    Copy one or more repositories by chaining Subnant targets: verify, dump, create,
    load and verify.  Allows different Subversion binaries on destination repository


Repository hook targets:

  * commit-email

    Generates email on post-commit event to addresses defined using Subversion
    property mail:post-commit


  * commit-access

    Provides granular access control to a repository suitable for those using the
    ra_svn repository access layer (svnserve)


Working copy targets:

  * bugtraq

    Sets bugtraq properties defined in subnant.config

    For information about bug tracking integration with Subversion:
    http://tortoisesvn.tigris.org/docs/TortoiseSVN_en/ch04s10.html


Other targets:

  * install

    Create subnant wrapper script


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
