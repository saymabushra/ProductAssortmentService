using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductAssortmentServices.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using ProductAssortmentServices.Service;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace ProductAssortmentServices.Controllers
{
    public class ProductToAssortmentController : Controller
    {
        
        // Get Assortment Data and Product Date for Add to AssortmentProduct
        public async Task<IActionResult> ProductToAssortment()
        {
            Assortments assrt = new Assortments();
            assrt.AssortmentDropDownList = await Utils.PopulateDropDownAssortment(); // Api call for get Assortments Data
            assrt.ProductList= await Utils.PopulateDropDownProduct();  // Api call for get Products Data
            return View(assrt);
        }

        // Add Assorment and Product to AssortmentProdcuts.
        [HttpPost]
        public async Task<IActionResult> ProductToAssortment(Assortments assrt)
        {
            string statusCode;
            Assortments receivedAssortments = new Assortments();
            List <string> getResponse= await Helper.AddAssortmentProducts(assrt);
            statusCode = getResponse[0];
            string getApiResponse = getResponse[1];
            
            receivedAssortments = JsonConvert.DeserializeObject<Assortments>(getApiResponse);
            assrt.AssortmentDropDownList = await Utils.PopulateDropDownAssortment();
            assrt.ProductList = await Utils.PopulateDropDownProduct();

            if ( statusCode=="201")  // Check response
            {
                var selectedItem = assrt.AssortmentDropDownList.Find(p => p.Value == assrt.AssrtmntId.ToString());
                var selectedItemProduct = assrt.ProductList.Find(p => p.Value == assrt.ProductId.ToString());
                if (selectedItemProduct != null)
                {
                    selectedItem.Selected = true;
                    selectedItemProduct.Selected = true;
                    ViewBag.Message = "Save !!" + "||Assortment ID: " + selectedItem.Value + "," + selectedItem.Text;
                    ViewBag.Message += "|| Product ID:" + selectedItemProduct.Value + "," + selectedItemProduct.Text;
                }
            }
            else
            {
                ViewBag.Message = "Not Save";
            }

            return View(assrt);
        }
    }
}
