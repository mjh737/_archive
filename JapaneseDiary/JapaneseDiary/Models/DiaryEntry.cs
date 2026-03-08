using System;

namespace JapaneseDiary.Models
{
    public class DiaryEntry
    {
        public DateOnly Date { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}
