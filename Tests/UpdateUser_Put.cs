using ApiAutomation.Base;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ApiAutomation
{
    public class UpdateUser_Put : TestBase
    {

        [Test]
        [Category("PUT")]
        public void TC01_Update_Reserved_Id_Should_Return_405()
        {
            var request = new RestRequest("/objects/7", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            var body = new
            {
                name = "Apple MacBook 17",
                data = new
                {
                    year = 2025,
                    price = 140000,
                    CPU_model = "Intel Core i9",
                    Hard_disk_size = "1 TB"
                }
            };
            request.AddJsonBody(body);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.MethodNotAllowed));
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty);
            Assert.That(response.ContentType, Is.Not.Null);
            Console.WriteLine("Responsed content: " + response.Content);

        }
        [Test]
        [Category("PUT")]
        public void TC02_Create_and_Update_New_User_and_validate_200()
        {
            //create new user first
            var request = new RestRequest("/objects", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                name = "One Plus Nord 3",
                data = new
                {
                    year = 2024,
                    price = 30000,
                    CPU_model = "Intel Core i9",
                    Hard_disk_size = "1 TB"
                }
            });
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty);
            Assert.That(response.ContentType, Does.Contain("application/json"));
            Console.WriteLine("Created User Content Response: " + response.Content);

            //Deserialize response
            var result = JsonConvert.DeserializeObject<ObjectResponse>(response.Content!);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.id, Is.Not.Null);
            Console.WriteLine("Created ID: " + result.id);

            //Update Above created Details
            var requestPut = new RestRequest($"/objects/{result.id}", Method.Put);
            requestPut.AddHeader("Content-Type", "application/json");
            var body = new
            {
                name = "Apple MacBook 17",
                data = new
                {
                    year = 2025,
                    price = 140000,
                    CPU_model = "Intel Core i9",
                    Hard_disk_size = "1 TB"
                }
            };
            requestPut.AddJsonBody(body);
            var responsePut = client.Execute(requestPut);
            Assert.That(responsePut.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responsePut.Content, Is.Not.Null.And.Not.Empty);
            Assert.That(responsePut.ContentType, Is.Not.Null);
            Console.WriteLine("Updated User Content Response:" + responsePut.Content);
        }

        [Test]
        [Category("PUT")]
        public void TC03_Update_User_with_invalid_id_and_validate_Error_404()
        {
            var request = new RestRequest("/objects/invalid-endpoint", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            var body = new
            {
                name = "Invalid Update",
            };
            request.AddJsonBody(body);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Console.WriteLine("Responsed content: " + response.Content);

        }
    }
}
