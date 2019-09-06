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

        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
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
                    var valuepair = Request.GetQueryNameValuePairs().FirstOrDefault(a => a.Key == "SimulatedRate");

                    if (Agreement.ValidBaseCodeRate(valuepair.Value))
                    { 
                        simAgreement = new AgreementSimulator(refAgreement,valuepair.Value);
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