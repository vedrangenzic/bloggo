using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bloggo.Models
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CommentContent { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
