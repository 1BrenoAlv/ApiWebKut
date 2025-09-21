namespace ApiWebKut.Data.Repository.Interface
{
    public interface ITypeContentRepository
    {
        Task<List<Models.TypeContent>> GetAllTypeContentsAsync();
        Task<Models.TypeContent?> GetTypeContentByIdAsync(int id);
        Task<Models.TypeContent> AddTypeContentAsync(Models.TypeContent typeContent);
        Task<Models.TypeContent?> UpdateTypeContentAsync(int id, Models.TypeContent typeContent);
        Task<bool> DeleteTypeContentAsync(int id);
    }
}
