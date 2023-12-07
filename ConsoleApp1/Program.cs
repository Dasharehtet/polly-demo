using ConsoleApp1;
using Polly;
using System.Diagnostics;

var httpClient = new HttpClient();
const string pdfUrl = "pdfUrl";
var retryPolicy = Policy<byte[]>
    .Handle<HttpRequestException>()
    .WaitAndRetryAsync(5,
        PollyPoliciesExtensions.ExponentialBackoff,
        onRetry: (_, timeSpan, retryAttempt, _) =>
        {
            Debug.WriteLine(5 != retryAttempt
                ? $"Retry attempt {retryAttempt}, waiting {timeSpan.Seconds} seconds"
                : $"Last retry, waiting {timeSpan.Seconds} seconds");
        });

var bytes = await retryPolicy.ExecuteAsync(() => httpClient.GetByteArrayAsync(pdfUrl));

Console.WriteLine(bytes);