namespace Synology.Api.Client.Extensions
{
    public static class BoolExtensions
    {
        public static string ToLowerString(this bool _bool)
        {
            return _bool.ToString().ToLower();
        }
    }
}
