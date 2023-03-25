using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SFA.Services;

namespace SFA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            //We are going to be using a timer to send out a notification. 
            //Every 2 weeks HQ staff needs an email sent to them on what missionaries
            //Will be going where. This will  happen everuy day. If the event date is exactly
            //two weeks away an email will be sent with all the information attached in an excel
           // Timer aTimer = new Timer();
           // Utilites tmp = new Utilites();
           // aTimer.Elapsed += new ElapsedEventHandler(tmp.SendEmailToHQStaff2weekschedules);
           // aTimer.Interval = 5000;
           // aTimer.Enabled = true;

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
