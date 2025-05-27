using Shortly.Core;

namespace Shortly.Api.Core.Tests;

public class Base62EncodingScenarios
{
    [Theory]
    [InlineData(1, "1")]
    [InlineData(987654321,"14q60P")]
    public void Should_encode_number_to_base62(long number, string expected)
    {
        number.EncodeToBase62().Should().Be(expected);
    }
}