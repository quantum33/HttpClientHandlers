using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DelegatingHttpHandlers
{
    public class FallbackHandler<T> : DelegatingHandler
    {
        public Func<T> Fallback { get; }
        public FallbackHandler(Func<T> fallback)
        {
            base.InnerHandler = new HttpClientHandler();// _handler;
            this.Fallback = fallback;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await base.SendAsync(request, cancellationToken);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (HttpRequestException)
            {
                // execute fallback
                var fallbackResult = Fallback();
                var json = JsonConvert.SerializeObject(Fallback());
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
            }

            throw null; // unreachable
        }

        //private static HttpClientHandler _handler = new HttpClientHandler();
    }
}
