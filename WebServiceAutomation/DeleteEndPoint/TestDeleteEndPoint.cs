using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using WebServiceAutomation.Helper.Request;
using WebServiceAutomation.Helper.ResponseData;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.XmlModel;
using WebServiceAutomation.Helper.Authentication;

namespace WebServiceAutomation.DeleteEndPoint
{
    [TestClass]
    public class TestDeleteEndPoint
    {
        private string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private string deleteUrl = "http://localhost:8080/laptop-bag/webapi/api/delete/";
        private string securePostUrl = "http://localhost:8080/laptop-bag/webapi/secure/add";
        private string secureDeleteUrl = "http://localhost:8080/laptop-bag/webapi/secure/delete/";
        private RestResponse restResponse;
        private string xmlMediaType = "application/xml";
        private Random random = new Random();

        [TestMethod]
        public void TestDelete()
        {
            /*
             * Using the Post and add a record to the application
             * Call delete end point to delete the record --> 200 OK
             * Call the delete end point --> 400 Not Found
             */

            int id = random.Next(1000);
            AddRecord(id);

            using (HttpClient httpClient = new HttpClient())
            {
                Task<HttpResponseMessage> httpResponseMessage = httpClient.DeleteAsync(deleteUrl + id);
                HttpStatusCode httpStatusCode = httpResponseMessage.Result.StatusCode;
                Assert.AreEqual(200, (int)httpStatusCode);

                httpResponseMessage = httpClient.DeleteAsync(deleteUrl + id);
                httpStatusCode = httpResponseMessage.Result.StatusCode;
                Assert.AreEqual(404, (int)httpStatusCode);
            }
        }

        public void AddRecord(int id)
        {
            string xmlData = "<Laptop>" +
                                   "<BrandName>Alienware</BrandName>" +
                                      "<Features>" +
                                          "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                          "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                          "<Feature >NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                          "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                      "</Features>" +
                                   "<Id>" + id + "</Id>" +
                                   "<LaptopName>Alienware M17</LaptopName>" +
                            "</Laptop>";

            Dictionary<string, string> httpHeaders = new Dictionary<string, string>()
                {
                    { "Accept", "application/xml" }
                };

            restResponse = HttpClientHelper.PerformPostRequest(postUrl, xmlData, xmlMediaType, httpHeaders);

            //HttpContent httpContent = new StringContent(xmlData, Encoding.UTF8, xmlMediaType);
            //HttpClientHelper.PerformPostRequest(postUrl, httpContent, headers);

            Assert.AreEqual(200, restResponse.StatusCode);

            Laptop xmlDatat = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.ResponseContent);
            //Console.WriteLine(xmlDatat);
        }

        [TestMethod]
        public void TestDeleteUsingHelperClass()
        {
            int id = random.Next(1000);
            AddRecord(id);

            restResponse = HttpClientHelper.PerformDeleteRequest(deleteUrl + id);
            Assert.AreEqual(200, restResponse.StatusCode);

            restResponse = HttpClientHelper.PerformDeleteRequest(deleteUrl + id);
            Assert.AreEqual(404, restResponse.StatusCode);
        }

        [TestMethod]
        public void TestSecureDeleteEndPoint()
        {
            int id = random.Next(1000);

            AddRecord(id);
            string auth = Base64StringConverter.GetBase64String("admin", "welcome");
            auth = "Basic " + auth;
            Dictionary<string, string> httpHeaders = new Dictionary<string, string>()
            {
                { "Authorization", auth }
            };

            restResponse = HttpClientHelper.PerformDeleteRequest(secureDeleteUrl + id, httpHeaders);
            Assert.AreEqual(200, restResponse.StatusCode);

            restResponse = HttpClientHelper.PerformDeleteRequest(secureDeleteUrl + id, httpHeaders);
            Assert.AreEqual(404, restResponse.StatusCode);
        }
    }
}
