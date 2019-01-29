using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ProjectGhost
{
	public class Program
	{
        public static Manager Manager = new Manager();


        public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
