using System;

namespace Bloggo.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentContent { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
