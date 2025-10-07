using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTaller.Domain.Enums;

namespace AutoTaller.Domain.Entities;

public class OrdenServicio
{
    public int Id { get; set; }
    public int VehiculoId { get; set; }
    public int MecanicoId { get; set; }
    public TipoServicio TipoServicio { get; set; }
    public EstadoOrden Estado { get; set; } = EstadoOrden.Pendiente;
    public DateTime FechaIngreso { get; set; } = DateTime.Now;
    public DateTime FechaEstimada { get; set; }
    public DateTime? FechaFinalizacion { get; set; }
    public string Observaciones { get; set; } = string.Empty;
    public decimal ManoObra { get; set; }

    // Relaciones
    public Vehiculo Vehiculo { get; set; } = null!;
    public Usuario Mecanico { get; set; } = null!;
    public ICollection<DetalleOrden> DetallesOrden { get; set; } = new List<DetalleOrden>();
    public Factura? Factura { get; set; }       
}