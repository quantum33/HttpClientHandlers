using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net;
using DelegatingHttpHandlers;

namespace WebApplication.NetFwk.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var retryHandler = new WaitAndRetryHandler(3, 10);
            var client = new HttpClient(retryHandler)
            {
                BaseAddress = new Uri("http://localhost:53528/")
            };

            var res1 = client.GetAsync("http://localhost:53528/api/values").Result;
            var res2 = client.GetAsync("http://localhost:53528/api/values").Result;

            Assert.AreEqual(res1.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(res2.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void FallbackHandler()
        {
            var fallbackHandler = new FallbackHandler<int>(() => 42);
            var client = new HttpClient(fallbackHandler)
            {
                BaseAddress = new Uri("http://inexistingUrl:5300/")
            };

            var res = client.GetAsync("http://localhost:53528/api/values").Result;
            var mustBe42 = res.Content.ReadAsStringAsync().Result;
            Assert.AreEqual(int.Parse(mustBe42), 42);
        }

        [TestMethod]
        public void Foo()
        {
            var client = new HttpClient();
            var res = client.GetAsync("http://www.google.com").Result.Content.ReadAsStringAsync();
        }
    }
}
