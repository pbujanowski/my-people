namespace MyPeople.Lambdas.Common.Dtos;

public class S3ObjectDto
{
    public required string Name { get; set; }

    public required Stream InputStream { get; set; }

    public required string BucketName { get; set; }
}
