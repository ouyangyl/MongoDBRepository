using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace DX.Listing.Merchant.Data.Dto
{
    public interface IAggregateRoot
    {
        ObjectId Id { get; set; }
    }
}
