﻿using System;
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


        [HttpGet]
        [Route("R")]
        public IActionResult GetSomeCustomers(int offset, int limit, string country)
        {
            northwindContext db = new northwindContext();
            try
            {
                if (country != null) // Jos country parametri todellakin annetaan
                {
                    List<Customers> asiakkaat = db.Customers.Where(d => d.Country == country).Take(limit).ToList();
                    return Ok(asiakkaat);
                }
                else // Ilman country tietoa
                {
                    List<Customers> asiakkaat = db.Customers.Skip(offset).Take(limit).ToList();
                    return Ok(asiakkaat);
                }
            }
            catch
            {
                return BadRequest("Something went wrong. Try get all and see if the country exists in the listing");
            }
            finally
            {
                db.Dispose();
            }
         }
           
        
       
        // Get all customers
        [HttpGet]
        [Route("")]
        public List<Customers> GetAllCustomers()
        {
            northwindContext db = new northwindContext();
            try
            {
                List<Customers> asiakkaat = db.Customers.ToList();
                Console.WriteLine(asiakkaat[1].ToString());
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

        // Get Customers by country parameter localhost:5001/api/customers/country/finland
        [HttpGet]
        [Route("country/{maa}")]
        public List<Customers> GetSomeCustomers(string maa) 
        {
            northwindContext db = new northwindContext();

            var someCustomers = from c in db.Customers
                                where c.Country == maa
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
            catch (Exception e)
            {
                return BadRequest("Asiakkaan lisääminen ei onnistunut. Alla lisätietoa" + e);
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
                    customer.Fax = asiakas.Fax;

                    db.SaveChanges();
                    return Ok(customer.CustomerId);
                }
                else
                {
                    return NotFound("Päivitettävää asiakasta ei löytynyt!");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Jokin meni pieleen asiakasta päivitettäessä. Alla lisätietoa" + e);
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
                    try
                    {
                        db.Customers.Remove(asiakas);
                        db.SaveChanges();
                        Console.WriteLine(key + " poistettiin");
                        return Ok("Asiakas " + key + " poistettiin");
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e + "Asiakkaalla on tilauksia? Poistaminen estetty?");
                    }
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
