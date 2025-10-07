using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTaller.Domain.Entities;

    public class Auditoria
    {
        public int Id { get; set; }
        public string Entidad { get; set; } = string.Empty; // Nombre de la tabla
        public string Accion { get; set; } = string.Empty; // INSERT, UPDATE, DELETE
        public int? UsuarioId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string? DatosAnteriores { get; set; } // JSON
        public string? DatosNuevos { get; set; } // JSON       
}