using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using DX.Listing.Merchant.Data.Dto;

namespace DX.Listing.Merchant.Data.Core
{
    public interface IRepository<T> : IEnumerable,IEnumerable<T>, IQueryable, IQueryable<T>
     where T : class,IAggregateRoot
    {
        T GetById(MongoDB.Bson.ObjectId id);
        T Find(Expression<Func<T, bool>> query);

        IEnumerable<T> FindAll();
        IEnumerable<T> FindAll(Expression<Func<T, bool>> queryPredicate);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> queryPredicate, Expression<Func<T, T>> selector);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> queryPredicate, Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> queryPredicate, Expression<Func<T, T>> selector, Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> queryPredicate, Expression<Func<T, T>> selector, Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder, int pageIndex, int pageSize, out int rowCount);
        IEnumerable<dynamic> FindAll(Expression<Func<T, bool>> queryPredicate, Expression<Func<T, dynamic>> selector);

        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(ObjectId id);

        bool Exist(Expression<Func<T, bool>> query);
    }
}
