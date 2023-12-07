using Polly;
using Polly.Extensions.Http;
using System.Diagnostics;

namespace ConsoleApp1;

public static class PollyPolicies
{
    public static IAsyncPolicy<HttpResponseMessage> RetryPolicy(int retryCount)
    {
        return HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(5,
            PollyPoliciesExtensions.ExponentialBackoff,
            onRetry: (_, timeSpan, retryAttempt, _) =>
            {
                Debug.WriteLine(retryCount != retryAttempt
                    ? $"Retry attempt {retryAttempt}, waiting {timeSpan.Seconds} seconds"
                    : $"Last retry, waiting {timeSpan.Seconds} seconds");
            });
    }
}