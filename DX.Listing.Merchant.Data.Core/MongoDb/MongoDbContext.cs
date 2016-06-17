using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using Common.Logging;

namespace DX.Listing.Merchant.Data.Core.MongoDb
{
    public class MongoDbContext
    {
        /// <summary>
        /// MongoUrl，由Spring注入。
        /// </summary>
        public MongoUrl Url { get; set; }

        /// <summary>
        /// MongoClient，由Spring注入。
        /// </summary>
        public MongoClient Client { get; set; }

        /// <summary>
        /// 获取MongoServer。
        /// </summary>
        public MongoServer Server
        {
            get
            {
                return this.Client.GetServer();
            }
        }

        /// <summary>
        /// 获取MongoDatabase。
        /// </summary>
        public MongoDatabase Database
        {
            get
            {
                return this.Server.GetDatabase(this.Url.DatabaseName);
            }
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public MongoCollection<T> GetCollection<T>(string collectionName)
        {
            return this.Database.GetCollection<T>(collectionName);
        }
    }
}
