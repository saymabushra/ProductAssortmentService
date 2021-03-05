using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAssortmentServiceApi.Models
{
    public class ApiConstants
    {
        public static string API_MODEL_ERROR = "Required parameter can not be null or empty";
       

        #region Response Messages
        public static string UNHANDELED_EXCEPTION_MESSAGE = "Unhandled Exception Occoured.";
        public static string NULL_POINTER_EXCEPTION_OCCURED = "Null pointer exception.Check your input.";
        public static string BAD_INPUT_MESSAGE = "Provide valid input.";
        public static string EXCEED_MAX_LIMIT = "Exceeded max input limit.";


        #endregion
    }
}
