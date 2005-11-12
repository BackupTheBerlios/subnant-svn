
// Copyright (C) 2005 Simon McKenna
//
// Licensed under the GNU General Public License
// http://www.gnu.org/copyleft/gpl.html
//
// $Id: custom-tasks.include 182 2005-07-19 11:03:14Z sshnug_si $ 

using System;
using System.Collections;
using System.IO;

using NAnt.Core;
using NAnt.Core.Attributes;

namespace Subnant.Functions
{
    /// <summary>
    /// Custom functions required by Subnant
    /// </summary>
    [FunctionSet("subnant", "Subnant")]
    public class SubnantFunctions : FunctionSetBase
    {
        #region Public Instance Constructors

        public SubnantFunctions(Project project, PropertyDictionary properties) : base(project, properties)
        {
        }

        #endregion Public Instance Constructors

        #region Public Instance Methods

	    /// <summary>
	    /// Determines whether to call help or default subnant target
	    /// </summary>
	    [Function("get-target")]
	    public string GetTarget()
	    {
	        if (Project.BuildTargets.Contains("help"))
	        {
	            return "help";
	        }
	        else
	        {
	            return Project.CurrentTarget.Name;
	        }
	    }
	
	    /// <summary>
	    /// Return count of build targets, as help should display subnant usage
	    /// if there is only one build target, rather than main targets help
	    /// </summary>
	    [Function("get-targets-count")]
	    public int GetTargetsCount()
	    {
	        return Project.BuildTargets.Count;
	    }
	
	    /// <summary>
	    /// Display list of build targets   
	    /// </summary>
	    [Function("get-build-targets")]
	    public string GetBuildTargets()
	    {
	        String targets = String.Empty;
	        System.Collections.Specialized.StringEnumerator targetEnumerator = Project.BuildTargets.GetEnumerator();
	
	        while (targetEnumerator.MoveNext())
	        {   
	            targets += targetEnumerator.Current+", ";
	        }   
	
	        if (targets.EndsWith(", "))
	        {   
	            targets = targets.Substring(0, targets.Length-2);
	        }   
	
	        return targets;
	    }
	
	    /// <summary>
	    /// NAnt path::get-directory-name() returns full path, sometimes we just want directory name
	    /// </summary>
	    [Function("return-directory-name")]
	    public string ReturnDirectoryName(string path)
	    {
	        DirectoryInfo directory = new DirectoryInfo(path);
	
	        if (directory == null)
	        {
	            return String.Empty;
	        }
	        else
	        {
	            return directory.Name;
	        }
	    }
	
	    /// <summary>
	    /// Display DateTime string from ticks
	    /// </summary>
	    /// <param name="ticks">DateTime to convert</param>
	    [Function("ticks-to-string")]
	    public string GetDateTimeFromTicks(Int64 ticks)
	    {
	        DateTime result = new DateTime(ticks);
	        return result.ToString();
	    }
	
	    /// <summary>
	    /// Subversion somtimes wants an Uri instead of path   
	    /// </summary>
	    /// <param name="path">Path to convert</param>
	    [Function("to-uri")]
	    public string PathToUriString(string path)
	    {
	        Uri pathToUri = new Uri(path);
	        return pathToUri.ToString();
	    }
	
	    /// <summary>
	    /// Display elapsed time using ticks for accuracy
	    /// </summary>
	    /// <param name="ticks">DateTime in ticks of end period</param>
	    /// <param name="showText">Option to display time format, or show in seconds with no text</param>
	    [Function("format-elapsed-time")]
	    public string FormatElapsedTime(long ticks, bool showText)
	    {
	        TimeSpan elapsed = TimeSpan.FromTicks(DateTime.Now.Ticks - ticks);
	
	        if (!showText || (elapsed.TotalMinutes < 5))
	        {
	            return String.Format("{0:F2}", elapsed.TotalSeconds);
	        }
	        else if (elapsed.TotalHours < 2)
	        {
	            return String.Format("{0:F2} minutes", elapsed.TotalMinutes);
	        }
	        else
	        {
	            return String.Format("{0:F2} hours", elapsed.TotalHours);
	        }
	    }
	
	    /// <summary>
	    /// Display length of file in Bytes or KB or MB or GB
	    /// </summary>
	    /// <param name="length">Length in bytes to convert</param>
	    /// <param name="useKibi">Calculate and show in Kibi format</param>
	    [Function("format-length")]
	    public string FormatLength(double length, bool useKibi)
	    {
	        int denominator = 1000;
	        string kibi = String.Empty;
	
	        if (useKibi)
	        {
	            denominator = 1024;
	            kibi = "i";
	        }
	
	        // Unable to use switch as demoninator is not constant
	        if (length < denominator)
	        {
	            return String.Format("{0:F0}B", length);
	        }
	        else if (length < (denominator*denominator))
	        {
	            return String.Format("{0:F2}K{1}B", (length/denominator), kibi);
	        }
	        else if (length < (denominator*denominator*denominator))
	        {
	            return String.Format("{0:F2}M{1}B", ((length/denominator)/denominator), kibi);
	        }
	        else
	        {
	            return String.Format("{0:F2}G{1}B", (((length/denominator)/denominator)/denominator), kibi);
	        }
	    }
	
	    /// <summary>
	    /// Build unique list of line separated directories recursing up
	    /// to root node based upon line separated list of directories.
	    /// Initial purpose is to convert 'svnlook dirs-changed' output
	    /// so that all parent directories are tested once for hook-prop.
	    /// </summary>
	    /// <param name="dirsChanged">List of dirs-changed</param>
	    [Function("build-dir-list")]
	    public string BuildDirList(string dirsChanged)
	    {
	        string[]  dirsList   = dirsChanged.Split(Environment.NewLine.ToCharArray());
	        string[]  tempDirs;
	        ArrayList dirsPath   = new ArrayList();
	        string    dirsResult = String.Empty;
	        string    tempPath   = String.Empty;
	        string    dirSplit   = "/";
	
	        foreach (string dir in dirsList)
	        {
	            tempPath = String.Empty;
	            tempDirs = dir.Split(dirSplit.ToCharArray());
	
	            foreach (string path in tempDirs)
	            {
	                tempPath += path;
	                if (!tempPath.EndsWith(dirSplit))
	                {
	                    tempPath += dirSplit;   
	                }   
	
	                // Add each directory to result if it isn't already in there
	                if (dirsPath.IndexOf(tempPath) < 0)
	                {
	                    dirsPath.Add(tempPath);
	                }
	            }
	        }
	
	        dirsPath.Sort();
	   
	        foreach (string dir in dirsPath)
	        {
	            dirsResult += dir + Environment.NewLine;
	        }
	
	        return dirsResult.Trim();
	    }
	
	    /// <summary>
	    /// Try and open a file for appending to determine if file is able to be deleted.
	    /// </summary>
	    /// <param name="filename">File to test</param>
	    [Function("can-delete-file")]
	    public bool CanDeleteFile(string filename)
	    {
	        try
	        {
	           StreamWriter w = File.AppendText(filename);
	           w.Close();                
	        }
	        catch
	        {
	            return false;
	        }
	        return true;
	    }
	
	    /// <summary>
	    /// Return STDIN as string
	    /// </summary>
	    [Function("get-stdin")]   
	    public string GetStdin()
	    {
	        return Console.In.ReadLine();
	    }

        #endregion Public Instance Methods
	}
}