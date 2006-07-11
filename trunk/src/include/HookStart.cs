
// Copyright (C) 2006 Simon McKenna
//
// Licensed under the GNU General Public License
// http://www.gnu.org/copyleft/gpl.html
//
// $Id$ 

using System;
using System.Diagnostics;
using System.IO;

namespace Subnant
{
    /// <summary>
    /// Used by Subversion post-commit (or similar) hooks to ensure client doesn't have
    /// to wait after commit has completed before allowing client to continue.
    /// </summary>
    class HookStart
    {
        string fileName;
        string[] arguments;

        static void Main(string[] args)
        {
            HookStart hookStart = new HookStart(args);
        }

        HookStart(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Starts process. Built for use with Subversion hooks");
                Console.WriteLine("Usage: HookStart.exe <path/to/process> [arguments]");
            }
            else
            {
                fileName = args[0];
                arguments = new String[args.Length - 1];

                for (int index = 1; index < args.Length; index++)
                {
                    arguments.SetValue(args[index], index - 1);
                }

                if (exists())
                {
                    start();
                }
            }
        }

        bool exists()
        {
            if (File.Exists(fileName))
            {
                return true;
            }
            else
            {
                Console.WriteLine("File not found: {0}", fileName);              
                return false;
            }
        }

        void start()
        {
            Process process = new Process();

            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = String.Join(" ", arguments);

            process.Start();
        }
    }
}