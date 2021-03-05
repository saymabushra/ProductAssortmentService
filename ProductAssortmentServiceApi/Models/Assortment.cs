using System;
using System.Collections.Generic;

#nullable disable

namespace ProductAssortmentServiceApi.Models
{
    public partial class Assortment
    {
        public Assortment()
        {
            AssortmentProducts = new HashSet<AssortmentProduct>();
        }

        public int AssrtmntId { get; set; }
        public string AssrtmntName { get; set; }
        public DateTime AssrtmntActiveFrom { get; set; }
        public DateTime? AssrtmntActiveTo { get; set; }

        public virtual ICollection<AssortmentProduct> AssortmentProducts { get; set; }
    }
}
