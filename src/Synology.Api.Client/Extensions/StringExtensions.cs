using System;

namespace Synology.Api.Client.Extensions;

public static class StringExtensions
{
    public static string ToQuotedString(this string? value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return $"\"{value}\"";
    }
}