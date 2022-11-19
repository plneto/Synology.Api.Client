using System.Collections.Generic;
using System.Linq;

namespace Synology.Api.Client.Extensions
{
    public static class StringArrayExtensions
    {
        public static string ToCommaSeparatedAroundBrackets(this IEnumerable<string> list) =>
            $"[{string.Join(",", list.Select(l => $"\"{l}\""))}]";
    }
}
