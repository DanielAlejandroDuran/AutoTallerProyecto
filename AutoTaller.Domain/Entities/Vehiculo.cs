using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTaller.Domain.Entities;

public class Vehiculo
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public int Anio { get; set; }
    public string VIN { get; set; } = string.Empty; // Número único del vehículo
    public int Kilometraje { get; set; }

    // Relaciones
    public Cliente Cliente { get; set; } = null!;
    public ICollection<OrdenServicio> OrdenesServicio { get; set; } = new List<OrdenServicio>();
    
}