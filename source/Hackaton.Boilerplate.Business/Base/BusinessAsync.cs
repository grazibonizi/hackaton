using Hackaton.Boilerplate.Abstraction.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Hackaton.Boilerplate.Abstraction.DAO;
using Hackaton.Boilerplate.Abstraction.Internals;
using Hackaton.Boilerplate.Constraint;

namespace Hackaton.Boilerplate.Business.Base
{
    public class BusinessAsync<T> : IBusinessAsync<T>
        where T : class
    {
        private readonly IRepositoryAsync<T> _repository;
        private readonly ILogger _logger;

        public BusinessAsync(
            IRepositoryAsync<T> repository,
            ILogger logger
        )
        {
            logger.Debug("constructing business!");

            Check.IsNull(() => repository);
            Check.IsNull(() => logger);

            logger.Debug("validation passed!");
            _repository = repository;
            _logger = logger;
        }

        public Task Delete(params T[] itens)
        {
            Check.IsNull(() => itens);

            _logger.Debug("Delete called!");
            return _repository.Delete(itens);
        }

        public Task<IList<T>> Get(Expression<Func<T, bool>> where)
        {
            Check.IsNull(() => where);

            _logger.Debug("Get called!");
            return _repository.Get(where);
        }

        public Task<IList<T>> GetAll()
        {
            _logger.Debug("GetAll called!");
            return _repository.GetAll();
        }

        public Task Insert(params T[] itens)
        {
            Check.IsNull(() => itens);

            _logger.Debug("Insert called!");
            return _repository.Insert(itens);
        }

        public Task Update(params T[] itens)
        {
            Check.IsNull(() => itens);

            _logger.Debug("Update called!");
            return _repository.Update(itens);
        }

    }
}
