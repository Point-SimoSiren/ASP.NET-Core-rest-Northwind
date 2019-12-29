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
    public class TuoteController : ControllerBase
    {

        // Hae kaikki

        [HttpGet]
        [Route("")]
        public List<Tuotteet> HaeKaikkiTuotteet()
        {
            northwindContext db = new northwindContext();
            try
            {
                List<Tuotteet> tuotteet = db.Tuotteet.ToList();
                return tuotteet;
            }
            finally
            {
                db.Dispose();
            }
        }

        // Haku id:llä
        [HttpGet]
        [Route("{id}")]
        public Tuotteet HaeTuote(int id)
        {
            northwindContext db = new northwindContext();
            try
            {
                Tuotteet tuote = db.Tuotteet.Find(id);
                return tuote;
            }
            finally
            {
                db.Dispose();
            }
        }

        // Uuden luonti
        [HttpPost]
        [Route("")]
        public ActionResult LuoTuote([FromBody] Tuotteet tuote)
        {
            northwindContext db = new northwindContext();
            try
            {

                db.Tuotteet.Add(tuote);
                db.SaveChanges();
                return Ok(tuote.tuoteId);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            finally
            {
                db.Dispose();
            }

        }
    }
}
