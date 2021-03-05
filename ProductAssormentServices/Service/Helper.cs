using Newtonsoft.Json;
using ProductAssortmentServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductAssortmentServices.Service
{
    public class Helper
    {
        public static async Task<string> GetProduct()  // Api call for GetProducts
        {
            string apiResponse;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Config.BaseURL + Config.ProductBaseEndPoint+"/"+"GetProducts")) 
                {
                    apiResponse = await response.Content.ReadAsStringAsync();

                }
            }
            return apiResponse;
        }

        public static async Task<List<string>> GetProductByID(int intId)  // Api call for Get product by product id
        {
            string apiResponse="";
            string statusCode;
            List<string> responseList = new List<string>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Config.BaseURL + Config.ProductBaseEndPoint +"/"+ "GetProduct" + "/" + intId))
                {
                        apiResponse = await response.Content.ReadAsStringAsync(); // Get Response
                        Int32 responseHttpStatusCode = (Int32)response.StatusCode; // Get StatusCode
                        statusCode = responseHttpStatusCode.ToString();

                    responseList.Add(statusCode);
                    responseList.Add(apiResponse); // Add Response
                }
            }

            return responseList;

        }

        public static async Task<List<string>> AddProduct(Products products) // Call api for Add product
        {
            string apiResponse = "";
            string statusCode;
            List<string> responseList = new List<string>();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(products), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(Config.BaseURL + Config.ProductBaseEndPoint+"/"+ "AddProduct", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    Int32 responseHttpStatusCode = (Int32)response.StatusCode;
                    statusCode = responseHttpStatusCode.ToString();
                    
                    responseList.Add(statusCode);
                    responseList.Add(apiResponse);
                }
            }
            return responseList;
        }

        public static async Task<string> GetAssortments() // Call Api for get assortments
        {
            string apiResponse;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Config.BaseURL + Config.AssortmentBaseEndPoint+"/"+ "GetAssortments"))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;
        }

        public static async Task<List<string>> GetAssortmentByID(int intId) // Call Api for get assortment by assortment id
        {
            string apiResponse = "";
            string statusCode;
            List<string> responseList = new List<string>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Config.BaseURL + Config.AssortmentBaseEndPoint +"/"+"GetAssortment" + "/" + intId))
                {
                    
                        apiResponse = await response.Content.ReadAsStringAsync();
                        Int32 responseHttpStatusCode = (Int32)response.StatusCode;
                        statusCode = responseHttpStatusCode.ToString();

                        responseList.Add(statusCode);
                        responseList.Add(apiResponse);
                }
            }

            return responseList;

        }

        public static async Task<List<string>> AddAssortment(Assortments assortment) // Api function for Add Assortment
        {
            string apiResponse = "";
            string statusCode;
            List<string> responseList = new List<string>();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(assortment), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(Config.BaseURL + Config.AssortmentBaseEndPoint+"/"+"AddAssortment", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync(); // Get Response
                    Int32 responseHttpStatusCode = (Int32)response.StatusCode;  // Get Status Code
                    statusCode = responseHttpStatusCode.ToString();

                    responseList.Add(statusCode);
                    responseList.Add(apiResponse);
                }
            }
            return responseList;
        }

        public static async Task <List<string>>AddAssortmentProducts(Assortments assortment) // Call Api function for Add Assortment to Products
        {
            string apiResponse;
            string statusCode;
            List<string> responseList = new List<string>();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(assortment), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(Config.BaseURL + Config.AssortmentProductBaseEndPoint+"/"+"AddAssortmentProduct", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync(); // Get Response
                    Int32 responseHttpStatusCode = (Int32)response.StatusCode;
                    statusCode = responseHttpStatusCode.ToString();

                    responseList.Add(statusCode);
                    responseList.Add(apiResponse);
                }
            }
           
            return responseList;
        }
    }
}
