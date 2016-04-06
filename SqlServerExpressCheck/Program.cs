using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace SqlServerExpressCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            string myServiceName = "MSSQL$SQLEXPRESS";
            string status;

            Console.WriteLine("Service: " + myServiceName);

            ServiceController mySc = new ServiceController(myServiceName);

            // Try to get the service status
            try
            {
                status = mySc.Status.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("Service not found.  It is probably not installed. \nException: " + e.Message);
                Console.ReadLine();
            }

            // Start the service if it was stopped
            if (mySc.Status.Equals(ServiceControllerStatus.Stopped) | mySc.Status.Equals(ServiceControllerStatus.StopPending))
            {
                try
                {
                    Console.WriteLine("Starting the service...");
                    mySc.Start();
                    mySc.WaitForStatus(ServiceControllerStatus.Running);
                    Console.WriteLine("The service is now " + mySc.Status.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in starting the service: " + e.Message);
                }
            }

            Console.WriteLine("Press a key to end the app...");
            Console.ReadLine();
            return;
        }
    }
}
