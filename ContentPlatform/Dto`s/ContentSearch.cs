using ContentPlatform.Helpers;
using System.Net.Mime;

namespace ContentPlatform.Dto_s
{
    public class ContentSearch
    {
        public List<ContentType>? ContentTypes { get; set; }
        public int? YearFrom { get; set;}
        public int? YearTo { get; set; }
        public double? MinRating { get; set; }
        public SortBy SortBy { get; set; }
    }
}
