using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bloggo.Models;
using Bloggo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Bloggo.Data;

namespace Bloggo.Controllers
{
    public class BlogPostController : Controller
    {

        private readonly IPostService _postService;
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogPostController(IPostService postService, UserManager<ApplicationUser> userManager)
        {
            
            _postService = postService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {


            var posts = await _postService.GetPostsAsync();

            var model = new PostViewModel()
            {
                Posts = posts
            };

            return View(model);

        }

        [Authorize]
        public IActionResult UserPost()
        {
            return View();
        }

        public async Task<IActionResult> UserPage()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var posts = await _postService.GetUserPostsAsync(currentUser);

            var model = new PostViewModel()
            {
                Posts = posts
            };

            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPost(Post newPost)
        {

            if (!ModelState.IsValid)
            {
                return View("UserPost");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();


            var successful = await _postService.AddPostAsync(newPost, currentUser);

            if (!successful)
            {
                return BadRequest("Could not add post.");
            }

            return RedirectToAction("Index");


        }
        public IActionResult PostPage(int id)
        {
          

            var post = _postService.GetPostById(id);
            

            return View(post);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePost(int postId)
        {
            
            var post = _postService.GetPostById(postId);
            var successful = await _postService.DeletePostAsync(post);

            if (!successful)
            {
                return BadRequest("Could not delete post.");
            }
            return RedirectToAction("Index");


        }
    }
}