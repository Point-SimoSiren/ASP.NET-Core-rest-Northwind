using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiHarj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Tämä kontrolleri on päivitetty kurssin koodimallista, selventävien kommenttejen vuoksi.

namespace CoreApiHarj.Controllers
{
    [Route("nw/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public List<Customers> GetAllCustomers() //Hakee kaikki rivit
        {
            northwindContext db = new northwindContext();
            List<Customers> asiakkaat = db.Customers.ToList();
            return asiakkaat;
        }

        [HttpGet]
        [Route("{id}")]
        public Customers GetOneCustomer(string id) //Find-metodi hakee AINA VAIN PÄÄAVAIMELLA YHDEN RIVIN
        {
            northwindContext db = new northwindContext();
            Customers asiakas = db.Customers.Find(id);
            return asiakas;
        }

        [HttpGet]
        [Route("country/{key}")]
        public List<Customers> GetSomeCustomers(string key) //Hakee jollain tiedolla mätsäävät rivit
        {
            northwindContext db = new northwindContext();

            var someCustomers = from c in db.Customers
                                where c.Country == key
                                select c;

            return someCustomers.ToList();
        }

        [HttpPost] //<-- filtteri, joka sallii vain POST-metodit (Http-verbit)
        [Route("")] //<-- Routen placeholder
        public ActionResult PostCreateNew([FromBody] Customers asiakas) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka on Customers-tyyppinen asiakas-niminen
        {
            northwindContext db = new northwindContext(); //Context = Kuten entities muodostettu Scaffold DBContext -työkalulla. Voisi olla myös entiteetti frameworkCore
            try
            {
                db.Customers.Add(asiakas);
                db.SaveChanges();
                return Ok(asiakas.CustomerId); //kuittaus Frontille, että päivitys meni oikein --> Frontti voi tsekata, että kontrolleri palauttaa järkevän arvon ja OK:n
            }
            catch (Exception)
            {
                return BadRequest("Jokin meni pieleen asiakasta lisättäessä, ota yhteyttä kuruun");
            }
            finally
            {
                db.Dispose();
            }

        }

        [HttpPut] //<-- filtteri, joka sallii vain PUT-metodit (Http-verbit)
        [Route("{key}")] //<-- Routemääritys asiakasavaimelle key=CustomerID
        public ActionResult PutEdit(string key, [FromBody] Customers asiakas) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka on Customers-tyyppinen asiakas-niminen
        {
            northwindContext db = new northwindContext(); //Context = Kuten entities muodostettu Scaffold DBContext -työkalulla. Voisi olla myös entiteetti frameworkCore
            try
            {
                Customers customer = db.Customers.Find(key);
                if (customer != null)
                {
                    customer.CompanyName = asiakas.CompanyName;
                    customer.ContactName = asiakas.ContactName;
                    customer.ContactTitle = asiakas.ContactTitle;
                    customer.Country = asiakas.Country;
                    customer.Address = asiakas.Address;
                    customer.City = asiakas.City;
                    customer.PostalCode = asiakas.PostalCode;
                    customer.Phone = asiakas.Phone;

                    db.SaveChanges();
                    return Ok(customer.CustomerId);
                }
                else
                {
                    return NotFound("Päivitettävää asiakasta ei löytynyt!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Jokin meni pieleen asiakasta päivitettäessä, ota yhteyttä kuruun");
            }
            finally
            {
                db.Dispose();
            }
        }

        [HttpDelete]
        [Route("{key}")]
        public ActionResult DeleteCustomer(string key)
        {
            northwindContext db = new northwindContext();
            Customers asiakas = db.Customers.Find(key);
            if (asiakas != null)
            {
                db.Customers.Remove(asiakas);
                db.SaveChanges();
                return Ok("Asiakas " + key + " poistettiin");
            }
            else
            {
                return NotFound("Asiakasta " + key + " ei löydy");
            }

        }
    }
}
