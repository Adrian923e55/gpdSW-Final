using System;
using System.Collections.Generic;

namespace gpdSW.Models.Entities;

public partial class Productos
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public int IdCategoria { get; set; }

    public int Stock { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal? Preciopromocion { get; set; }

    public virtual ICollection<Carrito> Carrito { get; set; } = new List<Carrito>();

    public virtual Catalogo IdCategoriaNavigation { get; set; } = null!;
}
