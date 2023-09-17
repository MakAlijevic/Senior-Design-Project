using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.BLL.DTO
{
    public class SearchDto
    {
        public int[]? categories { get; set; } = null;
        public string? searchParam { get; set; } = string.Empty;
        public int minPrice { get; set; }
        public int maxPrice { get; set; }
        public int sortingType { get; set; }
        public int pageNumber { get; set; }
    }
}
