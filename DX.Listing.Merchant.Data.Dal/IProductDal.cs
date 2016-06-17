using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DX.Listing.Merchant.Data.Dto;

namespace DX.Listing.Merchant.Data.Dal
{
    public interface IProductDal : DX.Listing.Merchant.Data.Core.IRepository<ProductDto> 
    {
        IEnumerable<ProductDto> DynamicLoad(DX.Listing.Merchant.Data.Dto.ProductQueryDto param, int pageIndex, int pageSize, out int rowCount);
    }
}
