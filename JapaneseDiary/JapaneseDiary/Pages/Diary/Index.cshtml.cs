using System.Text.Json;
using JapaneseDiary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace JapaneseDiary.Pages.Diary
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        private const int PageSize = 10;

        public IndexModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IReadOnlyList<DiaryEntry> Entries { get; private set; } = Array.Empty<DiaryEntry>();
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public string? Query { get; private set; }

        public void OnGet(int page = 1, string? q = null)
        {
            Query = q;
            var jsonPath = Path.Combine(_env.ContentRootPath, "Data", "diary.json");
            if (!System.IO.File.Exists(jsonPath))
            {
                Entries = Array.Empty<DiaryEntry>();
                CurrentPage = 1;
                TotalPages = 1;
                return;
            }

            using var reader = new StreamReader(jsonPath, Encoding.UTF8);
            var json = reader.ReadToEnd();
            var all = JsonSerializer.Deserialize<List<DiaryEntry>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<DiaryEntry>();

            if (!string.IsNullOrWhiteSpace(Query))
            {
                var ql = Query.Trim();
                all = all.Where(e => e.Title.Contains(ql, StringComparison.OrdinalIgnoreCase)
                                   || e.Text.Contains(ql, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Order chronologically
            all = all.OrderBy(e => e.Date).ToList();

            // Paginate by unique day rather than fixed page size
            var uniqueDays = all.Select(e => e.Date).Distinct().OrderBy(d => d).ToList();
            TotalPages = Math.Max(1, uniqueDays.Count);
            CurrentPage = Math.Clamp(page, 1, TotalPages);

            if (uniqueDays.Count == 0)
            {
                Entries = Array.Empty<DiaryEntry>();
                return;
            }

            var selectedDay = uniqueDays[CurrentPage - 1];
            Entries = all.Where(e => e.Date == selectedDay)
                         .OrderBy(e => e.Date)
                         .ToList();
        }
    }
}
