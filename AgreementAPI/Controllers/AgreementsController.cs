using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

using System.Data.Entity;
using System.Linq;

using AgreementAPI.Models;
using AgreementAPI.DBContext;
using System.Net;

namespace AgreementAPI.Controllers
{
    public class AgreementsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "agreement", "agreement3" };
        }


        /// <summary>
        /// Retrieves one agreement
        /// --optional: simulate impact of new base rate
        /// </summary>
        /// <param name="id">The id of the agreement to be retrieved</param>
        /// <param name="simulatedRate">The new base rate code to simulate</param>
        /// <returns></returns>
        public HttpResponseMessage Get(int id, string simulatedRate="")
        {
            Agreement refAgreement;
            AgreementSimulator simAgreement; 

            using (var context = new AgreementContext())

            {
                var fullagreements = context.Agreements.Include(agr => agr.CustomerDetails).ToList();
                refAgreement = fullagreements.FirstOrDefault(agr => agr.Id == id);

            }

            if (refAgreement != null)
            {
                if (Request.GetQueryNameValuePairs().Count() == 0)
                    return new HttpResponseMessage()
                    {
                        Content = new ObjectContent<Agreement>(refAgreement,
                          Configuration.Formatters.JsonFormatter)
                    };
                else
                {
                    if (Agreement.ValidBaseCodeRate(simulatedRate))
                    { 
                        simAgreement = new AgreementSimulator(refAgreement, simulatedRate);
                        return new HttpResponseMessage()
                        {
                            Content = new ObjectContent<AgreementSimulator>(simAgreement,
                                    Configuration.Formatters.JsonFormatter)
                        };
                    }else
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Rate not found");
                }

            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Agreement not found");

        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}