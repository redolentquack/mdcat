using System;

namespace mdcat
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            var exitCode = 0;

            try
            {
                exitCode = ExecuteApp(args);
            }
            catch (Exception exp)
            {
                Console.Error.WriteLine("Error:");
                Console.Error.WriteLine(exp.Message);
            }
            finally
            {
                // Always reset on the way out :)
                Console.ResetColor();
            }

            return exitCode;
        }

        private static int ExecuteApp(string[] args)
        {
            var app = new App();

            if (args == null || args.Length == 0 || args[0] == "-h" || args[0] == "--help" || args[0] == "/?")
            {
                app.Help();
                return -1;
            }

            app.Run(args[0]);

            return 0;
        }
    }
}