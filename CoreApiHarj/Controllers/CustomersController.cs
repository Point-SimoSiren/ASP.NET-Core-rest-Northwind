using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiHarj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// ------------------ Customers --------------------

namespace CoreApiHarj.Controllers
{
    [Route("nw/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        // Get all customers
        [HttpGet]
        [Route("")]
        public List<Customers> GetAllCustomers()
        {
            northwindContext db = new northwindContext();
            try
            {
                List<Customers> asiakkaat = db.Customers.ToList();
                return asiakkaat;
            }
            finally
            {
                db.Dispose();
            }
        }

        // Get 1 customer by id
        [HttpGet]
        [Route("{id}")]
        public Customers GetOneCustomer(string id)
        {
            northwindContext db = new northwindContext();
            Customers asiakas = db.Customers.Find(id);
            return asiakas;
        }

        // Get Customers by country parameter
        [HttpGet]
        [Route("country/{key}")]
        public List<Customers> GetSomeCustomers(string key) 
        {
            northwindContext db = new northwindContext();

            var someCustomers = from c in db.Customers
                                where c.Country == key
                                select c;

            return someCustomers.ToList();
        }

        // Create new Customer
        [HttpPost] 
        [Route("")] 
        public ActionResult PostCreateNew([FromBody] Customers asiakas) 
        {
            northwindContext db = new northwindContext();
            try
            {
                db.Customers.Add(asiakas);
                db.SaveChanges();
                return Ok(asiakas.CustomerId);
            }
            catch
            {
                return BadRequest("Asiakkaan lisääminen ei onnistunut.");
            }
            finally
            {
                db.Dispose();
            }
        }

        [HttpPut]
        [Route("{key}")]
        public ActionResult PutEdit(string key, [FromBody] Customers asiakas)
        {
            northwindContext db = new northwindContext();
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
            catch
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
            try
            {
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
            finally
            {
                db.Dispose();
            }
        }
    }
}
