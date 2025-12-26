using ApiAutomation.Base;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ApiAutomation
{
    internal class GetUserTest : BaseTest
    {
       
        [Test]
        [Category("GET")]
        public void TC01_Get_User_List()
        {
            var request = new RestRequest("/objects", Method.Get);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        [Category("GET")]
        public void TC02_Get_User_By_Id_Should_Return_200_And_Correct_Id()
        {
            // Arrange
            var request = new RestRequest("/objects", Method.Get);
            request.AddParameter("id", 3);
            request.AddParameter("id", 5);
            request.AddParameter("id", 10);

            // Act
            var response = client.Execute(request);

            // Assert

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Is.Not.Null);

            Console.WriteLine(response.Content); // for verification
        }
        [Test]
        [Category("GET")]
        public void TC03_Get_User_by_Id()
        {
            var request = new RestRequest("/objects/3", Method.Get);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));


            Assert.That(response.Content, Does.Contain("3")); //can be fail hence below is alternative solution
             // Deserialize response
            var result = JsonConvert.DeserializeObject<ObjectResponse>(response.Content);
            // Field validations
            Assert.That(result, Is.Not.Null);
            if (result is not null)
            {
                Assert.That(result.id, Is.EqualTo("3"));
                Assert.That(result.name, Does.Contain("Apple"));
                Assert.That(result.data, Is.Not.Null);
            }

        }

        [Test]
        [Category("GET")]
        public void TC04_Get_User_by_Id_and_verify_time()
        {
            var request = new RestRequest("/objects/5", Method.Get);
            //request.AddParameter("id", 5); //alternative
            var startTime = DateTime.Now;
            var response = client.Execute(request);
            var endTime = DateTime.Now;
            var responseTime = endTime - startTime;
            //status code
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            //response time
            Assert.That(responseTime.TotalSeconds, Is.LessThan(5));

            //response body
            Assert.That(response.Content, Is.Not.Null);

            //header
            Assert.That(response.Headers, Is.Not.Empty);

            //content type
            Assert.That(response.Content, Does.Contain("Samsung"));
            Assert.That(response.ContentType, Does.Contain("application/json"));

            //print on console
            Console.WriteLine("Status Code: " + response.StatusCode);
            Console.WriteLine("Response Time: " + responseTime.TotalSeconds);
            Console.WriteLine("Response content: " + response.Content);
            Console.WriteLine("Response Header: " + response.Headers);
            Console.WriteLine("Response Content Type: " + response.ContentType);

        }
        [Test]
        [Category("GET")]
        public void TC05_Get_User_by_invalid_end_point_and_should_return_404()
        {
            var request = new RestRequest("/objects/invalid-endpoint", Method.Get);
            var response = client.Execute(request);
            //validate status code
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

            //validate Response Body (Error Message)
            Assert.That(response.Content, Is.Not.Null);
            Assert.That(response.Content, Is.Not.Empty);
            Assert.That(response.Content.ToLower(), Does.Contain("was not found"));

            //No Business Data Returned
            Assert.That(response.Content, Does.Not.Contain("\"id\""));
            Assert.That(response.Content, Does.Not.Contain("\"name\""));

            // verify Response Headers Exist
            Assert.That(response.Headers, Is.Not.Empty);

            // verify Content type
            Assert.That(response.ContentType, Does.Contain("application/json"));

            //logging
            Console.WriteLine("response Content: " + response.Content);

        }
        
    }
}
