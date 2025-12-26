using ApiAutomation.Base;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ApiAutomation
{
    public class CreateUser : TestBase
    {
        [Test]
        [Category("POST")]
      public void TC01_Post_Create_New_User()
        {
            var request = new RestRequest("/objects", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                name = "Apple MacBook Pro 16",
                data = new
                {
                    year = 2019,
                    price = 1849.99,
                    CPU_model = "Intel Core i9",
                    Hard_disk_size = "1 TB"
                }

            });
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Console.WriteLine(response.Content);
        }

        [Test]
        [Category("POST")]
        public void TC02_Post_Create_New_User_and_validate_data()
        {
            var request = new RestRequest("/objects", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                name = "Apple MacBook Pro 17",
                data = new
                {
                    year = 2019,
                    price = 1849.99,
                    CPU_model = "Intel Core i9",
                    Hard_disk_size = "1 TB"
                }
            });
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Console.WriteLine(response.Content);

            Assert.That(response.Content, Is.Not.Null);
            Assert.That(response.Content, Is.Not.Empty);
            Assert.That(response.Headers, Is.Not.Null);
            Assert.That(response.Content, Does.Contain("Apple MacBook Pro 17"));
            Assert.That(response.ContentType, Does.Contain("application/json"));

            //Deserialize response
            var result = JsonConvert.DeserializeObject<ObjectResponse>(response.Content!);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.id, Is.Not.Null);

            Assert.That(result.name, Is.EqualTo("Apple MacBook Pro 17"));
            Assert.That(result.data, Is.Not.Null);
            Assert.That(result.data.ContainsKey("year"), Is.True);
            Assert.That(result.data["year"].ToString(), Is.EqualTo("2019"));
            Console.WriteLine("Created Object ID: " + result.id);

        }
        [Test]
        [Category("POST")]
        public void TC03_Create_New_User_with_EmptyRequestBody_andValidate400()
        {
            var request = new RestRequest("/objects", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(" ");
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Console.WriteLine(response.Content);
        }
        

    }
}
