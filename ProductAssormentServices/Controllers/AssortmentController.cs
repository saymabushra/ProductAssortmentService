using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductAssortmentServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ProductAssortmentServices.Service;
using System.Text;

namespace ProductAssortmentServices.Controllers
{
    public class AssortmentController : Controller
    {
        // Get All Assortment List
        public async Task<IActionResult> Assortment()
        {
            List<Assortments> assortmentList = new List<Assortments>();
            
            string apiResponse = await Helper.GetAssortments(); // Call Api 
            assortmentList = JsonConvert.DeserializeObject<List<Assortments>>(apiResponse);
            return View(assortmentList);
        }

        // Get Assortment  by Assortment Id
        public ViewResult GetAssortments() => View();
        [HttpPost]
        public async Task<IActionResult> GetAssortments(int id)
        {
            Assortments assortment = new Assortments();

            List<string> getResponse = await Helper.GetAssortmentByID(id); // Call Api
            string statusCode = getResponse[0];
            string apiResponse = getResponse[1];
            
            assortment = JsonConvert.DeserializeObject<Assortments>(apiResponse);
            if (statusCode != "200")
            {
                ViewBag.Message = "Something Wrong.Try again";
            }
            return View(assortment);
        }

        // Add Assortment
        public ViewResult AddAssortments() => View();
        [HttpPost]
        public async Task<IActionResult> AddAssortments(Assortments assortment)
        {
            Assortments receivedAssortments = new Assortments();
           
             
            List<string> getResponse = await Helper.AddAssortment(assortment); // Call Api
            string statusCode = getResponse[0];
            string apiResponse = getResponse[1];

            receivedAssortments = JsonConvert.DeserializeObject<Assortments>(apiResponse);
            if (statusCode == "201")
            {

                ViewBag.Message = "Save !!";
            }
            else
            {
                ViewBag.Message = "Not Save";
            }
            return View(receivedAssortments);
        }
    }
}
