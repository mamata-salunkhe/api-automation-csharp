using ApiAutomation.Base;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ApiAutomation
{
    internal class PartialUpdate_Patch : TestBase
    {
        
        [Test]
        [Category("PATCH")]
        public void TC01_Create_and_Partial_Update_User_info_and_validate_200()
        {
            //create new user first
            var request = new RestRequest("/objects", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                name = "Samsung Galaxy S21",
                data = new
                {
                    year = 2021,
                    price = 799.99,
                    CPU_model = "Exynos 2100",
                    Hard_disk_size = "128 GB"
                }
            });
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Console.WriteLine("Created User: " + response.Content);

            //Deserialize response
            Assert.That(response.Content, Is.Not.Null);
            var result = JsonConvert.DeserializeObject<ObjectResponse>(response.Content!);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.id, Is.Not.Null);
            Console.WriteLine("Created ID: " + result.id);

            // Now perform a PATCH to update part of the user data
            var patchRequest = new RestRequest($"/objects/{result.id}", Method.Patch);
            patchRequest.AddHeader("Content-Type", "application/json");
            patchRequest.AddJsonBody(new
            {
                data = new
                {
                    price = 749.99 // updating only the price
                }
            });
            var patchResponse = client.Execute(patchRequest);
            Assert.That(patchResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Console.WriteLine("Patched User: " + patchResponse.Content);
        }
        [Test]
        [Category("PATCH")]
        public void TC02_Partial_Update_Non_Existing_User_and_validate_404()
        {
            var request = new RestRequest("/objects/non_existing_id", Method.Patch);
            request.AddJsonBody(new
                {
                name = "Invalid-id"
            });
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
