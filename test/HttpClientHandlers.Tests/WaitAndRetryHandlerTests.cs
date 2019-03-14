using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace HttpClientHandlers.Tests
{
    public class WaitAndRetryHandlerTests
    {
        [Fact]
        public void RetryHandler_WhenUsed_CallSuccess()
        {
            var retryHandler = new WaitAndRetryHandler(3, 10);
            var url = "http://localhost:53528";
            var client = new HttpClient(retryHandler)
            {
                BaseAddress = new Uri(url)
            };

            var res1 = client.GetAsync($"{url}/api/values").Result;
            var res2 = client.GetAsync($"{url}/api/values").Result;

            Assert.Equal(HttpStatusCode.OK, res1.StatusCode);
            Assert.Equal(HttpStatusCode.OK, res2.StatusCode);
        }

        [Fact]
        public void FallbackHandler_WhenUsed_Returns42()
        {
            var fallbackHandler = new FallbackHandler<string>(() => "42");
            var client = new HttpClient(fallbackHandler)
            {
                BaseAddress = new Uri("http://inexistingUrl:5300/")
            };

            var res = client.GetAsync("http://toto:53528/api/values").Result;
            var mustBe42 = res.Content.ReadAsStringAsync().Result;
            Assert.Equal("42", mustBe42);
        }
    }
}