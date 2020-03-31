using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bloggo.Data.Migrations;
using Bloggo.Models;
using Bloggo.Models.Comments;
using Bloggo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace Bloggo.Controllers
{
    public class CommentController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public CommentController(IPostService postService, ICommentService commentService)
        {
            _commentService = commentService;
            _postService = postService;

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel commentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("PostPage");
            }

            var post = _postService.GetPostById(commentViewModel.PostId);

            if (commentViewModel.MainCommentId == 0)
            {
                post.MainComments = new List<MainComment>();

                post.MainComments.Add(new MainComment
                {

                    CommentContent = commentViewModel.CommentContent,
                    DateCreated = DateTime.Now

                }
                );
                  _postService.UpdatePost(post);

            }
            else
            {
                var subcomment = new SubComment
                {
                    MainCommentId = commentViewModel.MainCommentId,
                    CommentContent = commentViewModel.CommentContent,
                    DateCreated = DateTime.Now
                };

                _commentService.AddSubComment(subcomment);
            }

            await _postService.SaveChangesAsync();

            

            return RedirectToAction("PostPage", "BlogPost", new { id = commentViewModel.PostId });
        }
    }
}