namespace Core.Models
{
    public class ProductParamsRequest
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? OrderByfield { get; set; } 
        public bool? OrderAscending { get; set; }
        public string? Categories { get; set; }//json
        public string? Brands { get; set; }//json
        public string? Search { get; set; }
    }
}
