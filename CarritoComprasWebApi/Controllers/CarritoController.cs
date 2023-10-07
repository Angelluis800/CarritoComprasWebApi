using CarritoComprasWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarritoComprasWebApi.Controllers
{
    [Route("api/Carrito")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly CarritoWebContext _context;

        public CarritoController(CarritoWebContext context)
        {
            _context = context;
        }

        // GET: api/carrito
        [HttpGet]
        public List<CarritoInfo> Get()
        {
            return _context.CarritoInfos.ToList();
        }

        // POST api/carrito
        [HttpPost]
        public async Task<IActionResult> AgregarAlCarrito(int productId, int cantidad)
        {
            // Obtener el producto por ID desde la base de datos
            var producto = await _context.Products.FindAsync(productId);

            if (producto == null)
            {
                return NotFound(); // Devolver 404 si el producto no se encuentra
            }

            // Calcular el monto unitario multiplicando el precio por la cantidad
            decimal montoUnitario = producto.Precio * cantidad;

            // Crear un nuevo registro de CarritoInfo con los datos requeridos
            var carritoInfo = new CarritoInfo
            {
                Fecha = DateTime.Now.ToString("yyyy-MM-dd"),
                Cantidad = cantidad,
                Nombre = producto.Nombre,
                Monto = (int)montoUnitario
            };

            // Agregar el nuevo registro al contexto y guardar en la base de datos
            _context.CarritoInfos.Add(carritoInfo);
            await _context.SaveChangesAsync();

            // Recalcular el monto total y actualizar todos los registros en el carrito
            var registrosCarrito = await _context.CarritoInfos.ToListAsync();
            decimal montoTotal = registrosCarrito.Sum(cd => cd.Monto);
            foreach (var registro in registrosCarrito)
            {
                registro.MontoTotal = montoTotal;
            }
            await _context.SaveChangesAsync();

            return Ok(carritoInfo);
        }

        // PUT api/carrito/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCarrito(int id, int cantidad)
        {
            // Obtener el registro de CarritoInfo por ID desde la base de datos
            var carritoInfo = await _context.CarritoInfos.FindAsync(id);

            if (carritoInfo == null)
            {
                return NotFound(); // Devolver 404 si el registro no se encuentra
            }

            // Obtener el producto relacionado desde la base de datos
            var producto = await _context.Products.FirstOrDefaultAsync(p => p.Nombre == carritoInfo.Nombre);

            if (producto == null)
            {
                return NotFound(); // Devolver 404 si el producto no se encuentra
            }

            // Actualizar la cantidad y el monto unitario en el registro
            carritoInfo.Cantidad = cantidad;
            carritoInfo.Monto = (int)producto.Precio * cantidad;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Recalcular el monto total y actualizar todos los registros en el carrito
            var registrosCarrito = await _context.CarritoInfos.ToListAsync();
            decimal montoTotal = registrosCarrito.Sum(cd => cd.Monto);
            foreach (var registro in registrosCarrito)
            {
                registro.MontoTotal = montoTotal;
            }
            await _context.SaveChangesAsync();

            return Ok(carritoInfo); // Devolver el registro actualizado de CarritoInfo
        }
    }
}
