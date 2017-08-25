using APICRUD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APICRUD.Controllers
{
    public class EmpController : ApiController
    {
        [HttpGet]
       public IHttpActionResult GetApi()
        {
            Step2017Entities2 en = new Step2017Entities2();
            List<APIemp> list = en.APIemps.ToList();
            
            return Ok(list);
            
        }
        [HttpPost]
        public IHttpActionResult PostApi(APIemp em)
        {
            Step2017Entities2 en = new Step2017Entities2();
            en.APIemps.Add(em);
            en.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult DeleteApi(int id)
        {
            Step2017Entities2 en = new Step2017Entities2();
            APIemp em = new APIemp();
            var id1 = en.APIemps.Where(c => c.id == id).FirstOrDefault();
            en.APIemps.Remove(id1);
            en.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public IHttpActionResult UpdateApi(APIemp em)
        {
            Step2017Entities2 en = new Step2017Entities2();
            en.Entry(em).State = EntityState.Modified;
            en.SaveChanges();
            return Ok();
        }


    }
}
