using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CoreApiHarj.Models;
using Microsoft.AspNetCore.Mvc;

// ------------- Order details ---------------------------

namespace CoreApiHarj.Controllers
{
    [Route("nw/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        // Kaikki rivit OrderDetails taulusta
        [HttpGet]
        [Route("")]
        public List<OrderDetails> GetAllOrderDetails()
        {
            northwindContext db = new northwindContext();
            try
            {
                List<OrderDetails> orderDetails = db.OrderDetails.ToList();
                return orderDetails;
            }
            finally
            {
                db.Dispose();
            }
        }
        

        // Tilaus detaljien haku tilaus id:llä.
        [HttpGet]
        [Route("{id}")]
        public List<OrderDetails> GetDetailsByOrderId(int id)
        {
            northwindContext db = new northwindContext();
            try
            {
                var orderDetailsByOrderId = from oDet in db.OrderDetails
                                            where oDet.OrderId == id
                                              select oDet;

                return orderDetailsByOrderId.ToList();
            }
            catch
            {
                return null; 
            }
            finally
            {
                db.Dispose();
            }
        }

        // Tilaus detaljit tuote id:n mukaan
        [HttpGet]
        [Route("ProductId/{key}")]
        public List<OrderDetails> GetOrderDetailsByCustomer(int key)
        {
            northwindContext db = new northwindContext();
            try
            {
                var orderDetailsByProductId = from o in db.OrderDetails
                                  where o.ProductId == key
                                  select o;

                return orderDetailsByProductId.ToList();
            }
            finally
            {
                db.Dispose();
            }
        }


        // Uuden tilausdetalji -rivin luonti
        [HttpPost]
        [Route("")]
        public ActionResult CreatenewOrderDetDetail([FromBody] OrderDetails orderDetail)
        {
            northwindContext db = new northwindContext();
            try
            {

                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();

                return Ok(orderDetail.OrderId);
            }
            catch
            {
                return BadRequest("Adding order failed");
            }
            finally
            {
                db.Dispose();
            }

        }

        // Olemassaolevan tilausdetalji -rivin päivittäminen
        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateOrderDetail(int id, [FromBody] OrderDetails newOrderDet)
        {
            northwindContext db = new northwindContext();
            try
            {
                OrderDetails oldOrderDet = db.OrderDetails.Find(id);
                if (oldOrderDet != null)
                {
                    oldOrderDet.OrderId = newOrderDet.OrderId;
                    oldOrderDet.ProductId = newOrderDet.ProductId;
                    oldOrderDet.UnitPrice = newOrderDet.UnitPrice;
                    oldOrderDet.Quantity = newOrderDet.Quantity;
                    oldOrderDet.Discount = newOrderDet.Discount;
                    oldOrderDet.Order = newOrderDet.Order;
                    oldOrderDet.Product = newOrderDet.Product;
                    
                    db.SaveChanges();
                    return Ok(newOrderDet.OrderId);
                }
                else
                {
                    return NotFound("Tilausta ei löydy!");
                }
            }
            catch
            {
                return BadRequest("Tilauksen tietojen päivittäminen ei onnistunut.");
            }
            finally
            {
                db.Dispose();
            }
        }

        //Yksittäisen tilausdetalji -rivin poistaminen
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteOrderDetail(int id)
        {
            northwindContext db = new northwindContext();
            try
            {
                OrderDetails orderDet = db.OrderDetails.Find(id);
                if (orderDet != null)
                {
                    db.OrderDetails.Remove(orderDet);
                    db.SaveChanges();
                    return Ok("Tilaus detaljit tilaus id:llä " + id + " poistettiin");
                }
                else
                {
                    return NotFound("Tilausdetaljita tilaus id:llä" + id + " ei löydy");
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

