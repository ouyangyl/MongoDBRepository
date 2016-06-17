using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DX.Listing.Merchant.Data.Dto
{
    /// <summary>
    /// 包装袋类型
    /// </summary>
    public enum PackageType
    {
        /// <summary>
        /// 所有
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 大包
        /// </summary>
        BigBag = 1,

        /// <summary>
        /// 小包
        /// </summary>
        SmallBag = 2,
    }

    /// <summary>
    /// 产品状态
    /// </summary>
    public enum ProductStatus
    {
        /// <summary>
        /// 所有
        /// </summary>
        [Description("所有")]
        Unknown = 0,

        /// <summary>
        /// 草稿
        /// </summary>
        [Description("草稿")]
        Draft,

        /// <summary>
        /// 正在申请
        /// </summary>
        [Description("正在申请")]
        Applying,

        /// <summary>
        /// 已申请
        /// </summary>
        [Description("已申请")]
        Applied,

        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("审核通过")]
        Approved,

        /// <summary>
        /// 审核不通过
        /// </summary>
        [Description("审核不通过")]
        UnApproved,

        /// <summary>
        /// 已推送
        /// </summary>
        [Description("已推送")]
        Published,

        /// <summary>
        /// 正在推送
        /// </summary>
        [Description("正在推送")]
        Publishing
    }

    public enum PublishState
    {
        /// <summary>
        /// 初始状态
        /// </summary>
        Init = 0,

        /// <summary>
        /// 正在推送产品图片
        /// </summary>
        PublishingImage,

        /// <summary>
        /// 图片已发布
        /// </summary>
        ImagePublished,

        /// <summary>
        /// 正在推送产品信息
        /// </summary>
        PublishingDocument,

        /// <summary>
        /// 产品已发布
        /// </summary>
        DocumentPublished,

        /// <summary>
        /// 正在关联ListingSKU
        /// </summary>
        AssociatingListingSku,

        /// <summary>
        /// 已关联ListingSKU
        /// </summary>
        ListingSkuAssociated,

        /// <summary>
        /// 正在快照
        /// </summary>
        Snapshotting,

        /// <summary>
        /// 快照完成
        /// </summary>
        Snapshotted,

        /// <summary>
        /// 正在推送价格
        /// </summary>
        PublishingPrice,

        /// <summary>
        /// 价格已推送
        /// </summary>
        PricePublished,

        /// <summary>
        /// 正在推送销售状态
        /// </summary>
        PublishingSalesState,

        /// <summary>
        /// 销售状态已推送
        /// </summary>
        SalesStatePublished,
    }

    public enum SalesState
    {
        /// <summary>
        /// 正常在售
        /// </summary>
        [Description("正常销售")]
        OnSale,

        /// <summary>
        /// 下架
        /// </summary>
        [Description("下架")]
        OffSale,

        /// <summary>
        /// 暂停销售
        /// </summary>
        [Description("暂停销售")]
        Pause,

        /// <summary>
        /// 失效
        /// </summary>
        [Description("失效")]
        Invalid,
        /// <summary>
        /// 【闻祖东 2014-7-23-113850】新增的此枚举的默认值。
        /// </summary>
        [Description("未知(默认)")]
        Unknown,
    }

    public enum DXProductStatus
    {
        /// <summary>
        ///   草稿
        /// </summary>
        [Description("草稿")]
        Draft,

        /// <summary>
        ///   正常销售
        /// </summary>
        [Description("正常销售")]
        Normal,

        /// <summary>
        ///   预售
        /// </summary>
        [Description("预售")]
        PreSales,

        /// <summary>
        ///   停售
        /// </summary>
        [Description("停售")]
        PauseSales,

        /// <summary>
        ///   下架
        /// </summary>
        [Description("下架")]
        OffShelf
    }

    public enum ProductFrom
    {
        /// <summary>
        /// 新品上架
        /// </summary>
        New,

        /// <summary>
        /// 跟卖DX的SKU
        /// </summary>
        FollowDX,

        /// <summary>
        /// 跟卖Listing的SKU
        /// </summary>
        FollowListing
    }

    [BsonIgnoreExtraElements(true)]
    public class ProductDto :  IAggregateRoot
    {
        public ProductDto()
        {
            this.Id = ObjectId.GenerateNewId();
            this.LastModified = new List<ModificationRecordDto>();
            this.PriceChanges = new List<PriceChangeDto>();
        }

        [BsonId]
        [BsonElement("_id")]
        [JsonProperty("_id")]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 新品状态
        /// </summary>
        [BsonElement("status")]
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// 推送进度，只对Status == Pushing有效
        /// </summary>
        [BsonElement("publishState")]
        [JsonProperty("publishState")]
        public string PublishState { get; set; }

        /// <summary>
        /// 销售状态。在售和停售。
        /// </summary>
        [BsonElement("salesState")]
        [JsonProperty("salesState")]
        public string SalesState { get; set; }

        /// <summary>
        /// 中文描述
        /// </summary>
        [BsonElement("description")]
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// 主分类ID
        /// </summary>
        [BsonElement("primaryCategoryId")]
        [JsonProperty("primaryCategoryId")]
        public int PrimaryCategoryId { get; set; }

        /// <summary>
        /// 短标题
        /// </summary>
        [BsonElement("shortHeadLine")]
        [JsonProperty("shortHeadLine")]
        public string ShortHeadLine { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [BsonElement("headline")]
        [JsonProperty("headline")]
        public string Headline { get; set; }

        /// <summary>
        /// 英文描述
        /// </summary>
        [BsonElement("introduction")]
        [JsonProperty("introduction")]
        public string Introduction { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonElement("createdTime")]
        [JsonProperty("createdTime")]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [BsonElement("lastModified")]
        [JsonProperty("lastModified")]
        public IList<ModificationRecordDto> LastModified { get; set; }

        /// <summary>
        /// 毛重
        /// </summary>
        [BsonElement("shippingWeight")]
        [JsonProperty("shippingWeight")]
        public double ShippingWeight { get; set; }

        /// <summary>
        /// 包装成本
        /// </summary>
        [BsonElement("packagingCost")]
        [JsonProperty("packagingCost")]
        public double? PackagingCost { get; set; }

        /////// <summary>
        /////// 手工处理费
        /////// </summary>
        ////[BsonElement("handlingCost")]
        ////[JsonProperty("handlingCost")]
        ////public double? HandlingCost { get; set; }

        /// <summary>
        /// 出厂指导价格
        /// </summary>
        [BsonElement("msrpPrice")]
        [JsonProperty("msrpPrice")]
        public double? MSRPPrice { get; set; }

        /// <summary>
        /// 供应商ID
        /// </summary>
        [BsonElement("supplierCode")]
        [JsonProperty("supplierCode")]
        public string SupplierCode { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        [BsonElement("spec")]
        [JsonProperty("spec")]
        public string Spec
        {
            get
            {
                var spec = string.Format(
                    "Dimensions: {0} in x {1} in x {2} in ({3} cm x {4} cm x {5} cm)<br />Weight: {6} oz ({7} g)",
                    this.Length.HasValue ? Math.Round(this.Length.Value * 0.3937, 2).ToString() : string.Empty,
                    this.Width.HasValue ? Math.Round(this.Width.Value * 0.3937, 2).ToString() : string.Empty,
                    this.Height.HasValue ? Math.Round(this.Height.Value * 0.3937, 2).ToString() : string.Empty,
                    this.Length.HasValue ? Math.Round(this.Length.Value, 2).ToString() : string.Empty,
                    this.Width.HasValue ? Math.Round(this.Width.Value, 2).ToString() : string.Empty,
                    this.Height.HasValue ? Math.Round(this.Height.Value, 2).ToString() : string.Empty,
                    this.Weight.HasValue ? Math.Round(this.Weight.Value * 0.0353, 2).ToString() : string.Empty,
                    this.Weight.HasValue ? Math.Round(this.Weight.Value, 2).ToString() : string.Empty);
                return spec;
            }
        }

        /// <summary>
        /// 报关名称
        /// </summary>
        [BsonElement("declarationName")]
        [JsonProperty("declarationName")]
        public string DeclarationName { get; set; }

        /// <summary>
        /// 包装后长
        /// </summary>
        [BsonElement("shippingLength")]
        [JsonProperty("shippingLength")]
        public double ShippingLength { get; set; }

        /// <summary>
        /// 包装后宽
        /// </summary>
        [BsonElement("shippingWidth")]
        [JsonProperty("shippingWidth")]
        public double ShippingWidth { get; set; }

        /// <summary>
        /// 包装后高
        /// </summary>
        [BsonElement("shippingHeight")]
        [JsonProperty("shippingHeight")]
        public double ShippingHeight { get; set; }

        /// <summary>
        /// SKU
        /// </summary>
        [BsonElement("baseSku")]
        [JsonProperty("baseSku")]
        public int BaseSku { get; set; }

        /// <summary>
        /// 跟卖SKU
        /// </summary>
        [BsonElement("listingSku")]
        [JsonProperty("listingSku")]
        public string ListingSku { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [BsonElement("imgs")]
        [JsonProperty("imgs")]
        public IList<string> Imgs { get; set; }

        /// <summary>
        /// 成本价
        /// </summary>
        [BsonElement("unitPrice")]
        [JsonProperty("unitPrice")]
        public double UnitPrice { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        [BsonElement("salesPrice")]
        [JsonProperty("salesPrice")]
        public double SalesPrice { get; set; }

        /// <summary>
        /// 前端url的一部分，由headline转换而来。将headline中除字母数字以为的符号替换成-就是UrlRewriteTitle
        /// </summary>
        [BsonElement("urlRewriteTitle")]
        [JsonProperty("urlRewriteTitle")]
        public string UrlRewriteTitle { get; set; }

        /// <summary>
        /// 改价记录
        /// </summary>
        [BsonElement("priceChanges")]
        [JsonProperty("priceChanges")]
        public IList<PriceChangeDto> PriceChanges { get; set; }

        /// <summary>
        /// 长
        /// </summary>
        [BsonElement("length")]
        [JsonProperty("length")]
        public double? Length { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        [BsonElement("width")]
        [JsonProperty("width")]
        public double? Width { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        [BsonElement("height")]
        [JsonProperty("height")]
        public double? Height { get; set; }

        /// <summary>
        /// 净重
        /// </summary>
        [BsonElement("weight")]
        [JsonProperty("weight")]
        public double? Weight { get; set; }

        /// <summary>
        /// 到货周期
        /// </summary>
        [BsonElement("arrivalPeriod")]
        [JsonProperty("arrivalPeriod")]
        public int ArrivalPeriod { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [BsonElement("brand")]
        [JsonProperty("brand")]
        public string Brand { get; set; }

        /// <summary>
        /// 跟卖的是哪个SKU
        /// </summary>
        [BsonElement("followSku")]
        [JsonProperty("followSku")]
        public string FollowSku { get; set; }
    }
}
