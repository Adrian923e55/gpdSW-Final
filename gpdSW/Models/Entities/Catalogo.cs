using System;
using System.Collections.Generic;

namespace gpdSW.Models.Entities;

public partial class Catalogo
{
    public int Id { get; set; }

    public string Categorias { get; set; } = null!;

    public virtual ICollection<Productos> Productos { get; set; } = new List<Productos>();
}
