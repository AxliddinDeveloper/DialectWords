namespace DialectWords.Models.Foundations.words
{
    public class Word
    {
        public Guid id { get; set; }
        public string? AdabiyTil { get; set; }
        public string? Transliteratsiya { get; set; }
        public string? Transkripsiya { get; set; }
        public string? Turkum { get; set; }
        public string? ShevaIzohi { get; set; }
        public string? Misollar { get; set; }
        public string? Sinonim { get; set; }
        public string? Omonim { get; set; }
        public string? Antonim { get; set; }
        public string? OzlashganQatlam { get; set; }
        public string? RusTilida { get; set; }
        public string? IngilizTilida { get; set; }
    }
}
