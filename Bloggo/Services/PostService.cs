using Bloggo.Data;
using Bloggo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Bloggo.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Post[]> GetPostsAsync()
        {

            var posts = await _context.Posts                
                .AsNoTracking()
                .ToArrayAsync();

            return posts;

        }
        public Post GetPostById(int postId)
        {

            var post = _context.Posts
                .Include(p => p.MainComments)
                    .ThenInclude(p => p.SubComments)
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == postId);

            return post;

        }
        public async Task<bool> DeletePostAsync(Post post) {

            _context.Posts.Remove(post);

            var saveResult = await _context.SaveChangesAsync();

            return saveResult == 1;

        }
        
        public async Task<bool> AddPostAsync(Post newPost, ApplicationUser user)
        {

            newPost.UserId = user.Id;
            newPost.Username = user.UserName;
            newPost.DateCreated = DateTime.Now;

            _context.Posts.Add(newPost);

            var saveResult = await _context.SaveChangesAsync();

            return saveResult == 1;
        }
        public async Task<Post[]> GetUserPostsAsync(ApplicationUser user)
        {
            
            var posts = await _context.Posts
                .AsNoTracking()
                .Where(u => u.UserId == user.Id)
                .ToArrayAsync();

            return posts;

        }
        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
        }
    }
}
