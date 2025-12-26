using ApiAutomation.Base;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ApiAutomation.Tests
{
    internal class DeleteUserTest :TestBase
    {
        [Test]
        [Category("DELETE")]
        public void TC01_Delete_Reserved_Id_Should_Return_405()
        {
            var request = new RestRequest("/objects/3", Method.Delete);
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.MethodNotAllowed));
            Assert.That(response.Content, Is.Not.Null);
            Assert.That(response.ContentType, Is.Not.Null);

            TestContext.Out.WriteLine($"Status Code: {response.StatusCode}");
            TestContext.Out.WriteLine($"Response Body: {response.Content}");
        }
        [Test]
        [Category("DELETE")]
        public void TC02_Create_And_Delete_User_Should_Return_200()
        {
            //create a new user id
            var request = new RestRequest("/objects", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                name = "RealMe",
                data = new
                {
                    year = 2022,
                    price = 20000,
                    CPU_model = "Intel Core i9",
                    Hard_disk_size = "128 GB"
                }
            });
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.ContentType, Is.Not.Null);

            TestContext.Out.WriteLine($"Status Code: {response.StatusCode}"); //logging
            TestContext.Out.WriteLine($"Response body: {response.Content}");

            //deserilization
            Assert.That(response.Content, Is.Not.Null, "Response body is null");
            var result = JsonConvert.DeserializeObject<ObjectResponse>(response.Content!);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.id, Is.Not.Null);
            TestContext.Out.WriteLine($"Created New ID: {result.id}");

            //Delete Above User
            var deleteRequest = new RestRequest($"/objects/{result.id}", Method.Delete);
            var deleteResponse = client.Execute(deleteRequest);
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(deleteResponse.Content, Is.Not.Null); //confirmation msg

            //logging
            TestContext.Out.WriteLine($"Status Code after Delete: {deleteResponse.StatusCode}");
            TestContext.Out.WriteLine($"Response after Delete: {deleteResponse.Content}");
        }
    }
}
