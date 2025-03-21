using System;
using System.Collections.Generic;

namespace gpdSW.Models.Entities;

public partial class Eventos
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Detalleseventosv2> Detalleseventosv2 { get; set; } = new List<Detalleseventosv2>();
}
