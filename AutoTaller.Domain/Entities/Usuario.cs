using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTaller.Domain.Enums;

namespace AutoTaller.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public RolUsuario Rol { get; set; }
    public bool Activo { get; set; } = true;
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    
    // Relación con órdenes (como mecánico)
    public ICollection<OrdenServicio> OrdenesAsignadas { get; set; } = new List<OrdenServicio>();
}