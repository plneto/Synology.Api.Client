namespace Synology.Api.Client.Extensions;

public static class StringExtensions
{
    public static string ToQuotedString(this string value) => $"\"{value}\"";
}