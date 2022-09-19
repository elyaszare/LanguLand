using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace _0_Framework.Domain
{
    //T:Class
    //TKey: Value,Key
    public interface IRepository<TKey, T>
    {
        T? Get(TKey id);

        List<T> Get();

        void Create(T entity);

        bool Exists(Expression<Func<T, bool>> expression);

        void SaveChanges();
    }
}
