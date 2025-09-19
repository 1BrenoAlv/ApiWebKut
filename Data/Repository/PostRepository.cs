using ApiWebKut.Data.Repository.Interface;
using ApiWebKut.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiWebKut.Data.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _appDbContext;
        public PostRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Posts> AddPostAsync(Posts post)
        {
            await _appDbContext.Posts.AddAsync(post);
            await _appDbContext.SaveChangesAsync();
            return post;
        }
        public async Task<bool> DeletePostAsync(int id)
        {
            var postDelete = await _appDbContext.Posts.FindAsync(id);
            if (postDelete == null)
            {
                return false;
            }
            _appDbContext.Posts.Remove(postDelete);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        public async Task<List<Posts>> GetAllPostsAsync()
        {
            return await _appDbContext.Posts.ToListAsync();
        }
        public async Task<Posts?> GetPostByIdAsync(int id)
        {
            return await _appDbContext.Posts.FindAsync(id);
        }
        public async Task<Posts?> UpdatePostAsync(int id, Posts post)
        {
            var existingPost = await _appDbContext.Posts.FindAsync(id);
            if (existingPost == null)
            {
                return null;
            }
            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            existingPost.TypeContentId = post.TypeContentId;
            await _appDbContext.SaveChangesAsync();
            return existingPost;
        }
    }
}
