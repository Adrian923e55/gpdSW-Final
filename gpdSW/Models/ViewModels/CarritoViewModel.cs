namespace gpdSW.Models.ViewModels
{
    public class CarritoViewModel
    {
        public int? IdUsuario { get; set; }

        public int? IdProducto { get; set; }

        public int Cantidad { get; set; }
  

        ///////-- Prod
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }

        public string Categorias { get; set; } = null!;
        public decimal? Preciopromocion { get; set; }


    }
    public class VerCarrito
    {
        public decimal? PrecioTotal { get; set; }
        public IEnumerable<CarritoViewModel> listitacarrito { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
    }
}
