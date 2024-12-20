using Amazon.Lambda.TestUtilities;
using FluentAssertions;
using Xunit;

namespace MyPeople.Lambdas.Images.Function.Tests;

public class FunctionTest
{
    [Fact]
    public void TestToUpperFunction()
    {
        var context = new TestLambdaContext();
        var upperCase = Function.FunctionHandler("hello world", context);

        upperCase.Should().Be("HELLO WORLD");
    }
}
