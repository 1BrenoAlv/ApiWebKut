using ApiWebKut.DTOs.Posts;
using ApiWebKut.DTOs.TypeContent;

namespace ApiWebKut.Services.Interfaces
{
    public interface ITypeContentService
    {
        Task<List<TypeContentDto>> GetAllTypeContentsAsync();
        Task<TypeContentDto?> GetTypeContentByIdAsync(int id);
        Task<TypeContentDto> AddTypeContentAsync(CreateTypeContentDto typeContentDto);
        Task<TypeContentDto?> UpdateTypeContentAsync(int id, UpdateTypeContentDto typeContentDto);
        Task<bool> DeleteTypeContentAsync(int id);
    }
}