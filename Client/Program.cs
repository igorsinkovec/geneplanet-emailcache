using Orleans;
using Orleans.Configuration;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmailCache
{
    public class Program
    {
        public static Task Main()
        {
            return new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<ClientHostedService>();
                    services.AddSingleton<IHostedService>(sp => sp.GetService<ClientHostedService>());
                    services.AddSingleton<IClusterClient>(sp => sp.GetService<ClientHostedService>().Client);
                    services.AddSingleton<IGrainFactory>(sp => sp.GetService<ClientHostedService>().Client);
                })
                .ConfigureLogging(builder => builder.AddConsole())
                .RunConsoleAsync();
        }
    }
}

/*
namespace EmailCache
{

    public class Program
    {
        static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                using (var client = await ConnectClient())
                {
                    var email = "igor@fu.bar";

                    await EmailExists(client, email);


                    Console.ReadKey();
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nException while trying to run client: {e.Message}");
                Console.WriteLine("Make sure the silo the client is trying to connect to is running.");
                Console.WriteLine("\nPress any key to exit.");
                Console.ReadKey();
                return 1;
            }
        }

        private static async Task<IClusterClient> ConnectClient()
        {
            IClusterClient client;
            client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "GenePlanetEmailCache";
                })
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();

            await client.Connect();
            Console.WriteLine("Client successfully connected to silo host \n");
            return client;
        }

        private static async Task<bool> EmailExists(IClusterClient client, string email)
        {
            var emailList = client.GetGrain<IEmailList>(getDomain(email));
            var response = await emailList.Contains(email);
            Console.WriteLine("\n EmailExists: {0}\n", response);
            return response;
        }

        private static string getDomain(string email)
        {
            return new MailAddress(email).Host;
        }

        private static string getTopLevelDomain(string email)
        {
            return getDomain(email).Split('.')[1];
        }
    }
}
 */
