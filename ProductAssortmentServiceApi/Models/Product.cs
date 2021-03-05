using System;
using System.Collections.Generic;

#nullable disable

namespace ProductAssortmentServiceApi.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductEancode { get; set; }

        public virtual AssortmentProduct AssortmentProduct { get; set; }
    }
}
