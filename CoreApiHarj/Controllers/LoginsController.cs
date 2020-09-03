/*using System;
using System.Collections.Generic;
using CoreApiHarj.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreApiHarj.Tools;
using Microsoft.AspNetCore.Authorization;

namespace CoreApiHarj.Controllers
{
    [Route("nw/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        // Get all Logins
        [Authorize]
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

        //-------------------- kirjautuminen ------------------------------------------

        [HttpPost]
        [Route("singin")]
        public IActionResult Authenticate([FromBody] Logins login)
        {
            northwindContext context = new northwindContext();
            try
            {
                var pass = PasswordHash.Hasher(login.Password);
                var log = context.Logins.SingleOrDefault(x => x.Username == login.Username && x.Password == pass);

                if (log == null)
                {
                    return BadRequest("{ \"message\":\"Väärä käyttäjätunnus tai salasana\"}");
                }
                else
                {
                    string token = TokenGenerator.GenerateToken(log.Username);

                    string userJson = $"{{ \"user\": {{" +
                        $"\"userId\": \"{log.LoginId}\"," +
                        $"\"username\":\"{log.Username}\"," +
                        $"\"email\":\"{log.Email}\"}}," +
                        $"\"token\":\"{token}\"}}";

                    return Ok(userJson);
                }

            }
            catch (Exception ex)
            {
                return BadRequest("{\"message\":\"Woops, joku meni vikaan! " + ex.GetType() + " - " + ex.Message + "\"}");
            }
            finally
            {
                context.Dispose();
            }
        }

        // _________________________ Käyttäjän luominen ____________________

        [HttpPost]
        [Route("")]
        public IActionResult CreateNewLogin([FromBody] Logins login)
        {

            northwindContext context = new northwindContext();
            try
            {
                var check = context.Logins.SingleOrDefault(x => x.Username == login.Username);

                if (check == null)
                {
                    Logins newLogin = new Logins();
                    newLogin.Firstname = login.Firstname;
                    newLogin.Lastname = login.Lastname;
                    newLogin.Email = login.Email;
                    newLogin.Username = login.Username;
                    newLogin.Password = PasswordHash.Hasher(login.Password);
                    newLogin.AccesslevelId = login.AccesslevelId;

                    context.Logins.Add(newLogin);
                    context.SaveChanges();
                    return Ok("Käyttäjä luotu.");
                }
                else
                {
                    var kaytossa = "{\"message\":\"Käyttäjätunnus " + login.Username + " on jo käytössä. Valitse toinen tunnus\"}";
                    return BadRequest(kaytossa);
                }
            }
            catch (Exception ex)
            {
                var error = "{\"message\":\"Woops, joku meni vikaan! " + ex.GetType() + " - " + ex.Message + "\"}";
                return BadRequest(error);
            }
            finally
            {
                context.Dispose();
            }
        }

        /* Create new login
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
}*/