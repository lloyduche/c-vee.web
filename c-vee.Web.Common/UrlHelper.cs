using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;

namespace c_vee.Web.Common
{
    public class UrlHelper
    {
        public static string BaseAddress()
        {
            return "http://localhost:5000/";
        }

        public static string BaseAddress(string path)
        {
            return  "http://localhost:5000/" + path;
        }

        //Create a url given the url Path
        public static string CreateUrl(string urlPath, HttpContext context)
        {
            return string.Join('/', BaseAddress(), urlPath);
        }

        public static string CreateUrl(string urlPath, string url, HttpContext context)
        {
            return string.Join('/', BaseAddress( url), urlPath);
        }

        //generate link to be embeded in the emails
        public static string GetEmailLink(Dictionary<string, string> queryParams, string urlPath, HttpContext context)
        {
            var path = urlPath.StartsWith('/') ? urlPath.Substring(1) : urlPath;
            var baseUrl = CreateUrl(path, context);
            //construct the account confirmation link
            return QueryHelpers.AddQueryString(baseUrl, queryParams);
        }
    }
}
