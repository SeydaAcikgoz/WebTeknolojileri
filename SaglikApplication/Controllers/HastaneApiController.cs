using SaglikApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaglikApplication.Controllers
{
    [RoutePrefix("api/HastaneApi")]
    public class HastaneApiController : ApiController
    {
        SaglikkDBEntities ent = new SaglikkDBEntities();

        [HttpGet]
        [Route("Hastaneler")] 
        public IEnumerable<Hastane> GetHastaneler()
        {
            List<Hastane> model = new List<Hastane>();
            model = ent.Hastane.ToList();
            return model;
        }

        

     
    }

}