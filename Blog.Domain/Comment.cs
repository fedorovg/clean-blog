using System;

namespace Blog.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int PostId { get; set; }
        public Guid UserId { get; set; }
    }
}