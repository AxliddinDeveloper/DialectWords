using DialectWords.Models.Foundations.words;

namespace DialectWords.Models
{
    public class WordsViewModel
    {
        public IQueryable<Word> Words { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
    }
}
