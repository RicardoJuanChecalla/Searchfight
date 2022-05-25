namespace Searchfight.Core.Entities
{
    public class SearchResultBing
    {
        public string? _type { get; set; }
        public virtual QueryContext? QueryContext { get; set; }
        public virtual WebPage? WebPages { get; set; }
    }
}