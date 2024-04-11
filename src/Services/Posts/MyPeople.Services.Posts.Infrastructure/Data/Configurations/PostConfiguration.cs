using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPeople.Services.Posts.Domain.Entities;

namespace MyPeople.Services.Posts.Infrastructure.Data.Configurations;

public class PostConfiguration : EntityConfiguration<Post>
{
    public override void Configure(EntityTypeBuilder<Post> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.UserId).IsRequired();

        builder.Property(e => e.Content).IsRequired();

        builder.HasMany(e => e.Images).WithOne(e => e.Post).HasForeignKey(e => e.PostId);
    }
}
