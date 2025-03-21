using System;
using System.Collections.Generic;

namespace gpdSW.Models.Entities;

public partial class Recomendaciones
{
    public int Id { get; set; }

    public int Estrellas { get; set; }

    public string Descripcion { get; set; } = null!;

    public int IdUsuarios { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Usuarios IdUsuariosNavigation { get; set; } = null!;
}
