using ApiWebKut.Data.Repository.Interface;
using ApiWebKut.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ApiWebKut.Data.Repository
{
    public class PostRepository(AppDbContext appDbContext) : IPostRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));

        public async Task<Posts> AddPostAsync(Posts post)
        {
            await _appDbContext.Posts.AddAsync(post);
            await _appDbContext.SaveChangesAsync();
            return post;
        }
        public async Task<bool> DeletePostAsync(int id)
        {
            var postToDelete = await _appDbContext.Posts.FindAsync(id);

            if (postToDelete == null)
            {
                return false;
            }
            postToDelete.IsDeleted = true;
            _appDbContext.Posts.Update(postToDelete);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        public async Task<List<Posts>> GetAllPostsAsync()
        {
            return await _appDbContext.Posts
                .Where(p => !p.IsDeleted)
                .Include(p => p.User)
                .Include(p => p.TypeContent)
                .Include(p => p.Likes)
                .ToListAsync();  // Ele busca apenas os posts que tem com o IsDeleted = false
        }
        public async Task<Posts?> GetPostByIdAsync(int id)
        {
            return await _appDbContext.Posts
                .Include(p => p.User)
                .Include(p => p.TypeContent)
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted); // Ele busca apenas os posts que tem com o IsDeleted = false
        }
        public async Task<IEnumerable<Posts>> GetPostsByUserIdAsync(Guid userId)
        {
            return await _appDbContext.Posts
                .Where(p => p.UserId == userId && !p.IsDeleted)
                .Include(p => p.User)
                .Include(p => p.TypeContent)
                .Include(p => p.Likes)
               // .OrderByDescending(p) fazer depois
                .ToListAsync();
        }
        public async Task<Posts?> UpdatePostAsync(int id, Posts post)
        {
            var existingPost = await _appDbContext.Posts.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (existingPost == null)
            {
                return null;
            }

            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            existingPost.TypeContentId = post.TypeContentId;

            await _appDbContext.SaveChangesAsync();
            return await GetPostByIdAsync(id);
        }
    }
}
