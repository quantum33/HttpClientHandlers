using HttpClientHandlers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp.NETFwk.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var retryHandler = new WaitAndRetryHandler(3, 10);

            var client = new HttpClient(retryHandler)
            {
                BaseAddress = new Uri("http://localhost:53528/")
            };

            var response = client.GetAsync("http://localhost:53528/api/values").Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
