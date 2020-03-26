using Bloggo.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggo.Services
{
    public interface ICommentService
    {
        void AddSubComment(SubComment subcomment);
    }
}
