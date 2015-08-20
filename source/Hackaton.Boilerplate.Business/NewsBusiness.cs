using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hackaton.Boilerplate.Abstraction.Business;
using Hackaton.Boilerplate.Model;
using Hackaton.Boilerplate.Abstraction.DAO;
using Hackaton.Boilerplate.Constraint;
using Hackaton.Boilerplate.Abstraction.Internals;

namespace Hackaton.Boilerplate.Business
{
    public class NewsBusiness : IBusinessAsync<News>
    {
        private readonly IRepositoryAsync<News> _repository;
        private readonly ILogger _logger;

        public NewsBusiness(
            IRepositoryAsync<News> repository,
            ILogger logger
        )
        {
            logger.Debug("constructing NewsBusiness!");

            Check.IsNull(() => repository);
            Check.IsNull(() => logger);

            logger.Debug("validation passed!");
            _repository = repository;
            _logger = logger;

        }

        public Task Delete(params News[] itens)
        {
            Check.IsNull(() => itens);

            _logger.Debug("Delete called!");
            return _repository.Delete(itens);
        }

        public Task<IList<News>> Get(Expression<Func<News, bool>> where)
        {
            Check.IsNull(() => where);

            _logger.Debug("Get called!");
            return _repository.Get(where);
        }

        public Task<IList<News>> GetAll()
        {
            _logger.Debug("GetAll called!");
            return _repository.GetAll();
        }

        public Task Insert(params News[] itens)
        {
            Check.IsNull(() => itens);

            _logger.Debug("Insert called!");
            return _repository.Insert(itens);
        }

        public Task Update(params News[] itens)
        {
            Check.IsNull(() => itens);

            _logger.Debug("Update called!");
            return _repository.Update(itens);
        }
    }
}
