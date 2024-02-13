using MyPeople.Services.Images.Application.Repositories;
using MyPeople.Services.Images.Domain.Entities;
using MyPeople.Services.Images.Infrastructure.Data;

namespace MyPeople.Services.Images.Infrastructure.Repositories;

public class ImageRepository(ApplicationDbContext dbContext)
    : RepositoryBase<Image>(dbContext),
        IImageRepository
{
}