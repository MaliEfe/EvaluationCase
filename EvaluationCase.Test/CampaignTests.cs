using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Features.Commands.AddBasket;
using EvaluationCase.Application.Features.Commands.CreateCampaign;
using EvaluationCase.Application.Features.Commands.CreateProduct;
using EvaluationCase.Application.Features.Queries.EvaluateCampaigns;
using EvaluationCase.Application.Wrappers;
using EvaluationCase.Domain.Entities;
using EvaluationCase.Domain.Enums;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace EvaluationCase.Test
{
    public class CampaignTests
    {
        [Fact]
        public async Task Fiction1()
        {
            //Sepette 250 TL üzerine ev dekorasyon kategorisinde 25 indirim
            using var client = new TestClientProvider().TestClient;

            //create new campaign
            var campaign = new CreateCampaignCommand()
            {
                Name = "250-TL-üzerine-ev-dekorasyon-kategorisinde-25-indirim",
                IsActive = true,
                MinimumBasketPrice = 250,
                Discount = 25,
                CampaignType = CampaignType.Category,
                DiscountType = CampaignDiscountType.Rate,
                AffectedCount = 9999,
                CampaignTypeValue = new List<string> { "Ev Dekorasyonu" },
                EndDate = DateTime.UtcNow.AddDays(5),
                StartDate = DateTime.UtcNow
            };
            var result = await client.PostAsJsonAsync("/api/campaign", campaign);

            result.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            //create product -1
            var product_1 = new CreateProductCommand()
            {
                Name = "Halı",
                CategoryId = 2,
                CategoryName = "Ev Dekorasyonu",
                Price = 250,
                Brand = "Carpet",
                Code = "Carpet_1"
            };
            var resultProduct_1 = await client.PostAsJsonAsync("/api/product", product_1);

            resultProduct_1.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, resultProduct_1.StatusCode);
            var productContent_1 = await resultProduct_1.Content.ReadAsStringAsync();
            var responseContent_1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<Guid>>(productContent_1);

            //create product -2
            var product_2 = new CreateProductCommand()
            {
                Name = "Sehpa",
                CategoryId = 2,
                CategoryName = "Ev Dekorasyonu",
                Price = 400,
                Brand = "Table",
                Code = "Sehpa_1"
            };
            var resultProduct_2 = await client.PostAsJsonAsync("/api/product", product_2);

            resultProduct_2.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, resultProduct_2.StatusCode);
            var productContent_2 = await resultProduct_2.Content.ReadAsStringAsync();
            var responseContent_2 = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<Guid>>(productContent_2);

            //create product -3
            var product_3 = new CreateProductCommand()
            {
                Name = "Samsung Telefon",
                CategoryId = 1,
                CategoryName = "Telefon",
                Price = 1000,
                Brand = "Samsung",
                Code = "Telefon_1"
            };
            var resultProduct_3 = await client.PostAsJsonAsync("/api/product", product_3);

            resultProduct_3.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, resultProduct_3.StatusCode);
            var productContent_3 = await resultProduct_3.Content.ReadAsStringAsync();
            var responseContent_3 = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<Guid>>(productContent_3);

            //Add Basket
            var result_addbasket = await client.PostAsJsonAsync("/api/basket",
            new AddBasketCommand
            {
                BasketItems = new List<BasketItemRequestViewDto> {
                new BasketItemRequestViewDto{ ProductId=responseContent_1.Value, Quantity = 1 },
                new BasketItemRequestViewDto{ ProductId=responseContent_2.Value, Quantity = 2 },
                new BasketItemRequestViewDto{ ProductId=responseContent_3.Value, Quantity = 1 } }

            });

            result_addbasket.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, result_addbasket.StatusCode);
            var basketContent = await result_addbasket.Content.ReadAsStringAsync();
            var basketResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<Guid>>(basketContent);

            //campaign evaluate
            var result_evaluate = await client.PostAsJsonAsync("/api/campaign/campaign-evaluate",
            new EvaluateCampaignQuery
            {
                BasketId = basketResponse.Value
            });

            result_evaluate.EnsureSuccessStatusCode();
            var evaluateContent = await result_evaluate.Content.ReadAsStringAsync();
            var evaluateResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<BasketEvaluateReponseViewDto>>(evaluateContent);

            //total discount
            var basketTotal = (product_1.Price * 1) + (product_2.Price * 2) + (product_3.Price * 1);
            var basketDiscount = (product_1.Price * campaign.Discount * 1 / 100) + (product_2.Price * campaign.Discount * 2 / 100);

            Assert.Equal(HttpStatusCode.OK, result_evaluate.StatusCode);
            Assert.Equal(basketTotal - basketDiscount, evaluateResponse.Value.TotalDiscountedPrice);

        }


        [Fact]
        public async Task Fiction2()
        {
            //Sepette Apple marka cep telefonlarında(cep telefonu bir kategoridir) % 13 indirim
            using var client = new TestClientProvider().TestClient;

            //create new campaign
            var campaign = new CreateCampaignCommand()
            {
                Name = "apple-cep-telefonlarinda-13-indirim",
                IsActive = true,
                MinimumBasketPrice = 0,
                Discount = 13,
                CampaignType = CampaignType.Brand,
                DiscountType = CampaignDiscountType.Rate,
                AffectedCount = 9999,
                CampaignTypeValue = new List<string> { "Apple" },
                EndDate = DateTime.UtcNow.AddDays(5),
                StartDate = DateTime.UtcNow
            };
            var result = await client.PostAsJsonAsync("/api/campaign", campaign);

            result.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            //create product -1
            var product_1 = new CreateProductCommand()
            {
                Name = "IPhone11",
                CategoryId = 1,
                CategoryName = "Telefon",
                Price = 9000,
                Brand = "Apple",
                Code = "IPhone11_1"
            };
            var resultProduct_1 = await client.PostAsJsonAsync("/api/product", product_1);

            resultProduct_1.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, resultProduct_1.StatusCode);
            var productContent_1 = await resultProduct_1.Content.ReadAsStringAsync();
            var responseContent_1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<Guid>>(productContent_1);

            //create product -2
            var product_2 = new CreateProductCommand()
            {
                Name = "SamsungSE",
                CategoryId = 1,
                CategoryName = "Telefon",
                Price = 5000,
                Brand = "Samsung",
                Code = "SamsungSE_1"
            };
            var resultProduct_2 = await client.PostAsJsonAsync("/api/product", product_2);

            resultProduct_2.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, resultProduct_2.StatusCode);
            var productContent_2 = await resultProduct_2.Content.ReadAsStringAsync();
            var responseContent_2 = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<Guid>>(productContent_2);


            //Add Basket
            var result_addbasket = await client.PostAsJsonAsync("/api/basket",
            new AddBasketCommand
            {
                BasketItems = new List<BasketItemRequestViewDto> {
                new BasketItemRequestViewDto{ ProductId=responseContent_1.Value, Quantity = 1 },
                new BasketItemRequestViewDto{ ProductId=responseContent_2.Value, Quantity = 1 }}

            });

            result_addbasket.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, result_addbasket.StatusCode);
            var basketContent = await result_addbasket.Content.ReadAsStringAsync();
            var basketResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<Guid>>(basketContent);

            //campaign evaluate
            var result_evaluate = await client.PostAsJsonAsync("/api/campaign/campaign-evaluate",
            new EvaluateCampaignQuery
            {
                BasketId = basketResponse.Value
            });

            result_evaluate.EnsureSuccessStatusCode();
            var evaluateContent = await result_evaluate.Content.ReadAsStringAsync();
            var evaluateResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<BasketEvaluateReponseViewDto>>(evaluateContent);

            //total discount
            var basketTotal = (product_1.Price * 1) + (product_2.Price * 1);
            var basketDiscount = basketTotal - (product_1.Price * campaign.Discount * 1 / 100);

            Assert.Equal(HttpStatusCode.OK, result_evaluate.StatusCode);
            Assert.Equal(basketDiscount, evaluateResponse.Value.TotalDiscountedPrice);

        }

        [Fact]
        public async Task Fiction3()
        {
            //Ürün grup linki; http://domain..../kampanyalar/kedimamasi-sepette-50-tl-indirim
            using var client = new TestClientProvider().TestClient;

            //create new campaign
            var campaign = new CreateCampaignCommand()
            {
                Name = "kedimamasi-sepette-50-tl-indirim",
                IsActive = true,
                MinimumBasketPrice = 300,
                Discount = 50,
                CampaignType = CampaignType.ProductGroup,
                DiscountType = CampaignDiscountType.Amount,
                AffectedCount = 9999,
                CampaignTypeValue = new List<string> { "kedimamasi-sepette-50-tl-indirim" },
                EndDate = DateTime.UtcNow.AddDays(5),
                StartDate = DateTime.UtcNow
            };
            var result = await client.PostAsJsonAsync("/api/campaign", campaign);

            result.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            //create product -1
            var product_1 = new CreateProductCommand()
            {
                Name = "Whiskas Mama",
                CategoryId = 3,
                CategoryName = "Kedi Maması",
                Price = 350,
                Brand = "Whiskas",
                Code = "Whiskas_1",
                ProductGroupCodes = new List<string> { "kedimamasi-sepette-50-tl-indirim" }
            };
            var resultProduct_1 = await client.PostAsJsonAsync("/api/product", product_1);

            resultProduct_1.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, resultProduct_1.StatusCode);
            var productContent_1 = await resultProduct_1.Content.ReadAsStringAsync();
            var responseContent_1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<Guid>>(productContent_1);

            //create product -2
            var product_2 = new CreateProductCommand()
            {
                Name = "Proline Mama",
                CategoryId = 3,
                CategoryName = "Kedi Maması",
                Price = 500,
                Brand = "Proline",
                Code = "Proline_1",
                ProductGroupCodes = new List<string> { "kedimamasi-haftasonu-indirim" }
            };
            var resultProduct_2 = await client.PostAsJsonAsync("/api/product", product_2);

            resultProduct_2.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, resultProduct_2.StatusCode);
            var productContent_2 = await resultProduct_2.Content.ReadAsStringAsync();
            var responseContent_2 = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<Guid>>(productContent_2);

            //create product -3
            var product_3 = new CreateProductCommand()
            {
                Name = "Fix Mama",
                CategoryId = 3,
                CategoryName = "Kedi Maması",
                Price = 200,
                Brand = "Fix",
                Code = "Fix_1",
                ProductGroupCodes = new List<string> { "kedimamasi-sepette-50-tl-indirim" }
            };
            var resultProduct_3 = await client.PostAsJsonAsync("/api/product", product_3);

            resultProduct_3.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, resultProduct_3.StatusCode);
            var productContent_3 = await resultProduct_3.Content.ReadAsStringAsync();
            var responseContent_3 = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<Guid>>(productContent_3);

            //Add Basket
            var result_addbasket = await client.PostAsJsonAsync("/api/basket",
            new AddBasketCommand
            {
                BasketItems = new List<BasketItemRequestViewDto> {
                new BasketItemRequestViewDto{ ProductId=responseContent_1.Value, Quantity = 1 },
                new BasketItemRequestViewDto{ ProductId=responseContent_2.Value, Quantity = 1 },
                new BasketItemRequestViewDto{ ProductId=responseContent_3.Value, Quantity = 1 }
                }

            });

            result_addbasket.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, result_addbasket.StatusCode);
            var basketContent = await result_addbasket.Content.ReadAsStringAsync();
            var basketResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<Guid>>(basketContent);

            //campaign evaluate
            var result_evaluate = await client.PostAsJsonAsync("/api/campaign/campaign-evaluate",
            new EvaluateCampaignQuery
            {
                BasketId = basketResponse.Value
            });

            result_evaluate.EnsureSuccessStatusCode();
            var evaluateContent = await result_evaluate.Content.ReadAsStringAsync();
            var evaluateResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<BasketEvaluateReponseViewDto>>(evaluateContent);

            //total discount
            var basketTotal = (product_1.Price * 1) + (product_2.Price * 1) + (product_3.Price * 1);
            var basketDiscount = campaign.Discount;

            Assert.Equal(HttpStatusCode.OK, result_evaluate.StatusCode);
            Assert.Equal(basketTotal - basketDiscount, evaluateResponse.Value.TotalDiscountedPrice);

        }

        [Fact]
        public async Task Fiction4()
        {
            //Sepette 2000 TL üzeri alışverişinde 1 adet samsung kulaklık hediye (%100 indirim) 
            using var client = new TestClientProvider().TestClient;

            //create new campaign
            var campaign = new CreateCampaignCommand()
            {
                Name = "samsung-kulaklik-hediye",
                IsActive = true,
                MinimumBasketPrice = 2000,
                Discount = 100,
                CampaignType = CampaignType.ProductGroup,
                DiscountType = CampaignDiscountType.Rate,
                AffectedCount = 1,
                CampaignTypeValue = new List<string> { "elektronik" },
                EndDate = DateTime.UtcNow.AddDays(5),
                StartDate = DateTime.UtcNow
            };
            var result = await client.PostAsJsonAsync("/api/campaign", campaign);

            result.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            //create product -1
            var product_1 = new CreateProductCommand()
            {
                Name = "Samsung HS-1",
                CategoryId = 4,
                CategoryName = "Kulaklık",
                Price = 60,
                Brand = "Samsung",
                Code = "Samsung_1",
                ProductGroupCodes = new List<string> { "elektronik" }
            };
            var resultProduct_1 = await client.PostAsJsonAsync("/api/product", product_1);

            resultProduct_1.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, resultProduct_1.StatusCode);
            var productContent_1 = await resultProduct_1.Content.ReadAsStringAsync();
            var responseContent_1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<Guid>>(productContent_1);

            //create product -2
            var product_2 = new CreateProductCommand()
            {
                Name = "Samsung HS-2",
                CategoryId = 4,
                CategoryName = "Kulaklık",
                Price = 100,
                Brand = "Samsung",
                Code = "Samsung_2",
                ProductGroupCodes = new List<string> { "elektronik" }
            };
            var resultProduct_2 = await client.PostAsJsonAsync("/api/product", product_2);

            resultProduct_2.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, resultProduct_2.StatusCode);
            var productContent_2 = await resultProduct_2.Content.ReadAsStringAsync();
            var responseContent_2 = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<Guid>>(productContent_2);

            //create product -3
            var product_3 = new CreateProductCommand()
            {
                Name = "HardDisk",
                CategoryId = 5,
                CategoryName = "Bilgisayar",
                Price = 1700,
                Brand = "Hp",
                Code = "HardDisk_1",
                ProductGroupCodes = new List<string> { "elektronik" }
            };
            var resultProduct_3 = await client.PostAsJsonAsync("/api/product", product_3);

            resultProduct_3.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, resultProduct_3.StatusCode);
            var productContent_3 = await resultProduct_3.Content.ReadAsStringAsync();
            var responseContent_3 = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<Guid>>(productContent_3);

            //Add Basket
            var result_addbasket = await client.PostAsJsonAsync("/api/basket",
            new AddBasketCommand
            {
                BasketItems = new List<BasketItemRequestViewDto> {
                new BasketItemRequestViewDto{ ProductId=responseContent_1.Value, Quantity = 1 },
                new BasketItemRequestViewDto{ ProductId=responseContent_2.Value, Quantity = 1 },
                new BasketItemRequestViewDto{ ProductId=responseContent_3.Value, Quantity = 1 }
                }

            });

            result_addbasket.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, result_addbasket.StatusCode);
            var basketContent = await result_addbasket.Content.ReadAsStringAsync();
            var basketResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<Guid>>(basketContent);

            //campaign evaluate
            var result_evaluate = await client.PostAsJsonAsync("/api/campaign/campaign-evaluate",
            new EvaluateCampaignQuery
            {
                BasketId = basketResponse.Value
            });

            result_evaluate.EnsureSuccessStatusCode();
            var evaluateContent = await result_evaluate.Content.ReadAsStringAsync();
            var evaluateResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResponse<BasketEvaluateReponseViewDto>>(evaluateContent);

            //total discount
            var basketTotal = (product_1.Price * 1) + (product_2.Price * 1) + (product_3.Price * 1);

            Assert.Equal(HttpStatusCode.OK, result_evaluate.StatusCode);
            Assert.Equal(basketTotal, evaluateResponse.Value.TotalDiscountedPrice);

        }
    }
}
