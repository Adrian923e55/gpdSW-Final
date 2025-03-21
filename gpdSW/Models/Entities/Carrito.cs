using System;
using System.Collections.Generic;

namespace gpdSW.Models.Entities;

public partial class Carrito
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdProducto { get; set; }

    public int Cantidad { get; set; }

    public virtual Productos? IdProductoNavigation { get; set; }

    public virtual Usuarios? IdUsuarioNavigation { get; set; }
}
