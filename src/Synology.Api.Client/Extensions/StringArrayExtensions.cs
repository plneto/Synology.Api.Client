namespace Synology.Api.Client.Extensions
{
    public static class StringArrayExtensions
    {
        public static string ToCommaSeparatedAroundBrackets(this string[] list)
        {
            var param = "[";

            foreach (var str in list)
            {
                param += $"\"{str}\",";
            }

            param = param.TrimEnd(',');
            param += "]";

            return param;
        }
    }
}
