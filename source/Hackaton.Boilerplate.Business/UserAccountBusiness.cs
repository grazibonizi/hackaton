using Hackaton.Boilerplate.Abstraction.Business;
using Hackaton.Boilerplate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Hackaton.Boilerplate.Business.Base;
using Hackaton.Boilerplate.Abstraction.DAO;
using Hackaton.Boilerplate.Abstraction.Internals;
using MongoDB.Bson;
using System.Security.Cryptography;
using Hackaton.Boilerplate.Constraints;
using Hackaton.Boilerplate.Constraint;
using Hackaton.Boilerplate.Exceptions;

namespace Hackaton.Boilerplate.Business
{
    public class UserAccountBusiness : BusinessAsync<UserAccount>,
        IBusinessAsync<UserAccount>, IUserAccountBusinessAsync
    {
        private readonly ILogger _logger;
        private readonly IRepositoryAsync<UserAccount> _repository;
        private readonly IHash _hasher;

        public UserAccountBusiness(
            IRepositoryAsync<UserAccount> repository,
            ILogger logger,
            IHash hasher
        ) : base(repository, logger)
        {
            _logger = logger;
            _repository = repository;
            _hasher = hasher;
        }

        public override async Task Insert(params UserAccount[] itens)
        {
            Check.IsNull(() => itens);
            _logger.Debug("Insert called!");

            foreach (var userAccount in itens)
            {
                var emailExists = await EmailAlreadyExists(userAccount.Email);
                if (emailExists)
                {
                    throw new EmailAlreadyExistsException();
                }

                _logger.Debug(
                    string.Format(
                        "Hashing password for {0}!",
                        userAccount.FirstName
                    )
                );

                userAccount.Password = _hasher.Hash(
                    userAccount.Password,
                    GeneralConstants.SALT
                );
            }

            await _repository.Insert(itens);
        }

        public async Task<bool> EmailAlreadyExists(string email)
        {
            var emailFound = await _repository.Get(ua => ua.Email == email);
            return emailFound != null && emailFound.Count > 0;
        }

        public async Task<bool> EmailAlreadyExists(string email, ObjectId id)
        {
            var emailFound = await _repository.Get(
                ua =>
                    ua.Email == email
                &&  ua.Id != id
            );
            return emailFound != null 
                && emailFound.Count > 0;
        }

        public async Task<UserAccount> IdentifyUser(string email, string password)
        {
            var hash = _hasher.Hash(
                password,
                GeneralConstants.SALT
            );

            var userIdentified = await _repository.Get(
                ua =>
                    ua.Email == email
                &&  ua.Password == hash
            );

            return userIdentified.SingleOrDefault();
        }
    }
}
