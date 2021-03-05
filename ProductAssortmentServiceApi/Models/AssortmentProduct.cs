using System;
using System.Collections.Generic;

#nullable disable

namespace ProductAssortmentServiceApi.Models
{
    public partial class AssortmentProduct
    {
        public int ProductId { get; set; }
        public int AssrtmntId { get; set; }

        public virtual Assortment Assrtmnt { get; set; }
        public virtual Product Product { get; set; }
    }
}
