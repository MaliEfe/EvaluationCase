using EvaluationCase.Application.Features.Commands.CreateCampaign;
using EvaluationCase.Application.Features.Commands.CreateProduct;
using EvaluationCase.Application.Features.Commands.SetInActiveCampaign;
using EvaluationCase.Application.Wrappers;
using EvaluationCase.Domain.Entities;
using EvaluationCase.Domain.Enums;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Xunit;

namespace EvaluationCase.Test
{
    public class MethodTests
    {
        [Fact]
        public async Task CreateProduct()
        {
            using var client = new TestClientProvider().TestClient;

            var result = await client.PostAsJsonAsync("/api/product",
            new CreateProductCommand()
            {
                Name = "Apple Telefon",
                CategoryId = 1,
                CategoryName = "Telefon",
                Price = 9000,
                Brand = "Apple",
                Code = "Phone_1",
                ProductGroupCodes = new List<string> { "november-dicount-dates" }
            });

            result.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
        [Fact]
        public async Task CreateCampaign()
        {
            using var client = new TestClientProvider().TestClient;

            var result = await client.PostAsJsonAsync("/api/campaign",
            new CreateCampaignCommand()
            {
                Name = "kedimamasi-sepette-50-tl-indirim",
                IsActive = true,
                MinimumBasketPrice = 300,
                Discount = 50,
                CampaignType = 0,
                DiscountType = 0,
                AffectedCount = 9999,
                CampaignTypeValue = new List<string> { "kedimamasi-sepette-50-tl-indirim" },
                EndDate = DateTime.UtcNow.AddDays(5),
                StartDate = DateTime.UtcNow
            });
            result.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GetAllProducts()
        {
            using var client = new TestClientProvider().TestClient;

            var result = await client.GetAsync("/api/product");
            result.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GetAllCampaigns()
        {
            using var client = new TestClientProvider().TestClient;

            var result = await client.GetAsync("/api/campaign");
            result.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task SetInactiveCampaign()
        {
            using var client = new TestClientProvider().TestClient;

            //create new campaign
            var result = await client.PostAsJsonAsync("/api/campaign",
             new CreateCampaignCommand()
             {
                 Name = "test-50-tl-indirim",
                 IsActive = true,
                 MinimumBasketPrice = 100,
                 Discount = 10,
                 CampaignType = CampaignType.Category,
                 DiscountType = CampaignDiscountType.Rate,
                 AffectedCount = 1,
                 CampaignTypeValue = new List<string> { "test-50-tl-indirim" },
                 EndDate = DateTime.UtcNow.AddDays(5),
                 StartDate = DateTime.UtcNow
             });

            result.EnsureSuccessStatusCode();
            var contents = await result.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            //set false
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<Guid>>(contents);
            var resultInactive = await client.PostAsJsonAsync("/api/campaign/set-inactive-campaign", new SetInActiveCampaignCommand()
            {
                Id = response.Value
            });

            resultInactive.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, resultInactive.StatusCode);
        }
    }
}