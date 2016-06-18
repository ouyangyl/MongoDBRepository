using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DX.Listing.Merchant.Data.Core;
using DX.Listing.Merchant.Data.Core.MongoDb;
using DX.Listing.Merchant.Data.Dal;
using DX.Listing.Merchant.Data.Dto;
using DX.Listing.Merchant.Data.Dal.Impl;
using Spring.Core;
using Spring.Context;
using Spring.Context.Support;
using MongoDB.Bson;

namespace DalTest
{
    [TestClass]
    public class ProductDalTest : BaseTest
    {
        private IList<ProductDto> dtos = new List<ProductDto>();

        [TestCleanup]
        [Ignore]
        public void TestCleanup()
        {
            IProductDal target = ContextRegistry.GetContext().GetObject("ProductDal") as IProductDal;

            foreach (var dto in dtos)
            {
                target.Delete(dto.Id);
            }
        }

        [TestMethod]
        [Ignore]
        public void Insert()
        {
            IProductDal target = ContextRegistry.GetContext().GetObject("ProductDal") as IProductDal;
            //var dto = new ProductDto { ListingSku = System.Guid.NewGuid().ToString() };
            //bool bol = target.Insert(dto);
            //dtos.Add(dto);
            //Assert.IsTrue(bol);
            //Assert.IsNotNull(target.GetById(dto.Id));
        }

        [TestMethod]
        public void Find()
        {
            IProductDal target = ContextRegistry.GetContext().GetObject("ProductDal") as IProductDal;

            var sku = System.Guid.NewGuid().ToString();
            var dto = new ProductDto { ListingSku = sku, SalesState = "OnSale" };
            bool bol = target.Insert(dto);
            dtos.Add(dto);

            var actual = target.Find(p => p.ListingSku == sku);
            Assert.IsNotNull(actual);
            Assert.AreEqual("OnSale",actual.SalesState);

            actual = target.FirstOrDefault(p => p.ListingSku == sku);
            Assert.IsNotNull(actual);
            Assert.AreEqual("OnSale",actual.SalesState);
        }

        [TestMethod]
        [Ignore]
        public void FindAll()
        {
            IProductDal target = ContextRegistry.GetContext().GetObject("ProductDal") as IProductDal;

            var followSku = System.Guid.NewGuid().ToString();

            var sku = System.Guid.NewGuid().ToString();
            var dto = new ProductDto { ListingSku = sku, SalesState = "OnSale", FollowSku = followSku };
            bool bol = target.Insert(dto);
            dtos.Add(dto);

            var sku1 = System.Guid.NewGuid().ToString();
            var dto1 = new ProductDto { ListingSku = sku1, SalesState = "OffSale", FollowSku = followSku };
            bol = target.Insert(dto1);
            dtos.Add(dto1);

            var actual = target.FindAll(p => p.ListingSku == sku);
            Assert.AreEqual(1, actual.Count());

            actual = target.FindAll(p => p.ListingSku == followSku);
            Assert.AreEqual(0, actual.Count());

            actual = target.FindAll(p => p.FollowSku == followSku);
            Assert.AreEqual(2, actual.Count());

            var actual1 = target.FindAll(p => p.ListingSku == sku, p => p.ListingSku);
            Assert.IsTrue(actual1.First()==sku);

            actual = target.FindAll(p => p.ListingSku == sku, p => new ProductDto {ListingSku=p.ListingSku,FollowSku=p.FollowSku });
            Assert.IsNull(actual.First().SalesState);
            Assert.IsNotNull(actual.First().FollowSku);

            actual = target.FindAll(p => p.FollowSku == followSku,p=>p.ListingSku,SortOrder.Ascending);
            Assert.IsTrue(actual.First().ListingSku.CompareTo(actual.Last().ListingSku)<0);

            actual = target.FindAll(p => p.FollowSku == followSku, p => p.ListingSku, SortOrder.Descending);
            Assert.IsTrue(actual.First().ListingSku.CompareTo(actual.Last().ListingSku)>0);

            actual = target.FindAll(p => p.FollowSku == followSku, p => new ProductDto { ListingSku = p.ListingSku, FollowSku = p.FollowSku },
                p => p.ListingSku, SortOrder.Descending);
            Assert.IsNull(actual.First().SalesState);
            Assert.IsTrue(actual.First().ListingSku.CompareTo(actual.Last().ListingSku)>0);

            int rowCount = 0;
            actual = target.FindAll(p => p.FollowSku == followSku, null,p => p.ListingSku, SortOrder.Descending,0,1,out rowCount);
            Assert.AreEqual(1, actual.Count());

            actual = target.FindAll(p => p.FollowSku == followSku, null, p => p.ListingSku, SortOrder.Descending, 0, 2, out rowCount);
            Assert.AreEqual(2, actual.Count());


            actual = target.DynamicLoad(new ProductQueryDto { ListingSkus=new string[]{sku} }, 0, 2, out rowCount);
            Assert.AreEqual(1, actual.Count());

            actual = target.DynamicLoad(new ProductQueryDto { ListingSkus = new string[] { sku,sku1 } }, 0, 2, out rowCount);
            Assert.AreEqual(2, actual.Count());

            actual = target.DynamicLoad(new ProductQueryDto { ListingSkus = new string[] { sku },SalesState="OffSale" }, 0, 2, out rowCount);
            Assert.AreEqual(0, actual.Count());
        }
    }
}
