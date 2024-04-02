using System.Collections;
using MyPeople.Services.Images.Domain.Entities;

namespace MyPeople.Services.Images.Infrastructure.Tests.TestData;

public class ImageTestData : IEnumerable<object[]>
{
    private readonly IEnumerable<object[]> _images = [
        [
            new Image
            {
                Name = "image1.png",
                Content = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mNk+A8AAQUBAScY42YAAAAASUVORK5CYII=",
                ContentType = "image/png"
            }
        ]
    ];

    public IEnumerator<object[]> GetEnumerator() => _images.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}