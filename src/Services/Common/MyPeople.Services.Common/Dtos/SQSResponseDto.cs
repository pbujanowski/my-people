using System.Text.Json;

namespace MyPeople.Services.Common.Dtos;

public class SQSResponseDto
{
    public int StatusCode { get; set; }

    public string? Message { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}
