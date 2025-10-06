using ApiWebKut.Data.Repository.Interface;
using ApiWebKut.DTOs.TypeContent;
using ApiWebKut.Models;
using ApiWebKut.Services.Interfaces;

namespace ApiWebKut.Services
{
    public class TypeContentService(ITypeContentRepository typeContentRepository) : ITypeContentService
    {
       private readonly ITypeContentRepository _typeContentRepository = typeContentRepository ?? throw new ArgumentNullException(nameof(typeContentRepository));
        
        public async Task<List<TypeContentDto>> GetAllTypeContentsAsync()
        {
            var typeContents = await _typeContentRepository.GetAllTypeContentsAsync();
            return typeContents.Select(typeContent => new TypeContentDto
            {
                Id = typeContent.Id,
                Content = typeContent.Content,
                Description = typeContent.Description
            }).ToList();
        }
        public async Task<TypeContentDto?> GetTypeContentByIdAsync(int id)
        {
            var typeContent = await _typeContentRepository.GetTypeContentByIdAsync(id);
            if (typeContent == null)
            {
                return null;
            }
            return new TypeContentDto
            {
                Id = typeContent.Id,
                Content = typeContent.Content,
                Description = typeContent.Description
            };
        }
        public async Task<TypeContentDto> AddTypeContentAsync(CreateTypeContentDto typeContentDto)
        {
            var newTypeContent = new TypeContent
            {
                Content = typeContentDto.Content,
                Description = typeContentDto.Description
            };
            var createdTypeContent = await _typeContentRepository.AddTypeContentAsync(newTypeContent);
            return new TypeContentDto
            {
                Id = createdTypeContent.Id,
                Content = createdTypeContent.Content,
                Description = createdTypeContent.Description
            };
        }
        public async Task<TypeContentDto?> UpdateTypeContentAsync(int id, UpdateTypeContentDto typeContentDto)
        {
            var typeContentToUpdate = new TypeContent
            {
                Content = typeContentDto.Content,
                Description = typeContentDto.Description
            };
            var updatedTypeContent = await _typeContentRepository.UpdateTypeContentAsync(id, typeContentToUpdate);
            if (updatedTypeContent == null)
            {
                return null;
            }
            return new TypeContentDto
            {
                Id = updatedTypeContent.Id,
                Content = updatedTypeContent.Content,
                Description = updatedTypeContent.Description
            };
        }
        public async Task<bool> DeleteTypeContentAsync(int id)
        {
            return await _typeContentRepository.DeleteTypeContentAsync(id);
        }
    }
}
