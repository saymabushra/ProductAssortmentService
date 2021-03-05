using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using ProductAssortmentServices.Models;
using Newtonsoft.Json;
using ProductAssortmentServices.Service;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ProductAssortmentServices.Controllers
{
    public class ProductController : Controller
    {
       // Get All Products List
        public async Task<IActionResult> Product()
        {
            List<Products> productList = new List<Products>();
            string apiResponse = await Helper.GetProduct(); // Call Api
            productList = JsonConvert.DeserializeObject<List<Products>>(apiResponse);
            return View(productList);
        }

        // Get Product by Product id
        public ViewResult GetProducts() => View();
        [HttpPost]
        public async Task<IActionResult> GetProducts(int id)
        {
            Products products = new Products();
            
            
            List<string> getResponse = await Helper.GetProductByID(id); // Call Api
            string statusCode = getResponse[0];
            string apiResponse = getResponse[1];
            products = JsonConvert.DeserializeObject<Products>(apiResponse);
            if (statusCode != "200")
            {
                ViewBag.Message = "Something Wrong.Try again";
            }
            return View(products);
        }

        // Add Assortment 
        public ViewResult AddProducts() => View();
        [HttpPost]
        public async Task<IActionResult> AddProducts(Products products)
        {
            Products receivedProducts = new Products();

            List<string> getResponse = await Helper.AddProduct(products); //Call Api
            string statusCode = getResponse[0];
            string apiResponse = getResponse[1];
       
            receivedProducts = JsonConvert.DeserializeObject<Products>(apiResponse);
            if (statusCode == "201")
            {
               
                 ViewBag.Message = "Save !!" ;
                    
            }
            else
            {
                ViewBag.Message = "Not Save";
            }
            return View(receivedProducts);
        }

    }
}
