using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPeople.Services.Images.Domain.Entities;

namespace MyPeople.Services.Images.Infrastructure.Data.Configurations;

public class ImageConfiguration : EntityConfiguration<Image>
{
    public override void Configure(EntityTypeBuilder<Image> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name).IsRequired();

        builder.Property(e => e.Extension).IsRequired();

        builder.Property(e => e.Content).IsRequired();
    }
}
