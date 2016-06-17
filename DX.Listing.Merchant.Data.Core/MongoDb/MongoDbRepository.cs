using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using Common.Logging;
using DX.Listing.Merchant.Data.Dto;

namespace DX.Listing.Merchant.Data.Core.MongoDb
{
    public abstract class MongoDbRepository<T> : DX.Listing.Merchant.Data.Core.IRepository<T> 
        where T : class,IAggregateRoot
    {
        private readonly MongoDbContext context;

        private readonly string collectionName;

        /// <summary>
        /// 日志。由Spring注入。
        /// </summary
        public ILog Logger { get; set; }

        public ERManager ERManager { get; set; }

        public MongoCollection<T> Collection
        {
            get
            {
                return context.GetCollection<T>(collectionName);
            }
        }

        public MongoDbRepository(MongoDbContext context, string collectionName)
        {
            this.context = context;
            this.collectionName = collectionName;
        }

        public T GetById(ObjectId id)
        {
            var result = Collection.AsQueryable().FirstOrDefault(p => p.Id == id);

            return result;
        }

        public T Find(Expression<Func<T, bool>> queryPredicate)
        {
            return Collection.AsQueryable().FirstOrDefault(queryPredicate);
        }

        public IEnumerable<T> FindAll()
        {
            return FindAll(null);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> queryPredicate)
        {
            return FindAll(queryPredicate, null,null, SortOrder.Unspecified);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> queryPredicate, Expression<Func<T, T>> selector)
        {
            return FindAll(queryPredicate, selector, null, SortOrder.Unspecified);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> queryPredicate, Expression<Func<T, dynamic>> sortPredicate,SortOrder sortOrder)
        {
            var query = Collection.AsQueryable().Where(queryPredicate);
            if (sortPredicate != null)
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(sortPredicate);
                }
                else if (sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(sortPredicate);
                }
            }

            return query.ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> queryPredicate,Expression<Func<T,T>> selector,  Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            var query = Collection.AsQueryable();
            if(queryPredicate!=null)
            {
                query = query.Where(queryPredicate);
            }
            if (selector != null)
            {
                query = query.Select(selector);
            }
            if (sortPredicate != null)
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(sortPredicate);
                }
                else if (sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(sortPredicate);
                }
            }

            return query.ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> queryPredicate, Expression<Func<T, T>> selector, Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder,int pageIndex,int pageSize,out int rowCount)
        {
            if (pageIndex < 0 || pageSize < 0)
            {
                throw new ArgumentException("分页参数无效");
            }

            var query = Collection.AsQueryable();
            if (queryPredicate != null)
            {
                query = query.Where(queryPredicate);
            }
            if (selector != null)
            {
                query = query.Select(selector);
            }
            if (sortPredicate != null)
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(sortPredicate);
                }
                else if (sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(sortPredicate);
                }
            }
            else
            {
                throw new ArgumentNullException("分页查询时排序谓词不能为空");
            }

            rowCount = query.Count();

            return query.Skip(pageIndex*pageSize).Take(pageSize).ToList();
        }

        public IEnumerable<dynamic> FindAll(Expression<Func<T, bool>> queryPredicate, Expression<Func<T, dynamic>> selector)
        {
            var query = Collection.AsQueryable().Where(queryPredicate).Select(selector);

            return query.ToList();
        }

        public bool Insert(T entity)
        {
            return Collection.Insert<T>(entity).Ok;
        }

        public bool Update(T entity)
        {
            return Collection.Save<T>(entity).Ok;
        }

        public bool Delete(ObjectId id)
        {
            var query = Query<T>.Where(p=>p.Id==id);
            return Collection.Remove(query).Ok;
        }

        public bool Exist(Expression<Func<T, bool>> queryPredicate)
        {
            return Collection.AsQueryable().Count(queryPredicate)>0;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return Collection.AsQueryable().GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return Collection.AsQueryable().GetEnumerator();
        }

        public Type ElementType
        {
            get { return Collection.AsQueryable().ElementType; }
        }

        public Expression Expression
        {
            get { return Collection.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return Collection.AsQueryable().Provider; }
        }
    }
}
