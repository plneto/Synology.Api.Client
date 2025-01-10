namespace Synology.Api.Client.Extensions;

public static class StringExtensions
{
    public static string ToQuottedString(this string value) => $"\"{value}\"";
}