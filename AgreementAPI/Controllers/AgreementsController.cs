using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

using System.Data.Entity;

using AgreementAPI.Models;
using AgreementAPI.DBContext;

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
            foreach (var parameter in Request.GetQueryNameValuePairs())
            {
                var key = parameter.Key;
                var value = parameter.Value;
            }


            VilLibRate.CallWebService();

            Agreement ag1 = null;

            using (var context = new AgreementContext())
            {
                var a = context.Agreements.Include(agr => agr.CustomerDetails);

                foreach (Agreement ag in a)
                {
                    ag1 = ag;
                    string re = ag.CustomerDetails.FirstName;
                }

            }

            Customer c = ag1.CustomerDetails;
            
            return new HttpResponseMessage()
            {
                Content = new ObjectContent<Agreement>(ag1,
                          Configuration.Formatters.JsonFormatter)
            };
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