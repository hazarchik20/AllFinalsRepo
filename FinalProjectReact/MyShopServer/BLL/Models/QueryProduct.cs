using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class QueryProduct
    {
            public int Page { get; set; } = 1;
            public int PageSize { get; set; } = 8;
            public string? Search { get; set; }
            public int? CategoryId { get; set; }
            public decimal? PriceFrom { get; set; }
            public decimal? PriceTo { get; set; }
            public string SortBy { get; set; } = "name";
            public string SortDir { get; set; } = "asc";
    }
}
