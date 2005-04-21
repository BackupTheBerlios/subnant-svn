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

  * .NET runtime >= 1.1 or Mono runtime >= 1.1.5

  * NAnt and NAntContrib >= 0.85rc2

  * Subversion >= 1.1.3


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
  subnant migrate -D:to-svn-root=/svn2root/repos -D:to-svn-bindir=/svn2/bin

  // Something went wrong?  Run with -debug switch for more info
  subnant test -debug


Repository targets:

  * backup

    Backup some or all repositories under svn-root using methods:

      hotcopy     : verbatim copy of repository into hotcopy-root
      dump        : dump all revisions into dump-root using portable format 
      incremental : dump younger revisions after highest revision in dump-root


  * create

    Create one or more repositories using configuration in subnant.config
    and setup hook scripts and configuration files if defined


  * dump

    Dump and compress some or all repositories under svn-root


  * load

    Uncompress and load some or all repositories from svn-dumps to svn-root


  * migrate

    Migrate one or more repositories by chaining Subnant targets:
    verify -> dump -> create -> load -> verify

    Allows for different Subversion binary version to be used on destination
    repository targets (create, load and verify)


  * verify

    Verify some or all repositories under svn-root


Repository hook targets:

  * commit-access

    Provides granular access control to a repository using pre-commit hook.


  * commit-allower

    Provides user-level access control to a repository using start-commit hook.


  * commit-email

    Sends email on post-commit hook using Subversion property hook:commit-email
    on parent directory(s) of committed files.  Shows who, why, what, when and
    where changes were made for a revision.


  * propchange-access

    Provides granular access control to repository property changes using
    pre-revprop-change hook.


  * propchange-email

    Sends email on post-revprop-change hook using Subversion property
    hook:propchange-email on parent directory(s) of committed files.
    Shows who, why, what, when and where property changes were made.


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
    copy post-propchange.bat.example post-propchange.bat
    [edit if necessary to suit your setup]

    // Run test to check hooks are working and email is being sent
    subnant test

    // Create new repository (or copy hook script into repos/conf)
    // any hook scripts in subnant/conf are copied into repository
    subnant create -D:repos=hooktest
