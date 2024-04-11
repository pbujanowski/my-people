using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPeople.Services.Posts.Domain.Entities;

namespace MyPeople.Services.Posts.Infrastructure.Data.Configurations;

public class PostImageConfiguration : EntityConfiguration<PostImage>
{
    public override void Configure(EntityTypeBuilder<PostImage> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.PostId).IsRequired();

        builder
            .HasOne(e => e.Post)
            .WithMany(e => e.Images)
            .HasForeignKey(e => e.PostId)
            .IsRequired();

        builder.Property(e => e.ImageId).IsRequired();
    }
}
