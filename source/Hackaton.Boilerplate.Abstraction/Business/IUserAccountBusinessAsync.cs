using Hackaton.Boilerplate.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton.Boilerplate.Abstraction.Business
{
    public interface IUserAccountBusinessAsync
    {
        Task<bool> EmailAlreadyExists(string email);
        Task<bool> EmailAlreadyExists(string email, ObjectId id);
        Task<UserAccount> IdentifyUser(string email, string password);
    }
}
