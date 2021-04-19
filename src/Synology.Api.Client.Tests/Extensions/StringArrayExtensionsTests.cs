using Xunit;
using Synology.Api.Client.Extensions;
using FluentAssertions;

namespace Synology.Api.Client.Tests.Extensions
{
    public class StringArrayExtensionsTests
    {
        [Fact]
        public void StringArrayExtensions_ValidArray_Success()
        {
            // arrange
            var words = new[] { "Hello", "World" };

            // act
            var result = words.ToCommaSeparatedAroundBrackets();

            // assert
            result.Should().BeEquivalentTo(@"[""Hello"",""World""]");
        }

        [Fact]
        public void StringArrayExtensions_EmptyArray_ReturnEmptyBrackets()
        {
            // arrange
            var words = new string[0];

            // act
            var result = words.ToCommaSeparatedAroundBrackets();

            // assert
            result.Should().BeEquivalentTo("[]");
        }
    }
}
