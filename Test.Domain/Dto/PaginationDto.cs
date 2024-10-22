using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Domain.Dto
{
    public class PaginationDto
    {
        public int Page { get; set; } = 1;
        public int RecordsNumber { get; set; } = 10;
        public double Count { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public string? Filter { get; set; }
    }
}
