using Bloggo.Data;
using Bloggo.Models.Comments;

namespace Bloggo.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;

        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddSubComment(SubComment subcomment)
        {
            _context.SubComments.Add(subcomment);
        }
    }
}
