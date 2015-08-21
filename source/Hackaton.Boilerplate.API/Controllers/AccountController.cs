using Hackaton.Boilerplate.Abstraction.Business;
using Hackaton.Boilerplate.Abstraction.Internals;
using Hackaton.Boilerplate.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Hackaton.Boilerplate.API.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IBusinessAsync<UserAccount> _userAccountBusiness;

        public AccountController(
            ILogger logger,
            IBusinessAsync<UserAccount> userAccountBusiness
        )
        {
            _logger = logger;
            _userAccountBusiness = userAccountBusiness;
        }

        // GET: api/Account/5
        public async Task<HttpResponseMessage> Get(string id)
        {
            try
            {
                ObjectId oId = ObjectId.Parse(id);

                IList<UserAccount> accounts;
                if (oId == ObjectId.Empty)
                {
                    var loggedUser = HttpContext.Current.User.Identity.Name;
                    accounts = await _userAccountBusiness.GetAll();
                }
                else
                {
                    accounts = await _userAccountBusiness.Get(
                        account => account.Id.Equals(oId)
                    );
                }

                if (accounts != null && accounts.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, accounts);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error calling business get method!", ex);
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        // POST: api/Account
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Post([FromBody]UserAccount value)
        {
            try
            {
                await _userAccountBusiness.Insert(value);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.Error("Error calling business get method!", ex);
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        // PUT: api/Account/5
        public async Task<HttpResponseMessage> Put(ObjectId id, [FromBody]UserAccount value)
        {
            try
            {
                value.Id = id;
                await _userAccountBusiness.Update(value);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.Error("Error calling business get method!", ex);
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        // DELETE: api/Account/5
        public async Task<HttpResponseMessage> Delete(ObjectId id)
        {
            try
            {
                await _userAccountBusiness.Delete(
                    new UserAccount
                    {
                        Id = id
                    }
                );
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.Error("Error calling business get method!", ex);
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}
