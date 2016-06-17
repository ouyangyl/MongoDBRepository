using System;
using System.ComponentModel;
using MongoDB.Bson.Serialization.Attributes;

namespace DX.Listing.Merchant.Data.Dto
{
    public enum ModifyType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown,

        /// <summary>
        /// 新品上架审核通过
        /// </summary>
        ApprovePass,

        /// <summary>
        /// 新品上架审核不通过
        /// </summary>
        ApproveNoPass,

        /// <summary>
        /// 修改
        /// </summary>
        Modify,

        /// <summary>
        /// 开卖操作
        /// </summary>
        OnSale,

        /// <summary>
        /// 下架
        /// </summary>
        OffSale,

        /// <summary>
        /// 停卖操作
        /// </summary>
        Pause,

        /// <summary>
        /// 失效
        /// </summary>
        Invalid,

        /// <summary>
        /// 发布公告
        /// </summary>
        [Description("创建")]
        NewsAdd,

        /// <summary>
        /// 修改公告
        /// </summary>
        [Description("修改")]
        NewsModify
    }

    public class ModificationRecordDto
    {
        /// <summary>
        /// 修改人用户名
        /// </summary>
        [BsonElement("modifiedBy")]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 是否成员
        /// </summary>
        [BsonElement("isStaff")]
        public bool IsStaff { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 修改日志
        /// </summary>
        [BsonElement("note")]
        public string Note { get; set; }

        /// <summary>
        /// 修改类型
        /// </summary>
        [BsonElement("modifyType")]
        public string ModifyType { get; set; }
    }
}
