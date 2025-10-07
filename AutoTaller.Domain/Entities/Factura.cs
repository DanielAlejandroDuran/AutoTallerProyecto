using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTaller.Domain.Entities;

public class Factura
{
    public int Id { get; set; }
    public int OrdenServicioId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    public decimal SubtotalRepuestos { get; set; }
    public decimal ManoObra { get; set; }
    public decimal Total { get; set; }
    public string NumeroFactura { get; set; } = string.Empty;

    // Relación uno a uno
    public OrdenServicio OrdenServicio { get; set; } = null!;       
}