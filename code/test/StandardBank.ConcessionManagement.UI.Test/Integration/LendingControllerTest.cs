using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StandardBank.ConcessionManagement.UI.Test.Integration
{
    public class LendingControllerTest : IClassFixture<TestFixture<Startup>>
    {
       
        private readonly HttpClient _client;
        public LendingControllerTest(TestFixture<Startup> fixture)
        {

            _client = fixture.Client;
        }
        [Fact]
        public async Task NewLending_Should_Fail_When_concession_is_invalid()
        {
            var lend = JsonConvert.SerializeObject(new LendingConcession { Concession = new Model.UserInterface.Concession { Id =1 } });
            var message = new StringContent(lend, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/Lending/NewLending",message);
            Assert.Equal(Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity, (int)response.StatusCode);
        }
    }
}
