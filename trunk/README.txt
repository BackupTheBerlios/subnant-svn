Subnant

Subversion administration using NAnt on .NET or Mono runtime
http://subnant.berlios.de

Thanks to the Subversion scripts used as inspiration
http://svn.collab.net/viewcvs/svn/trunk/tools/hook-scripts/

Licensed under the GNU General Public License
http://www.gnu.org/copyleft/gpl.html

$Id$


Goals:

  * Backup multiple repositiories (hotcopy+verify|dump+compress,email result)

  * Migrate multiple repositories (verify,dump,create,load,verify)

  * Define standard repository configuration (conf,hooks), then make it easy
    to create repositories to standard, automatically included in backups

  * Provide hook-script functionality similar to Subversion's own scripts,
    but without the need for 3rd party tools (Perl,Python,sendmail,etc)


Pre-requisites:

  * .NET 1.1+ or Mono 1.1.5+ runtime

  * NAnt and NAntContrib 0.85rc2+

  * Subversion 1.1.3+


Windows Installation:

  // Export, checkout or extract Subnant into local filesystem
  svn export svn://svn.berlios.de/subnant/trunk "C:\Program Files\Subnant"

  // Use NAnt to install subnant wrapper script
  cd "C:\Program Files\Subnant\src"
  nant install


Linux Installation:

  // Export, checkout or extract Subnant into local filesystem
  svn export svn://svn.berlios.de/subnant/trunk /usr/local/subnant

  // Use NAnt to install subnant wrapper script
  cd /usr/local/subnant/src
  nant install


Setup:

  // Create config file by cloning example
  cd ../conf
  copy subnant.config.example subnant.config
  [edit subnant.config]

  // Display current configuration
  subnant config


Run:

  // Help for each main target is available
  subnant -projecthelp
  subnant help
  subnant help backup
  
  // Run from console or create scheduled task or cron job
  subnant test
  subnant backup -D:sendmail=true
  subnant create verify dump -D:repos=repo1,repo2
  subannt migrate -D:to-svn-root=/svn2root/repos -D:to-svn-bindir=/svn2/bin

  // Something went wrong?  Run with -debug switch for more info
  subnant test -debug


Repository targets:

  * backup

    Backup some or all repositories under svn-root using methods:

      hotcopy   : verbatim copy of repository
      dump      : dump all revisions into portable format
      increment : dump all revisions since last increment


  * create

    Create one or more repositories using configuration in subnant.config
    and setup hook scripts and configuration files if defined


  * dump

    Dump and compress some or all repositories under svn-root


  * load

    Uncompress and load some or all repositories from svn-dumps to svn-root


  * migrate

    Migrate one or more repositories by chaining Subnant targets:
    backup -> create -> load -> verify

    Allows for different Subversion binary version to be used on destination
    repository targets (create, load and verify)


  * verify

    Verify some or all repositories under svn-root


Repository hook targets:

  * commit-access

    Provides granular access control to a repository suitable for those
    using the ra_svn repository access layer (svnserve)


  * commit-email

    Generates email on post-commit event to addresses defined using
    Subversion property mail:post-commit


Working copy targets:

  * bugtraq

    Sets bugtraq properties defined in subnant.config

    For information about bug tracking integration with Subversion:
    http://tortoisesvn.tigris.org/docs/TortoiseSVN_en/ch04s10.html


Other targets:

  * config

    Display configuration of subnant.config


  * install

    Create subnant wrapper script


  * test

    Tests Subnant using the configuration in subnant.config


Using hook targets:

    // Create hook scripts by cloning example
    cd subnant/hooks
    copy post-commit.bat.example post-commit.bat

    // Run test to check post-commit email is being sent
    subnant test

    // Create new repository (or copy hook script into repos/conf)
    subnant create -D:repos=hooktest
