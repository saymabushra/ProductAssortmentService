using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAssortmentServices.Models
{
    public class Assortments
    {
        public int AssrtmntId { get; set; }
        public string AssrtmntName { get; set; }
        public DateTime AssrtmntActiveFrom { get; set; }
        public DateTime? AssrtmntActiveTo { get; set; }

        public List<SelectListItem> AssortmentDropDownList { get; set; }
        
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<SelectListItem> ProductList { get; set; }
    }
}
