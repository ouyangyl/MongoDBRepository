using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DX.Listing.Merchant.Data.Dto
{
    /// <summary>
    /// 查询用
    /// </summary>
    public class ProductQueryDto
    {
        /// <summary>
        /// 供应商Code
        /// </summary>
        public string SupplierCode { get; set; }

        /// <summary>
        /// Ids
        /// </summary>
        public IList<string> Ids { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public IList<string> Statuses { get; set; }

        /// <summary>
        /// 销售状态
        /// </summary>
        public IList<string> SalesStates { get; set; }

        /// <summary>
        /// BaseSkus
        /// </summary>
        public IList<int> BaseSkus { get; set; }

        /// <summary>
        /// ListingSkus
        /// </summary>
        public IList<string> ListingSkus { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Headline { get; set; }

        /// <summary>
        /// 创建时间-开始
        /// </summary>
        public DateTime StartCreate { get; set; }

        /// <summary>
        /// 创建时间-结束
        /// </summary>
        public DateTime EndCreate { get; set; }

        /// <summary>
        /// 销售状态
        /// </summary>
        public string SalesState { get; set; }

        /// <summary>
        /// 新品类型
        /// string.Empty-所有，new-上新品，follow-跟卖
        /// </summary>
        public string NewType { get; set; }
    }
}
