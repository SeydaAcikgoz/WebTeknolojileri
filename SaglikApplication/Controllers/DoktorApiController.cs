using SaglikApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaglikApplication.Controllers
{
    [RoutePrefix("api/DoktorApi")]
    public class DoktorApiController : ApiController
    {
        SaglikkDBEntities ent = new SaglikkDBEntities();

        [HttpGet]
        [Route("Doktorlar")]
        public IEnumerable<Doktor> GetDoktorlar()
        {
            List<Doktor> model = new List<Doktor>();
            model = ent.Doktor.ToList();
            return model;
        }
    }
}
