﻿namespace MyPeople.Common.Models.Dtos;

public class CreatePostDto
{
    public Guid? UserId { get; set; }

    public string? Content { get; set; }
}
