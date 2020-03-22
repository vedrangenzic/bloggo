using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggo.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        [Required(ErrorMessage = "Title with maximum 20 characters is required.")]
        [MaxLength(15)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Text with minimum 300 characters is required.")]
        [MinLength(300)]
        public string Value { get; set; }
        [Required(ErrorMessage = "Text with maximum 10 characters is required.")]
        [MaxLength(10)]
        public string Subject { get; set; }
        public string Content { get; set; }

    }
}
 