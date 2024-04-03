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
        
        _repositories.Images.Detach(createdEntity);
        
        return _mapper.Map<ImageDto>(createdEntity);
    }

    public async Task<IEnumerable<ImageDto>?> CreateImagesAsync(
        IEnumerable<CreateImageDto> imagesDto
    )
    {
        var createdEntities = imagesDto
            .Select(imageDto => _mapper.Map<Image>(imageDto))
            .Select(entity => _repositories.Images.Create(entity))
            .ToList();

        await _repositories.SaveChangesAsync();

        foreach (var entity in createdEntities)
        {
            _repositories.Images.Detach(entity);
        }
        
        return _mapper.Map<IEnumerable<ImageDto>>(createdEntities);
    }

    public async Task<IEnumerable<ImageDto>?> DeleteImagesAsync(
        IEnumerable<DeleteImageDto> imagesDtos
    )
    {
        var entities = _mapper.Map<IEnumerable<Image>>(imagesDtos);
        var deletedEntities = entities
            .Select(entity => _repositories.Images.Delete(entity))
            .ToList();

        await _repositories.SaveChangesAsync();

        foreach (var entity in deletedEntities)
        {
            _repositories.Images.Detach(entity);
        }
        
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

        return _mapper.Map<IEnumerable<ImageDto>>(entities);
    }
}