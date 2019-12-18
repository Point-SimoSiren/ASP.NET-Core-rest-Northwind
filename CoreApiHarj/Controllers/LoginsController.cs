using System;
using System.Collections.Generic;
using CoreApiHarj.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiHarj.Controllers
{
    [Route("nw/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        // Get all Logins
        [HttpGet]
        [Route("")]
        public List<Logins> GetAllLogins()
        {
            northwindContext db = new northwindContext();
            try
            {
                List<Logins> users = db.Logins.ToList();
                return users;
            }
            finally
            {
                db.Dispose();
            }
        }


        // Get Logins by Username
        [HttpGet]
        [Route("Username/{key}")]
        public List<Logins> GetSomeLogins(string key)
        {
            northwindContext db = new northwindContext();

            var someLogins = from l in db.Logins
                             where l.Username == key
                             select l;

            return someLogins.ToList();
        }

        // Create new login
        [HttpPost]
        [Route("")]
        public ActionResult PostCreateNew([FromBody] Logins user)
        {
            northwindContext db = new northwindContext();
            try
            {
                db.Logins.Add(user);
                db.SaveChanges();
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest("Käyttäjän lisääminen ei onnistunut." + e);
            }
            finally
            {
                db.Dispose();
            }
        }

        [HttpPut]
        [Route("{key}")]
        public ActionResult PutEdit(int key, [FromBody] Logins user)
        {
            northwindContext db = new northwindContext();
            try
            {
                Logins login = db.Logins.Find(key);
                if (login != null)
                {
                    login.Firstname = user.Firstname;
                    login.Lastname = user.Lastname;
                    login.Email = user.Email;
                    login.Username = user.Username;
                    login.Password = user.Password;
                    login.AccesslevelId = user.AccesslevelId;

                    db.SaveChanges();
                    return Ok(login.Firstname);
                }
                else
                {
                    return NotFound("Päivitettävää useria ei löytynyt!");
                }
            }
            catch
            {
                return BadRequest("Jokin meni pieleen useria päivitettäessä, ota yhteyttä jonnekin");
            }
            finally
            {
                db.Dispose();
            }
        }

        [HttpDelete]
        [Route("{key}")]
        public ActionResult Deletelogin(int key)
        {
            northwindContext db = new northwindContext();
            try
            {
                Logins user = db.Logins.Find(key);
                if (user != null)
                {
                    db.Logins.Remove(user);
                    db.SaveChanges();
                    return Ok("user " + key + " poistettiin");
                }
                else
                {
                    return NotFound("useria " + key + " ei löydy");
                }
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}