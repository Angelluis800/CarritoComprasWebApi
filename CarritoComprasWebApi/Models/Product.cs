using System;
using System.Collections.Generic;

namespace CarritoComprasWebApi.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public int Existencia { get; set; }
}
