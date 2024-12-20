using Amazon.Lambda.Core;

[assembly: LambdaSerializer(
    typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer)
)]

namespace MyPeople.Lambdas.Images.Function;

public class Function
{
    public static string FunctionHandler(string input, ILambdaContext context)
    {
        return input.ToUpper();
    }
}
