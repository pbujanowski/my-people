using System.Collections;
using MyPeople.Services.Images.Domain.Entities;

namespace MyPeople.Services.Images.Tests.Common.TestData;

public class ImageTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return
        [
            new Image
            {
                Id = Guid.NewGuid(),
                Name = $"{Guid.NewGuid()}.png",
                Content =
                    "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mNk+A8AAQUBAScY42YAAAAASUVORK5CYII=",
                ContentType = "image/png"
            }
        ];
        yield return
        [
            new Image
            {
                Id = Guid.NewGuid(),
                Name = $"{Guid.NewGuid()}.jpg",
                Content
                    = "/9j/4AAQSkZJRgABAQEASABIAAD/2wBDAAMCAgMCAgMDAwMEAwMEBQgFBQQEBQoHBwYIDAoMDAsKCwsNDhIQDQ4RDgsLEBYQERMUFRUVDA8XGBYUGBIUFRT/wAALCAABAAEBAREA/8QAFAABAAAAAAAAAAAAAAAAAAAACf/EABQQAQAAAAAAAAAAAAAAAAAAAAD/2gAIAQEAAD8AKp//2Q==",
                ContentType = "image/jpeg"
            }
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}