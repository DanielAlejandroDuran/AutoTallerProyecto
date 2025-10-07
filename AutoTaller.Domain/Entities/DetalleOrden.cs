using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTaller.Domain.Entities;

public class DetalleOrden
{
    public int Id { get; set; }
    public int OrdenServicioId { get; set; }
    public int RepuestoId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; } // Guardamos el precio al momento de la orden

    // Relaciones
    public OrdenServicio OrdenServicio { get; set; } = null!;
    public Repuesto Repuesto { get; set; } = null!;

    // Propiedad calculada
    public decimal Subtotal => Cantidad * PrecioUnitario;        
}