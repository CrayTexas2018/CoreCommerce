using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace CoreCommerce.Models
{
    public class ShopifyManager
    {
        private ApplicationContext context;

        public ShopifyManager(ApplicationContext context)
        {
            this.context = context;
        }

        public string shopifyGetRequest(string endpoint)
        {
            // Get all products from shopify
            //string url = "https://bee3869c2811039606dad0f0d819e543:9d8c95ebd2408e11d3ac87d3e5d5c65f@core-commerce-dev.myshopify.com/admin/products.json";
            CompanyRepository cr = new CompanyRepository(context);
            string json = null;
            endpoint = cr.GetCompanyFromApiUser().shopify_url + endpoint;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            string username = cr.GetCompanyFromApiUser().shopify_secret;
            string password = cr.GetCompanyFromApiUser().shopify_password;
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            request.Headers.Add("Authorization", "Basic " + svcCredentials);
            WebResponse response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                json = reader.ReadToEnd();
            }
            return json;
        }

        public string shopifyPostRequest(string endpoint, string json)
        {
            CompanyRepository cr = new CompanyRepository(context);
            endpoint = cr.GetCompanyFromApiUser().shopify_url + endpoint;
            string username = cr.GetCompanyFromApiUser().shopify_secret;
            string password = cr.GetCompanyFromApiUser().shopify_password;
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(endpoint);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Basic " + svcCredentials);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
    }
}