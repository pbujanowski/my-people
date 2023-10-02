using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyPeople.Services.Images.Application.Dtos;
using MyPeople.Services.Images.Application.Services;
using MyPeople.Services.Images.Application.Wrappers;
using MyPeople.Services.Images.Domain.Entities;

namespace MyPeople.Services.Images.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly IRepositoryWrapper _repositories;
        private readonly IMapper _mapper;

        public ImageService(IRepositoryWrapper repositories, IMapper mapper)
        {
            _repositories = repositories;
            _mapper = mapper;
        }

        public async Task<ImageDto?> CreateImageAsync(ImageDto imageDto)
        {
            var entity = _mapper.Map<Image>(imageDto);
            var createdEntity = _repositories.Images.Create(entity);
            await _repositories.SaveChangesAsync();
            return _mapper.Map<ImageDto>(createdEntity);
        }

        public async Task<ImageDto?> GetImageByIdAsync(Guid id)
        {
            var entity = await _repositories.Images
                .FindByCondition(p => p.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return entity is null ? null : _mapper.Map<ImageDto>(entity);
        }
    }
}
