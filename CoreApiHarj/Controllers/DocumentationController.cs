using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiHarj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiHarj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentationController : ControllerBase
    {
        [HttpGet]
        [Route("{keycode}")]
        public ActionResult GetDocs(string keycode)
        {
            northwindContext db = new northwindContext();
            List<Documentation> docs = db.Documentation.ToList();

            if (docs[0].Keycode == keycode)
            {
                return Ok(docs.ToList());
                db.Dispose();
            }
            else
            {
                return Unauthorized(); // Http status code 401
                db.Dispose();
            }
        }
    }
}