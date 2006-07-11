using System;
using System.Diagnostics;
using System.IO;

namespace subnant
{
    class HookStart
    {
        string fileName;
        string[] arguments;

        static void Main(string[] args)
        {
            HookStart program = new HookStart(args);

            if (args.Length > 0 && program.exists())
            {
                program.start();
            }
        }

        HookStart(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Starts long running process for Subversion hooks");
                Console.WriteLine("Usage: HookStart.exe <path/to/program> [arguments]");
            }
            else
            {
                fileName = args[0];
                arguments = new String[args.Length - 1];

                for (int index = 1; index < args.Length; index++)
                {
                    arguments.SetValue(args[index], index - 1);
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
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.Arguments = String.Join(" ", arguments);

            process.Start();
        }
    }
}