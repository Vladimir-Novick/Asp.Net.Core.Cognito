using System;
using Microsoft.AspNetCore.Hosting;


[assembly: HostingStartup(typeof(Asp.Net.Core.Cognito.Areas.Identity.IdentityHostingStartup))]
namespace Asp.Net.Core.Cognito.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            try
            {
                builder.ConfigureServices((context, services) =>
                {
                });
            }
            catch (Exception ex) {
                 Console.WriteLine(ex.Message);
            }
        }
    }
}