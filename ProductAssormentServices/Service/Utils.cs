using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProductAssortmentServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductAssortmentServices.Service
{
    public class Utils
    {
        public static async Task<List<SelectListItem>> PopulateDropDownAssortment()
        {
                       
                List<SelectListItem> items = new List<SelectListItem>();
                List<Assortments> assortmentList = new List<Assortments>();

                        string apiResponseAssortment = await Helper.GetAssortments();
                        assortmentList = JsonConvert.DeserializeObject<List<Assortments>>(apiResponseAssortment);
                        for (int i = 0; i <= assortmentList.Count - 1; i++)
                        {
                            items.Add(new SelectListItem
                            {
                                Text = assortmentList[i].AssrtmntName.ToString(),
                                Value = assortmentList[i].AssrtmntId.ToString()
                            });
                        }

            return items;
        }

        public static async Task<List<SelectListItem>> PopulateDropDownProduct() // function for populate product data for Dropdown
        {

            Products products = new Products();

            List<SelectListItem> producItems = new List<SelectListItem>();
            List<Products> productList = new List<Products>();

             string apiResponse = await Helper.GetProduct();
             productList = JsonConvert.DeserializeObject<List<Products>>(apiResponse);

                    for (int i = 0; i <= productList.Count - 1; i++)
                    {
                        producItems.Add(new SelectListItem
                        {
                            Text = productList[i].ProductName.ToString(),
                            Value = productList[i].ProductId.ToString()
                        });
                    }
             
            return producItems;
        }
    }
}
