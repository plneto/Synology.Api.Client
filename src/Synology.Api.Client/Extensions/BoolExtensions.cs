namespace Synology.Api.Client.Extensions
{
    public static class BoolExtensions
    {
        public static string ToLowerString(this bool @bool)
        {
            return @bool.ToString().ToLower();
        }
    }
}
