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

namespace Hackaton.Boilerplate.Business
{
    public class UserAccountBusiness : BusinessAsync<UserAccount>,
        IBusinessAsync<UserAccount>, IUserAccountBusinessAsync
    {
        private readonly ILogger _logger;
        private readonly IRepositoryAsync<UserAccount> _repository;

        public UserAccountBusiness(
            IRepositoryAsync<UserAccount> repository,
            ILogger logger
        ) : base(repository, logger)
        {
            _logger = logger;
            _repository = repository;
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
                && ua.Id != id
            );
            return emailFound != null && emailFound.Count > 0;
        }

        public async Task<UserAccount> IdentifyUser(string email, string password)
        {
            var sha512 = SHA512.Create();
            var hash = BitConverter.ToString(
                sha512.ComputeHash(
                    Encoding.Default.GetBytes(
                        string.Format("{0}{1}", password, GeneralConstants.SALT)
                    )
                )
            );

            var userIdentified = await _repository.Get(
                ua =>
                    ua.Email == email
                && ua.Password == hash
            );

            return userIdentified.SingleOrDefault();
        }
    }
}
