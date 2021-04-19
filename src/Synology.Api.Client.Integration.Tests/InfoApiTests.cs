using FluentAssertions;
using Synology.Api.Client.Integration.Tests.Fixtures;
using Xunit;

namespace Synology.Api.Client.Integration.Tests
{
    public class InfoApiTests : IClassFixture<SynologyFixture>
    {
        private readonly SynologyFixture _fixture;

        public InfoApiTests(SynologyFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void InfoApi_Query_Success()
        {
            // arrange && act
            var response = await _fixture.Client.InfoApi().QueryAsync();

            // assert
            response.Should().NotBeNull();
        }
    }
}