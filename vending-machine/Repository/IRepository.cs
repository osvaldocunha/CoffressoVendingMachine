using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace vending_machine.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        Task<T> GetById(Expression<Func<T, bool>> Predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

  }
}
