using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTaller.Domain.Entities;

public class Repuesto
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty; // Único
    public string Descripcion { get; set; } = string.Empty;
    public int Stock { get; set; }
    public decimal PrecioUnitario { get; set; }
    public int StockMinimo { get; set; } = 5;
    
    // Relación muchos a muchos con OrdenServicio a través de DetalleOrden
    public ICollection<DetalleOrden> DetallesOrden { get; set; } = new List<DetalleOrden>();
}