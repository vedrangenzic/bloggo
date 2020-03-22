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
                .ToArrayAsync();

            return posts;

        }
        public Post GetPostById(int postId)
        {

            var post = _context.Posts.FirstOrDefault(p => p.Id == postId);

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

            _context.Posts.Add(newPost);

            var saveResult = await _context.SaveChangesAsync();

            return saveResult == 1;
        }
        public async Task<Post[]> GetUserPostsAsync(ApplicationUser user)
        {
            
            var posts = await _context.Posts
                .Where(u => u.UserId == user.Id)
                .ToArrayAsync();

            return posts;

        }
    }
}
