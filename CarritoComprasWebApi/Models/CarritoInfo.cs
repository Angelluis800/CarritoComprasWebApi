using System;
using System.Collections.Generic;

namespace CarritoComprasWebApi.Models;

public partial class CarritoInfo
{
    public string Fecha { get; set; } = null!;

    public int Id { get; set; }

    public int NumeroCarrito { get; set; }

    public int Cantidad { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Monto { get; set; }

    public decimal MontoTotal { get; set; }
}
