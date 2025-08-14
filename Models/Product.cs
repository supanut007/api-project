using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace ProjectApi.Models
{
    public class Product
    {
        public int Id { get; set; } // PK
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }
}

