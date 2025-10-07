using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTaller.Domain.Entities;

    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relación uno a muchos
        public ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
    }