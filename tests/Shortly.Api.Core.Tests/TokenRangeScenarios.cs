using Shortly.Core;

namespace Shortly.Api.Core.Tests;

public class TokenRangeScenarios
{
    [Fact]
    public void When_start_is_greater_than_end_token_then_throw_exception()
    {
        var act = () => new TokenRange(10, 5);
        act.Should().Throw<ArgumentException>().WithMessage("End Must Be Greater Than Or Equal The Start");
    }
}