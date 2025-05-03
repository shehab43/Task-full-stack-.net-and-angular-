using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class queryParams
    {
        public int? Take { get; set; } 
        public int? Skip { get; set; } 
        public string? SearchTerm { get; set; }
        public string? Filter { get; set; }
    }
}
