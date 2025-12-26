using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAutomation.Base
{
    public class TestBase
    {
        protected RestClient client;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://api.restful-api.dev");
        }

        [TearDown]
        public void TearDown()
        {
            client?.Dispose();
        }
    }
}
