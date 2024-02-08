﻿namespace MyPeople.Common.Models.Dtos;

public class ImageDto
{
    public Guid? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Name { get; set; }

    public string? ContentType { get; set; }

    public string? Content { get; set; }
}
