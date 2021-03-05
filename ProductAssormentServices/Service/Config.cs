using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAssortmentServices.Service
{
    public class Config
    { 
        // Base URL and Endpoint
        public static string BaseURL = "https://localhost:44340/api/"; 
        public static string ProductBaseEndPoint = "Products";
        public static string AssortmentBaseEndPoint = "Assortments";
        public static string AssortmentProductBaseEndPoint = "AssortmentProducts";

    }
}
