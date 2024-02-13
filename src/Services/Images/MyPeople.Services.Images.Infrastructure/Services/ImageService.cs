using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyPeople.Common.Abstractions.Services;
using MyPeople.Common.Models.Dtos;
using MyPeople.Services.Images.Application.Wrappers;
using MyPeople.Services.Images.Domain.Entities;

namespace MyPeople.Services.Images.Infrastructure.Services;

public class ImageService(IRepositoryWrapper repositories, IMapper mapper) : IImageService
{
    private readonly IMapper _mapper = mapper;
    private readonly IRepositoryWrapper _repositories = repositories;

    public async Task<ImageDto?> CreateImageAsync(CreateImageDto imageDto)
    {
        var entity = _mapper.Map<Image>(imageDto);
        var createdEntity = _repositories.Images.Create(entity);
        await _repositories.SaveChangesAsync();
        return _mapper.Map<ImageDto>(createdEntity);
    }

    public async Task<IEnumerable<ImageDto>?> CreateImagesAsync(
        IEnumerable<CreateImageDto> imagesDto
    )
    {
        var createdEntities = new List<Image>();
        foreach (var imageDto in imagesDto)
        {
            var entity = _mapper.Map<Image>(imageDto);
            var createdEntity = _repositories.Images.Create(entity);
            createdEntities.Add(createdEntity);
        }

        await _repositories.SaveChangesAsync();
        return _mapper.Map<IEnumerable<ImageDto>>(createdEntities);
    }

    public async Task<IEnumerable<ImageDto>?> DeleteImagesAsync(
        IEnumerable<DeleteImageDto> imagesDtos
    )
    {
        var entities = _mapper.Map<IEnumerable<Image>>(imagesDtos);
        var deletedEntities = new List<Image>();
        foreach (var entity in entities)
        {
            var deletedEntity = _repositories.Images.Delete(entity);
            deletedEntities.Add(deletedEntity);
        }

        await _repositories.SaveChangesAsync();
        return _mapper.Map<IEnumerable<ImageDto>>(deletedEntities);
    }

    public async Task<ImageDto?> GetImageByIdAsync(Guid id)
    {
        var entity = await _repositories
            .Images.FindByCondition(p => p.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return entity is null ? null : _mapper.Map<ImageDto>(entity);
    }

    public async Task<IEnumerable<ImageDto>?> GetImagesByIdsAsync(IEnumerable<Guid> ids)
    {
        var entities = await _repositories
            .Images.FindByCondition(p => ids.ToList().Contains((Guid)p.Id!))
            .AsNoTracking()
            .ToListAsync();

        return entities is null ? null : _mapper.Map<IEnumerable<ImageDto>>(entities);
    }
}