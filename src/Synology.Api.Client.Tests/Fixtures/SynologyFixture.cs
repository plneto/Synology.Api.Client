using Flurl.Http;
using Synology.Api.Client.ApiDescription;

namespace Synology.Api.Client.Tests.Fixtures
{
    public class SynologyFixture
    {
        public SynologyFixture()
        {
            SynologyHttpClient = new SynologyHttpClient(new FlurlClient(BaseUrl));
            ApisInfo = new DefaultApisInfo();
        }

        public ISynologyHttpClient SynologyHttpClient { get; }

        public IApisInfo ApisInfo { get; }

        public string BaseUrl => "http://dsm-url.com/webapi";
    }
}
