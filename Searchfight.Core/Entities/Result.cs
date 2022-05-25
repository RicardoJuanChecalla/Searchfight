namespace Searchfight.Core.Entities
{
    public class Result
    {
        public string? Title {get; set;}
        public string? Link {get; set;}
        public string? Description {get; set;}
        public virtual ICollection<AdditionalLink>? AdditionalLinks { get; set; }
        public virtual Cite? Cite { get; set; }
    }
}    