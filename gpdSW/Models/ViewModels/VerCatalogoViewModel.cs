namespace gpdSW.Models.ViewModels
{
    public class VerCatalogoViewModel
    {
        public IEnumerable<infoProductos> listaProductos { get; set; }
    }
    public class infoProductos
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public decimal? Precio { get; set; }
        public int Stock { get; set; }

        public decimal? Preciopromocion { get; set; }
        public string IdCategoria { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public int catid { get; set; }


    }
}
