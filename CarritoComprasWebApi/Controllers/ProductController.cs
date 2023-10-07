using CarritoComprasWebApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarritoComprasWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        CarritoWebContext carrito = new CarritoWebContext();
        // GET: api/<ProductController>
        [HttpGet]
        public List<Product> Get()
        {
            return carrito.Products.ToList<Product>();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product p = carrito.Products.Find(id);
            if (p == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(p);
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] Product value)
        {
            carrito.Products.Add(value);
            Product p = carrito.Products.Find(value.Id);
            if (p == null)
            {
                carrito.Products.Add(value);
            }
            else
            {
                carrito.Products.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                carrito.Products.Update(value);
            }
            carrito.SaveChanges();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product p = carrito.Products.Find(id);
            if (p == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    carrito.Products.Remove(p);
                    carrito.SaveChanges();                    
                }
                catch
                {
                    return StatusCode(500);
                }
                return Ok();

            }
        }
    }
}
