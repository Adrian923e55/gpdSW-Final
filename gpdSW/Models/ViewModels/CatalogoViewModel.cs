using gpdSW.Models.Entities;

namespace gpdSW.Models.ViewModels
{
    public class CatalogoViewModel
    {
        public IEnumerable<categCatalogo> listacategorias { get; set; }
        public IEnumerable<poductoslist> listaproducttos { get; set; }
    }
    public class categCatalogo
    {
        public int Id { get; set; }
        public string Categorias { get; set; } = null!;

    }
    public class poductoslist
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public decimal? Precio { get; set; }

        public int? IdCategoria { get; set; }
        public decimal? Preciopromocion { get; set; }
        public int Stock { get; set; }
    }
}
