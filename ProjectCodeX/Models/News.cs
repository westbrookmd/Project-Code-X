using System;
using System.Collections.Generic;

namespace ProjectCodeX.Models
{
    public partial class News
    {
        public int ArticleId { get; set; }
        public DateTime PublishDate { get; set; }
        public string Summary { get; set; } = "";
        public int ViewCount { get; set; }
        public string Author { get; set; } = "";
        public string Content { get; set; } = "";
        public string Headline { get; set; } = "";
    }
}
