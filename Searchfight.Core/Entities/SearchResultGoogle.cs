
namespace Searchfight.Core.Entities
{
    public class SearchResultGoogle
    {
        public virtual ICollection<Result>? Results { get; set; }
        public virtual ICollection<string>? ImageResults { get; set; }
        public long Total { get; set; }
        public virtual ICollection<string>? Answers { get; set; }
        public decimal Tiempo { get; set; }
        public string? DeviceRegion { get; set; }
        public string? DeviceType { get; set; }
    }
}