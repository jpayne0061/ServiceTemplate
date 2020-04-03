using System;
using System.IO;
using System.ServiceProcess;

namespace MyService
{
    class Program
    {
        public const string ServiceName = "MyService";

        static void Main(string[] args)
        {
            try
            {
                if (Environment.UserInteractive)
                {
                    // running as console app
                    Start(args);

                    while(true)
                    {
                        Random r = new Random();
                        var x = r.Next(1, 8);
                    }

                    Console.WriteLine("Press any key to stop...");

                    Stop();
                }
                else
                {
                    // running as service
                    using (var service = new Service())
                    {
                        ServiceBase.Run(service);
                    }
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(@"c:\temp\MyService.txt", ex.Message);
                throw;
            }


        }

        public static void Start(string[] args)
        {

            File.AppendAllText(@"c:\temp\MyService.txt", String.Format("{0} started{1}", DateTime.Now, Environment.NewLine));
        }

        public static void Stop()
        {
            File.AppendAllText(@"c:\temp\MyService.txt", String.Format("{0} stopped{1}", DateTime.Now, Environment.NewLine));
        }
    }
}