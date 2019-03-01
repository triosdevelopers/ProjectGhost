using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ProjectGhost
{
	public class Program
	{
        public static Manager Manager = new Manager();
        public static Connections Connections = new Connections();

        public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:5000")
                .UseStartup<Startup>();
	}
}
