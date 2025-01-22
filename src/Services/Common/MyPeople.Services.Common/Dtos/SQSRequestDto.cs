namespace MyPeople.Services.Common.Dtos;

public class SQSRequestDto
{
    public required string QueueUrl { get; set; }

    public required IEnumerable<string> Messages { get; set; }
}
