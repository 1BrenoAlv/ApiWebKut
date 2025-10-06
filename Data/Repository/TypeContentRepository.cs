using ApiWebKut.Data.Repository.Interface;
using ApiWebKut.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiWebKut.Data.Repository
{
    public class TypeContentRepository(AppDbContext appDbContext) : ITypeContentRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));

        public async Task<TypeContent> AddTypeContentAsync(TypeContent typeContent)
        {
            await _appDbContext.TypeContent.AddAsync(typeContent);
            await _appDbContext.SaveChangesAsync();
            return typeContent;
        }

        public async Task<bool> DeleteTypeContentAsync(int id)
        {
            var typeContentDelete = _appDbContext.TypeContent.Find(id);
            if (typeContentDelete == null)
            {
                return false;
            }
            _appDbContext.TypeContent.Remove(typeContentDelete);
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<TypeContent>> GetAllTypeContentsAsync()
        {
            return await _appDbContext.TypeContent.ToListAsync();
        }

        public async Task<TypeContent> GetTypeContentByIdAsync(int id)
        {
            return await _appDbContext.TypeContent.FindAsync(id);
        }

        public async Task<TypeContent> UpdateTypeContentAsync(int id, TypeContent typeContent)
        {
            var typeContentUpdate = await _appDbContext.TypeContent.FindAsync(id);
            if (typeContentUpdate == null)
            {
                return null;
            } 
            typeContentUpdate.Content = typeContent.Content;
            typeContentUpdate.Description = typeContent.Description;
            await _appDbContext.SaveChangesAsync();
            return typeContentUpdate;
        }
    }
}
