using Moq;
using System;
using Xunit;
using HbCampaignModule.Domain.Entities;
using HbCampaignModule.Domain.Interfaces.ModelIF;
using HbCampaignModule.Domain.Model;
using HbCampaignModule.Domain.Results;
using HbCampaignModule.Infrastructure.Context;
using HbCampaignModule.WebApi.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HbCampaignModule.Test
{
    [Collection("Database")]
    public class ChecksCommands
    {
        const string badRequest = "Microsoft.AspNetCore.Mvc.BadRequestObjectResult";
        const string okRequest = "Microsoft.AspNetCore.Mvc.OkObjectResult";
        Mock<IProductRepository> mockProduct = new Mock<IProductRepository>();
        Mock<IOrderRepository> mockOrder = new Mock<IOrderRepository>();
        Mock<ICampaignRepository> mockCampaign = new Mock<ICampaignRepository>();
        Mock<ILogger<CampaignController>> mockLogger = new Mock<ILogger<CampaignController>>();
        private PostgreSqlDbContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<PostgreSqlDbContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            var context = new PostgreSqlDbContext(options);

            var product1 = new Product { Id = 1, ProductCode = "P1", Price = 100, Stock=100,  CreatedDate = DateTime.Now  };
            var product2 = new Product { Id = 2, ProductCode = "P2", Price = 500, Stock = 3, CreatedDate = DateTime.Now };

            var campaign1 = new Campaign { Id = 1, Name = "C1", ProductCode= "P1", Duration = 10, PriceManipulationLimit =100,TargetSalesCount =100,IsActive=true, CreatedDate=DateTime.Now};

            context.Products.Add(product1);
            context.Products.Add(product2);

            context.Campaigns.Add(campaign1);

            context.Orders.Add(new Order { Id = 1, Product = product1, Quantity = 3, Campaign = null, CreatedDate = DateTime.Now });
            context.Orders.Add(new Order { Id = 2, Product = product2, Quantity = 1, Campaign = campaign1, CreatedDate = DateTime.Now });

            context.SaveChanges();

            return context;
        }

        [Theory]
        [InlineData("P145645")]
        [InlineData("C145645")]
        [InlineData("P0")]
        [InlineData("p999")]
        public void CheckNotExistsProduct(string productCode)
        {
            var mockProduct = new Mock<IProductRepository>();
            mockProduct.Setup(x => x.CheckProduct(productCode)).Returns(new ErrorResult());
            using (var context = GetContextWithData())
            using (var controller = new CampaignController(context, mockProduct.Object,mockOrder.Object,mockCampaign.Object, mockLogger.Object))
            {   
                var result = controller.GetProductInfo(productCode);
                Assert.NotNull(result);
                Assert.Equal(result.ToString(), badRequest);
            }
        }

        [Theory]
        [InlineData("P1")]
        [InlineData("P2")]
        public void CheckExistsProduct(string productCode)
        {
            mockProduct.Setup(x => x.CheckProduct(productCode)).Returns(new SuccessResult());
            mockProduct.Setup(x => x.GetProductInfo(productCode)).Returns(new SuccessDataResult<ProductDto>());
            using (var context = GetContextWithData())
            using (var controller = new CampaignController(context, mockProduct.Object, mockOrder.Object, mockCampaign.Object, mockLogger.Object))
            {
                var result = controller.GetProductInfo(productCode);
                Assert.NotNull(result);
                Assert.Equal(result.ToString(), okRequest);
            }
        }

        [Theory]
        [InlineData("P1", 100, 1000)]
        public void FailureCreateProduct(string productCode, int price, int stock)
        {
            ProductDto product = new ProductDto {ProductCode = productCode, Price = price, Stock = stock};
            mockProduct.Setup(x => x.ProductExists(productCode)).Returns(new SuccessResult());
            mockProduct.Setup(x => x.CreateProduct(product)).Returns(new ErrorDataResult<ProductDto>());
            using (var context = GetContextWithData())
            using (var controller = new CampaignController(context, mockProduct.Object, mockOrder.Object, mockCampaign.Object, mockLogger.Object))
            {
                var result = controller.CreateProduct(product);
                Assert.NotNull(result);
                Assert.Equal(result.ToString(), badRequest);
            }
        }

        [Theory]
        [InlineData("P10",100,1000)]
        public void SuccessCreateProduct(string productCode, int price, int stock)
        {
            ProductDto product = new ProductDto { ProductCode = productCode, Price = price, Stock = stock};
            mockProduct.Setup(x => x.ProductExists(productCode)).Returns(new SuccessResult());
            mockProduct.Setup(x => x.CreateProduct(product)).Returns(new SuccessDataResult<ProductDto>());
            using (var context = GetContextWithData())
            using (var controller = new CampaignController(context, mockProduct.Object, mockOrder.Object, mockCampaign.Object, mockLogger.Object))
            {
                var result = controller.CreateProduct(product);
                Assert.NotNull(result);
                Assert.Equal(result.ToString(), okRequest);
            }
        }


        [Theory]
        [InlineData("C21221")]
        public void CheckNotExistsCampaign(string name)
        {
            mockCampaign.Setup(x => x.CheckCampaign(name)).Returns(new ErrorResult());
            using (var context = GetContextWithData())
            using (var controller = new CampaignController(context, mockProduct.Object, mockOrder.Object, mockCampaign.Object, mockLogger.Object))
            {
                var result = controller.GetCampaignInfo(name);
                Assert.NotNull(result);
                Assert.Equal(result.ToString(), badRequest);
            }
        }

        [Theory]
        [InlineData("C1")]
        public void CheckExistsCampaign(string name)
        {
            mockCampaign.Setup(x => x.CheckCampaign(name)).Returns(new SuccessDataResult<CampaignDto>());
            mockCampaign.Setup(x => x.GetCampaignInfo(name)).Returns(new SuccessDataResult<CampaignDto>());

            using (var context = GetContextWithData())
            using (var controller = new CampaignController(context, mockProduct.Object, mockOrder.Object, mockCampaign.Object, mockLogger.Object))
            {
                var result = controller.GetCampaignInfo(name);
                Assert.NotNull(result);
                Assert.Equal(result.ToString(), okRequest);
            }
        }

        [Theory]
        [InlineData("C10", "P6", 10, 50, 100)]
        public void FailureCreateCampaignNotExistsProduct(string name, string productCode, int duration, int limit, int targetSalesCount)
        {
            CampaignDto campaign = new CampaignDto { Name = name, ProductCode = productCode, Duration = duration, PriceManipulationLimit = limit, TargetSalesCount = targetSalesCount };
            mockCampaign.Setup(x => x.CampaignExists(campaign)).Returns(new SuccessResult());
            mockCampaign.Setup(x => x.ProductExists(productCode)).Returns(new ErrorDataResult<CampaignDto>());
            using (var context = GetContextWithData())
            using (var controller = new CampaignController(context, mockProduct.Object, mockOrder.Object, mockCampaign.Object, mockLogger.Object))
            {
                var result = controller.CreateCampaign(campaign);
                Assert.NotNull(result);
                Assert.Equal(result.ToString(), badRequest);
            }
        }

        [Theory]
        [InlineData("C1", "P1", 10, 50, 100)]
        public void FailureCreateCampaignAlreadyExists(string name, string productCode, int duration, int limit, int targetSalesCount)
        {
            CampaignDto campaign = new CampaignDto { Name = name, ProductCode = productCode, Duration = duration, PriceManipulationLimit = limit, TargetSalesCount = targetSalesCount };
            mockCampaign.Setup(x => x.CampaignExists(campaign)).Returns(new ErrorResult());
            using (var context = GetContextWithData())
            using (var controller = new CampaignController(context, mockProduct.Object, mockOrder.Object, mockCampaign.Object, mockLogger.Object))
            {
                var result = controller.CreateCampaign(campaign);
                Assert.NotNull(result);
                Assert.Equal(result.ToString(), badRequest);
            }
        }

        [Theory]
        [InlineData("P254", 5)]
        public void FailureCreateOrderNotExistsProduct(string productCode, int quantity)
        {
            OrderDto order = new OrderDto { ProductCode = productCode, Quantity = quantity };
            mockOrder.Setup(x => x.CheckProduct(order.ProductCode)).Returns(new ErrorResult());
            using (var context = GetContextWithData())
            using (var controller = new CampaignController(context, mockProduct.Object, mockOrder.Object, mockCampaign.Object, mockLogger.Object))
            {
                var result = controller.CreateOrder(order);
                Assert.NotNull(result);
                Assert.Equal(result.ToString(), badRequest);
            }
        }

        [Theory]
        [InlineData("P2", 5)]
        public void FailureCreateOrderNotExistsStock(string productCode, int quantity)
        {
            OrderDto order = new OrderDto { ProductCode = productCode, Quantity = quantity };
            mockOrder.Setup(x => x.CheckProduct(order.ProductCode)).Returns(new SuccessResult());
            mockOrder.Setup(x => x.CheckStock(order)).Returns(new ErrorDataResult<OrderDto>());
            using (var context = GetContextWithData())
            using (var controller = new CampaignController(context, mockProduct.Object, mockOrder.Object, mockCampaign.Object, mockLogger.Object))
            {
                var result = controller.CreateOrder(order);
                Assert.NotNull(result);
                Assert.Equal(result.ToString(), badRequest);
            }
        }

        [Theory]
        [InlineData("P1", 2)]
        public void SuccessCreateOrder(string productCode, int quantity)
        {
            OrderDto order = new OrderDto { ProductCode = productCode, Quantity = quantity };
            mockOrder.Setup(x => x.CheckProduct(order.ProductCode)).Returns(new SuccessResult());
            mockOrder.Setup(x => x.CheckStock(order)).Returns(new SuccessDataResult<OrderDto>());
            mockOrder.Setup(x => x.CreateOrder(order)).Returns(new SuccessDataResult<OrderDto>());
            using (var context = GetContextWithData())
            using (var controller = new CampaignController(context, mockProduct.Object, mockOrder.Object, mockCampaign.Object, mockLogger.Object))
            {
                var result = controller.CreateOrder(order);
                Assert.NotNull(result);
                Assert.Equal(result.ToString(), okRequest);
            }
        }
    }
}

