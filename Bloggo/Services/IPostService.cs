using Bloggo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggo.Services
{
    public interface IPostService
    {
        Task<Post[]> GetPostsAsync();
        Post GetPostById(int postId);
        Task<bool> DeletePostAsync(Post post);
        Task<bool> AddPostAsync(Post newPost, ApplicationUser currentUser);
        Task<Post[]> GetUserPostsAsync(ApplicationUser user);
    }
}
