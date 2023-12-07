namespace ConsoleApp1;

public static class PollyPoliciesExtensions
{
    public static TimeSpan ExponentialBackoff(int retryCount) =>
        TimeSpan.FromSeconds(Math.Pow(2, retryCount));
}