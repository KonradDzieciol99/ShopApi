using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecification
    {
        public ProductSpecification()
        {

        }

        public ProductSpecification(int? pageNumber, int? pageSize, int? minPrice, int? maxPrice, List<int>? brandsIds, List<int>? categoriesIds, string? orderByfield, bool? orderAscending, string? search)
        {
            PageNumber = pageNumber ?? 1;
            PageSize = pageSize ?? 5;
            MinPrice = minPrice ?? 0;
            MaxPrice = maxPrice ?? 100000;
            this.brandsIds = brandsIds;
            this.categoriesIds = categoriesIds;
            OrderByfield = orderByfield ?? "Price";
            OrderAscending = orderAscending ?? true;
            Search = search;
        }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = 10000;
        public List<int>? brandsIds { get; set; } = null;
        public List<int>? categoriesIds { get; set; } = null;
        public string OrderByfield { get; set; } = "Price";
        public bool OrderAscending { get; set; } = true;
        public string? Search { get; set; } = null;
    }
}
