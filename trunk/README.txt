Subnant

Subversion administration using NAnt on .NET or Mono runtime
http://subnant.berlios.de

Thanks to the Subversion scripts used as inspiration
http://svn.collab.net/viewcvs/svn/trunk/tools/hook-scripts/

Licensed under the GNU General Public License
http://www.gnu.org/copyleft/gpl.html

$Id$


Goals:

  * Backup multiple repositiories (hotcopy+verify|dump+compress->email result)

  * Migrate multiple repositories (verify->dump->create->load->verify)

  * Provide hook-script functionality similar to Subversion's own scripts,
    but without the need for third party tools (Perl,Python,sendmail,etc)

  * Define standard repository configuration (conf,hooks), then make it easy
    to create repositories to standard, automatically included in backups and
    migrations as they are created under a standard svn-root directory


Prerequisites:

  * .NET runtime >= 1.1 or Mono runtime >= 1.1.8

  * NAnt and NAntContrib >= 0.85rc4

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

  // Project help and help for each main target is available
  subnant -projecthelp
  subnant help
  subnant help hotcopy
  
  // Run from console or create scheduled task or cron job
  subnant test
  subnant hotcopy -D:sendmail=true
  subnant create verify dump -D:repos=repo1,repo2
  subnant migrate -D:to-svn-root=/svn2root/repos -D:to-svn-bindir=/svn2/bin

  // Something went wrong?  Run with -debug switch for more info
  subnant test -debug


Repository targets:

  * create

    Create one or more repositories under svn-root using configuration in
    subnant.config, optionally copying hook scripts and configuration files
    from subnant/conf


  * dump

    Dump and compress some or all repositories in svn-root into dump-root


  * hotcopy

    Hotcopy and verify some or all repositories in svn-root into hotcopy-root


  * load

    Uncompress and load repositories from dump-root to svn-root


  * migrate

    Migrate some or all repositories by chaining Subnant targets:
    verify -> dump -> create -> load -> verify

    Allows for different Subversion binary version to be used on destination
    repository targets (create, load and verify)


  * upgrade-bdb

    Upgrade bdb style repositories


  * verify

    Verify some or all repositories in svn-root


Repository hook targets:

  * commit-access

    Provides granular access control to a repository using pre-commit hook in
    conjunction with Subversion properties

    For more information:
    http://svn.berlios.de/viewcvs/*checkout*/subnant/trunk/doc/commit-access.html


  * commit-allower

    Provides user-level access control to a repository using start-commit hook


  * commit-email

    Sends email using post-commit hook by reading Subversion hook:commit-email
    property on parent directory(s) of committed files  
    
    Can show who, why, what, when and where revision changes were made

    For more information:
    http://svn.berlios.de/viewcvs/*checkout*/subnant/trunk/doc/commit-email.html


  * commit-message

    Ensures log message is entered by using pre-commit hook


  * revprop-change-access

    Provides granular access control to revision property changes using
    pre-revprop-change hook in conjunction with Subversion properties

    For more information:
    http://svn.berlios.de/viewcvs/*checkout*/subnant/trunk/doc/revprop-change-access.html  


  * revprop-change-email

    Sends email on post-revprop-change hook using Subversion property
    hook:revprop-change-email on parent directory(s) of committed files

    Can show who, why, what and when revision property changes were made

    For more information:
    http://svn.berlios.de/viewcvs/*checkout*/subnant/trunk/doc/revprop-change-email.html  


Working copy targets:

  * bugtraq

    Sets bugtraq properties defined in subnant.config

    For information about bug tracking integration with Subversion and clients:
    http://tortoisesvn.tigris.org/docs/TortoiseSVN_en/ch04s10.html


Other targets:

  * config

    Display configuration of subnant.config


  * install

    Create subnant wrapper script


  * test

    Tests Subnant targets in a test environment


Using hook targets:

    // Create hook scripts by cloning example
    cd subnant/hooks
    copy pre-commit.bat.example pre-commit.bat
    copy post-commit.bat.example post-commit.bat
    [edit if necessary to suit your setup]

    // Run test to check hooks are working and email is being sent
    subnant test -D:target=commit-email,commit-message

    // Create new repository (or copy hook scripts into repos/conf)
    // any hook scripts in subnant/conf are copied into repository
    subnant create -D:repos=hooktest
