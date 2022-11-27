using System;
namespace VisualNovelAPI.Models
{
    public class VisualNovel
    {
        public int VisualNovelID { get; set; }
        public string? Title { get; set; }
        public string? Languages { get; set; }
        public string? Genre { get; set; }
        public string? Developer { get; set; }
    }
}

