using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hackaton.Boilerplate.Abstraction.DAO
{
    public interface IRepositoryAsync<T>
    {
        Task Delete(params T[] itens);
        Task<IList<T>> Get(Expression<Func<T, bool>> where);
        Task<IList<T>> GetAll();
        Task Insert(params T[] itens);
        Task Update(params T[] itens);
    }
}
