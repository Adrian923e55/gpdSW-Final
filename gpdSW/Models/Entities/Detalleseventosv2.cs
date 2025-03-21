using System;
using System.Collections.Generic;

namespace gpdSW.Models.Entities;

public partial class Detalleseventosv2
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int? IdEvento { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual Eventos? IdEventoNavigation { get; set; }
}
