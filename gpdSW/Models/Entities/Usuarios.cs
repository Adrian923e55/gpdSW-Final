using System;
using System.Collections.Generic;

namespace gpdSW.Models.Entities;

public partial class Usuarios
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public int Rol { get; set; }

    public virtual ICollection<Carrito> Carrito { get; set; } = new List<Carrito>();

    public virtual ICollection<Recomendaciones> Recomendaciones { get; set; } = new List<Recomendaciones>();
}
