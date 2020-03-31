using Bloggo.Models;
using Bloggo.Models.Comments;
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
        bool UpdatePost(Post post);
        Task<bool> SaveChangesAsync();
    }
}
