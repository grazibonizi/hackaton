using Hackaton.Boilerplate.Abstraction.Business;
using Hackaton.Boilerplate.Abstraction.Internals;
using Hackaton.Boilerplate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Hackaton.Boilerplate.API.Controllers
{
    [Authorize]
    public class Test2Controller : ApiController
    {
        private readonly ILogger _logger;
        private readonly IBusinessAsync<News> _business;

        public Test2Controller(
            ILogger logger,
            IBusinessAsync<News> business
        )
        {
            _logger = logger;
            _business = business;
        }

        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                var itens = await _business.Get(w => w.Active);

                if (itens != null && itens.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, itens);
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
    }
}
