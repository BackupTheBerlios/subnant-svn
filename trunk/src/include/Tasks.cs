
// Copyright (C) 2005 Simon McKenna
//
// Licensed under the GNU General Public License
// http://www.gnu.org/copyleft/gpl.html
//
// $Id$ 

using System;
using System.IO;

using NAnt.Core;
using NAnt.Core.Attributes;

namespace Subnant.Tasks
{
	/// <summary>
	/// Write message to stderr
	/// </summary>
	[TaskName("stderr")]
	public class WriteStderr : Task
	{
	    #region Private Instance Fields
	
	    private string _message;
	
	    #endregion Private Instance Fields
	
	    #region Public Instance Properties
	
	    /// <summary>
	    /// Error message written
	    /// </summary>
	    [TaskAttribute("message", Required=true)]
	    [StringValidator(AllowEmpty=false)]
	    public string ErrorMessage
	    {
	      get { return _message; }
	      set { _message = value; }
	    }
	
	    #endregion Public Instance Properties
	
	    #region Override implementation of Task
	    
	    protected override void ExecuteTask()
	    {
	        Console.Error.WriteLine(ErrorMessage);
	    }
	    
	    #endregion Override implementation of Task
	}
	
	/// <summary>
	/// Write message to console and log file
	/// </summary>
	[TaskName("log-echo")]
	public class LogEcho : Task
	{
	    #region Private Instance Fields
	
	    private string _message;
	    private FileInfo _file;
	
	    #endregion Private Instance Fields
	
	    #region Public Instance Properties
	
	    /// <summary>
	    /// Message to be written to log and displayed in console
	    /// </summary>
	    [TaskAttribute("message", Required=true)]
	    [StringValidator(AllowEmpty=true)]
	    public string Message
	    {
	      get { return _message; }
	      set { _message = value; }
	    }
	
	    /// <summary>
	    /// File log is written to
	    /// </summary>
	    [TaskAttribute("file", Required=true)]
	    [StringValidator(AllowEmpty=false)]
	    public FileInfo File
	    {
	      get { return _file; }
	      set { _file = value; }
	    }
	
	    #endregion Public Instance Properties
	
	    #region Override implementation of Task
	    
	    protected override void ExecuteTask()
	    {
	        Log(Level.Warning, Message);
	
	        // Always append to log file
	        using (StreamWriter writer = new StreamWriter(File.FullName, true))
	        {
	            writer.WriteLine(Message);
	        }               
	    }
	    
	    #endregion Override implementation of Task
	}
}
