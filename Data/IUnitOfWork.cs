using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IUnitOfWork
    {
        Task<T> Get<T>(Guid id);
        Task<IEnumerable<T>> GetAll<T>();
    }
}