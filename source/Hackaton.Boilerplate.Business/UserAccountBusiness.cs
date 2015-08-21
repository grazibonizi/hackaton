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

namespace Hackaton.Boilerplate.Business
{
    public class UserAccountBusiness : 
        BusinessAsync<UserAccount>, 
        IBusinessAsync<UserAccount>
    {
        public UserAccountBusiness(
            IRepositoryAsync<UserAccount> repository,
            ILogger logger)
            : base(repository, logger)
        {

        }
    }
}
