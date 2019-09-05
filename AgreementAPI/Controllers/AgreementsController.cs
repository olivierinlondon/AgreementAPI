using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
        public string Get(int id)
        {
            VilLibRate.CallWebService();

            using (var context = new AgreementContext())
            {

                foreach (Agreement ag in context.Agreements)
                {
                    string re = ag.BaseCodeRate;
                }

            }

                return "agreement";
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