using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DX.Listing.Merchant.Data.Dto;
using DX.Listing.Merchant.Data.Core.MongoDb;
using MongoDB.Bson;
//using MongoDB.Driver;
//using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace DX.Listing.Merchant.Data.Dal.Impl
{
    public class ProductDal : MongoDbRepository<ProductDto>, IProductDal
    {
        public ProductDal(MongoDbContext context, string collectionName)
            : base(context, collectionName)
        {
            
        }

        public IEnumerable<ProductDto> DynamicLoad(DX.Listing.Merchant.Data.Dto.ProductQueryDto param,int pageIndex,int pageSize,out int rowCount)
        {
            //var query = this as IQueryable<ProductDto>;
            var query = Collection.AsQueryable();
            if (param.Ids != null && param.Ids.Count > 0)
            {
                ObjectId[] ids = param.Ids.Select(o => ObjectId.Parse(o)).ToArray();
                query = query.Where(p=>ids.Contains(p.Id));
            }

            if (!string.IsNullOrEmpty(param.SupplierCode))
            {
                query = query.Where(p=>p.SupplierCode==param.SupplierCode);
            }

            if (param.ListingSkus != null && param.ListingSkus.Count > 0)
            {
                query = query.Where(p=>param.ListingSkus.Contains(p.ListingSku));
            }

            if (!string.IsNullOrEmpty(param.SalesState))
            {
                query = query.Where(p => p.SalesState == param.SalesState);
            }
            

            rowCount = query.Count();
            query = query.OrderBy(p=>p.ListingSku).Skip(pageIndex * pageSize).Take(pageSize);

            var result = query.ToList();
            return result;
        }
    }
}
