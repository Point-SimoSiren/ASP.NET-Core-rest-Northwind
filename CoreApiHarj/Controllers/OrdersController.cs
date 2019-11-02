using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiHarj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// ----------------- Orders ---------------------

namespace CoreApiHarj.Controllers
{
    // Koko tilaustaulun sisältö
    [Route("nw/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public List<Orders> GetAllOrders()
        {
            northwindContext db = new northwindContext();
            try
            {
                List<Orders> Orders = db.Orders.ToList();
                return Orders;
            }
            finally
            {
                db.Dispose();
            }
        }

        // Haku id:llä
        [HttpGet]
        [Route("{id}")]
        public Orders GetOneorder(int id)
        {
            northwindContext db = new northwindContext();
            try
            {
                Orders order = db.Orders.Find(id);
                return order;
            }
            finally
            {
                db.Dispose();
            }
        }

        
/*        [HttpGet]
        [Route("supplierId/{key}")]
        public List<Orders> GetOrdersBySupplier(int key)
        {
            northwindContext db = new northwindContext();
            try
            {
                var prodBySupp = from p in db.Orders
                                 where p.SupplierId == key
                                 select p;

                return prodBySupp.ToList();
            }
            finally
            {
                db.Dispose();
            }
        }*/


        // Uuden tilauksen luonti
        [HttpPost]
        [Route("")]
        public ActionResult CreateNeworder([FromBody] Orders order)
        {
            northwindContext db = new northwindContext();
            try
            {

                db.Orders.Add(order);
                db.SaveChanges();
                
                return Ok(order.OrderId);
            }
            catch (Exception)
            {
                return BadRequest("Adding order failed");
            }
            finally
            {
                db.Dispose();
            }

        }

/*        // Olemassaolevan tilauksen päivittäminen
        [HttpPut]
        [Route("{id}")]
        public ActionResult Updateorder(int id, [FromBody] Orders newProd)
        {
            northwindContext db = new northwindContext();
            try
            {
                Orders oldProd = db.Orders.Find(id);
                if (oldProd != null)
                {
                    oldProd.orderName = newProd.orderName;
                    oldProd.SupplierId = newProd.SupplierId;
                    oldProd.CategoryId = newProd.CategoryId;
                    oldProd.QuantityPerUnit = newProd.QuantityPerUnit;
                    oldProd.UnitsInStock = newProd.UnitsInStock;
                    oldProd.UnitsOnOrder = newProd.UnitsOnOrder;
                    oldProd.ReorderLevel = newProd.ReorderLevel;
                    // Otin huvikseni myös nämä UI path prosessointia varten luodut rivit mukaan
                    // jos haluaa esitellä joskus toimivaa kokonaisuutta missä Rpa tekee tarkastuksia.
                    oldProd.Discontinued = newProd.Discontinued;
                    oldProd.Rpaprocessed = newProd.Rpaprocessed;

                    db.SaveChanges();
                    return Ok(newProd.orderId);
                }
                else
                {
                    return NotFound("Tilaustta ei löydy!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Tilauksen tietojen päivittäminen ei onnistunut.");
            }
            finally
            {
                db.Dispose();
            }
        }
*/
        //Yksittäisen tilauksen poistaminen
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Deleteorder(int id)
        {
            northwindContext db = new northwindContext();
            try
            {
                Orders order = db.Orders.Find(id);
                if (order != null)
                {
                    db.Orders.Remove(order);
                    db.SaveChanges();
                    return Ok("Tilaus id:llä " + id + " poistettiin");
                }
                else
                {
                    return NotFound("Tilaustta id:llä" + id + " ei löydy");
                }
            }
            catch
            {
                return BadRequest();
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}