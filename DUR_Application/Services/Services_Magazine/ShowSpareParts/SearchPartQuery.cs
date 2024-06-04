using DUR_Application.Model;

namespace DUR_Application.Services.Services_Magazine.ShowSpareParts
{
    public class SearchPartQuery
    {
        public string? Search { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    
        public string SortBy { get; set; }
        
        public SortDirection SortDirection { get; set; }   
    }
}
