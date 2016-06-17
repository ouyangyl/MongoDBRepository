using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MongoDB.Bson.Serialization.Attributes;

namespace DX.Listing.Merchant.Data.Dto
{
    /// <summary>
    /// 供应商修改价格的申请记录
    /// </summary>
    public class PriceChangeDto
    {
        /// <summary>
        /// 原始供货价
        /// </summary>
        [BsonElement("originalPrice")]
        public double OriginalPrice { get; set; }

        /// <summary>
        /// 目标供货价
        /// </summary>
        [BsonElement("targetPrice")]
        public double TargetPrice { get; set; }

        /// <summary>
        /// 原始销售价
        /// </summary>
        [BsonElement("originalSellPrice")]
        public double OriginalSellPrice { get; set; }

        /// <summary>
        /// 目标销售价
        /// </summary>
        [BsonElement("targetSellPrice")]
        public double TargetSellPrice { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 修改人用户名
        /// </summary>
        [BsonElement("modifiedBy")]
        public string ModifiedBy { get; set; }
    }
}
