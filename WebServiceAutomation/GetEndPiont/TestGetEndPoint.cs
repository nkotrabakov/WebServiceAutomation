using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;
using WebServiceAutomation.Helper.Request;
using WebServiceAutomation.Helper.ResponseData;
using WebServiceAutomation.Helper.Authentication;

namespace WebServiceAutomation.GetEndPiont
{
    [TestClass]
    public class TestGetEndPoint
    {
        private string getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";
        private string secureGetUrl = "http://localhost:8080/laptop-bag/webapi/secure/all";
        private string delayGet = "http://localhost:8080/laptop-bag/webapi/delay/all";

        [TestMethod]
        public void TestGetAllEndPoint()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.GetAsync(getUrl);
            httpClient.Dispose();
        }

        [TestMethod]
        public void TestGetAllEndPointWithUri()
        {
            //Create HTTP client
            HttpClient httpClient = new HttpClient();

            //Create the request and execute it
            Uri getUri = new Uri(getUrl);
            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUri);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            Console.WriteLine(httpResponseMessage.ToString());

            //Status
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Status => " + statusCode);
            Console.WriteLine("Status code => " + (int)statusCode);

            //Response data
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result; 
            Console.WriteLine(data);

            //Close the connection
            httpClient.Dispose();
        }

        [TestMethod]
        public void TestGetAllEndPointWithInvalidUri()
        {
            //Create HTTP client
            HttpClient httpClient = new HttpClient();

            //Create the request and execute it
            Uri getUri = new Uri(getUrl + "/new");
            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUri);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            Console.WriteLine(httpResponseMessage.ToString());

            //Status
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Status => " + statusCode);
            Console.WriteLine("Status code => " + (int)statusCode);

            //Response data
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine(data);

            //Close the connection
            httpClient.Dispose();
        }

        [TestMethod]
        public void TestAllEndPiontInJsonFormat()
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders requestHeader = httpClient.DefaultRequestHeaders;
            requestHeader.Add("Accept", "application/json");

            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUrl);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            Console.WriteLine(httpResponseMessage.ToString());

            //Status
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Status => " + statusCode);
            Console.WriteLine("Status code => " + (int)statusCode);

            //Response data
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine(data);

            //Close the connection
            httpClient.Dispose();
        }

        [TestMethod]
        public void TestAllEndPiontInXmlFormat()
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders requestHeader = httpClient.DefaultRequestHeaders;
            requestHeader.Add("Accept", "application/xml");

            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUrl);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            Console.WriteLine(httpResponseMessage.ToString());

            //Status
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Status => " + statusCode);
            Console.WriteLine("Status code => " + (int)statusCode);

            //Response data
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine(data);

            //Close the connection
            httpClient.Dispose();
        }

        [TestMethod]
        public void TestAllEndPiontInXmlFormatUsingAcceptHeader()
        {
            MediaTypeWithQualityHeaderValue jsonHeader = new MediaTypeWithQualityHeaderValue("application/json");
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders requestHeader = httpClient.DefaultRequestHeaders;
            requestHeader.Accept.Add(jsonHeader);
            //requestHeader.Add("Accept", "application/xml");

            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUrl);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            Console.WriteLine(httpResponseMessage.ToString());

            //Status
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Status => " + statusCode);
            Console.WriteLine("Status code => " + (int)statusCode);

            //Response data
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine(data);

            //Close the connection
            httpClient.Dispose();
        }

        [TestMethod]
        public void TestGetEndPointUsingSendAsync()
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.RequestUri = new Uri(getUrl);
            httpRequestMessage.Method = HttpMethod.Get;
            httpRequestMessage.Headers.Add("Accept", "application/json");

            HttpClient httpClient = new HttpClient();
            httpClient.SendAsync(httpRequestMessage);

            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getUrl);
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            Console.WriteLine(httpResponseMessage.ToString());

            //Status
            HttpStatusCode statusCode = httpResponseMessage.StatusCode;
            Console.WriteLine("Status => " + statusCode);
            Console.WriteLine("Status code => " + (int)statusCode);

            //Response data
            HttpContent responseContent = httpResponseMessage.Content;
            Task<string> responseData = responseContent.ReadAsStringAsync();
            string data = responseData.Result;
            Console.WriteLine(data);

            //Close the connection
            httpClient.Dispose();

        }

        [TestMethod]
        public void TestUsingStatement()
        {
            using(HttpClient httpClient = new HttpClient())
            {
                using(HttpRequestMessage httpRequestMessage = new HttpRequestMessage()) 
                {
                    httpRequestMessage.RequestUri = new Uri(getUrl);
                    httpRequestMessage.Method = HttpMethod.Get;
                    httpRequestMessage.Headers.Add("Accept", "application/json");

                    Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);

                    using (HttpResponseMessage httpResponseMessage = httpResponse.Result)
                    {
                        Console.WriteLine(httpResponseMessage.ToString());

                        //Status
                        HttpStatusCode statusCode = httpResponseMessage.StatusCode;
                        Console.WriteLine("Status => " + statusCode);
                        Console.WriteLine("Status code => " + (int)statusCode);

                        //Response data
                        HttpContent responseContent = httpResponseMessage.Content;
                        Task<string> responseData = responseContent.ReadAsStringAsync();
                        string data = responseData.Result;
                        Console.WriteLine(data);

                        RestResponse restResponse = new RestResponse((int)statusCode, responseData.Result);
                        Console.WriteLine(restResponse.ToString());
                    }
                }
            }
        }

        [TestMethod]
        public void TestDeserilizationOfJsonResponse()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                {
                    httpRequestMessage.RequestUri = new Uri(getUrl);
                    httpRequestMessage.Method = HttpMethod.Get;
                    httpRequestMessage.Headers.Add("Accept", "application/json");

                    Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);

                    using (HttpResponseMessage httpResponseMessage = httpResponse.Result)
                    {
                        Console.WriteLine(httpResponseMessage.ToString());

                        //Status
                        HttpStatusCode statusCode = httpResponseMessage.StatusCode;
                        Console.WriteLine("Status => " + statusCode);
                        Console.WriteLine("Status code => " + (int)statusCode);

                        //Response data
                        HttpContent responseContent = httpResponseMessage.Content;
                        Task<string> responseData = responseContent.ReadAsStringAsync();
                        string data = responseData.Result;
                        Console.WriteLine(data);

                        RestResponse restResponse = new RestResponse((int)statusCode, responseData.Result);
                        //Console.WriteLine(restResponse.ToString());

                        List<JsonRootObject> jsonObject = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.ResponseContent);
                        Console.WriteLine(jsonObject[0].ToString());
                    }
                }
            }
        }

        [TestMethod]
        public void TestDeserilizationOfXmlResponse()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                {
                    httpRequestMessage.RequestUri = new Uri(getUrl);
                    httpRequestMessage.Method = HttpMethod.Get;
                    httpRequestMessage.Headers.Add("Accept", "application/xml");

                    Task<HttpResponseMessage> httpResponse = httpClient.SendAsync(httpRequestMessage);

                    using (HttpResponseMessage httpResponseMessage = httpResponse.Result)
                    {
                        Console.WriteLine(httpResponseMessage.ToString());

                        //Status
                        HttpStatusCode statusCode = httpResponseMessage.StatusCode;
                        Console.WriteLine("Status => " + statusCode);
                        Console.WriteLine("Status code => " + (int)statusCode);

                        //Response data
                        HttpContent responseContent = httpResponseMessage.Content;
                        Task<string> responseData = responseContent.ReadAsStringAsync();
                        string data = responseData.Result;
                        //Console.WriteLine(data);

                        RestResponse restResponse = new RestResponse((int)statusCode, responseData.Result);
                        //Console.WriteLine(restResponse.ToString());

                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(LaptopDetailss));

                        TextReader textReader = new StringReader(restResponse.ResponseContent);

                        LaptopDetailss xmlData = (LaptopDetailss)xmlSerializer.Deserialize(textReader);
                        Console.WriteLine(xmlData.ToString());

                        //1st assertion for status code
                        Assert.AreEqual(200, restResponse.StatusCode);

                        //2nd assertion for response data
                        Assert.IsNotNull(restResponse.ResponseContent);

                        //3rd assertion
                        Assert.IsTrue(xmlData.Laptop.Features.Feature.Contains("Windows 10 Home 64-bit English"), "Item not found");

                        //4th assertion
                        Assert.AreEqual("Alienware", xmlData.Laptop.BrandName);
                    }
                }
            }
        }

        [TestMethod]
        public void GetUsingHelperMethod()
        {
            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept", "application/json");

            RestResponse restResponse = HttpClientHelper.PerformGetRequest(getUrl, httpHeader);

            //List<JsonRootObject> jsonObject = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.ResponseContent);
            //Console.WriteLine(jsonObject[0].ToString());

            List<JsonRootObject> jsonData = ResponseDataHelper.DeserializeJsonResponse<List<JsonRootObject>>(restResponse.ResponseContent);
            Console.WriteLine(jsonData.ToString());
        }

        [TestMethod]
        public void TestSecuredGetEndPoint()
        {
            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept", "application/json");
            string authHeader = Base64StringConverter.GetBase64String("admin", "welcome");
            authHeader = "Basic " + authHeader;

            httpHeader.Add("Authorization", authHeader);

            RestResponse restResponse = HttpClientHelper.PerformGetRequest(secureGetUrl, httpHeader);

            //List<JsonRootObject> jsonObject = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.ResponseContent);
            //Console.WriteLine(jsonObject[0].ToString());
            Assert.AreEqual(200, restResponse.StatusCode);

            List<JsonRootObject> jsonData = ResponseDataHelper.DeserializeJsonResponse<List<JsonRootObject>>(restResponse.ResponseContent);
            Console.WriteLine(jsonData.ToString());
        }

        [TestMethod]
        public void TestGetEndpoint_Sync() 
        {
            //Statement 1
            HttpClientHelper.PerformGetRequest(delayGet, null);
            //Statement 2
            HttpClientHelper.PerformGetRequest(delayGet, null);
            //Statement 3
            HttpClientHelper.PerformGetRequest(delayGet, null);
            //Statement 4
            HttpClientHelper.PerformGetRequest(delayGet, null);
        }

        [TestMethod]
        public void TestGetEndpoint_Async()
        {
            Task t1 = new Task(getEndPointFailed());
            t1.Start();
            Task t2 = new Task(getEndPointFailed());
            t2.Start();
            Task t3 = new Task(getEndPointFailed());
            t3.Start();
            Task t4 = new Task(getEndPointFailed());
            t4.Start();

            t1.Wait();
            t2.Wait();
            t3.Wait();
            t4.Wait();
        }

        private Action getEndPoint()
        {
            Dictionary<string, string> httpHeaders = new Dictionary<string, string>()
            {
                { "Accept", "application/xml" },
            };
            return new Action(() =>
            {
                RestResponse restresponse = HttpClientHelper.PerformGetRequest(delayGet, httpHeaders);
                Assert.AreEqual(200, restresponse.StatusCode);
            });
        }

        private Action getEndPointFailed()
        {
            Dictionary<string, string> httpHeaders = new Dictionary<string, string>()
            {
                { "Accept", "application/xml" },
            };
            return new Action(() =>
            {
                RestResponse restresponse = HttpClientHelper.PerformGetRequest(delayGet, httpHeaders);
                Assert.AreEqual(202, restresponse.StatusCode);
            });
        }
    }
}
