namespace Synology.Api.Client.Session
{
    public class SynologySession : ISynologySession
    {
        public SynologySession(string sid)
        {
            Sid = sid;
        }

        public string Sid { get; }
    }
}
