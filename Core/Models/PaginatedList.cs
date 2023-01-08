using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class PaginatedList
    {
        public PaginatedList(int currentPage, int pageSize, int count,int totalPages, List<Product> list)
        {
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.TotalCount = count;
            this.TotalPages = totalPages;
            this.List = list;
        }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public List<Product> List { get; set; }
    }
}
