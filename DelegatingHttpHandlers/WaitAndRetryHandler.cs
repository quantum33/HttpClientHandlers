using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DelegatingHttpHandlers
{
    public class WaitAndRetryHandler : DelegatingHandler
    {
        /// <summary>
        /// Time to wait before re-trying (in ms)
        /// </summary>
        public int Delay { get; }

        /// <summary>
        /// Number of retries
        /// </summary>
        public int RetryCount { get; }

        public WaitAndRetryHandler(int retryCount, int delay)
        {
            if (retryCount <= 0) { throw new ArgumentOutOfRangeException(nameof(retryCount)); }
            if (delay <= 0) { throw new ArgumentOutOfRangeException(nameof(delay)); }

            base.InnerHandler = new HttpClientHandler();
            RetryCount = retryCount;
            Delay = delay;
        }

        /// <inheritdoc />
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            for (int i = 0; i < RetryCount; i++)
            {
                try
                {
                    var response = await base.SendAsync(request, cancellationToken);
                    response.EnsureSuccessStatusCode();
                    return response;
                }
                catch (HttpRequestException) when (i == RetryCount - 1)
                {
                    // too many errors
                    throw;
                }
                catch (HttpRequestException)
                {
                    // wait and retry
                    await Task.Delay(Delay);
                }
            }

            throw null; // unreachable
        }
    }
}
