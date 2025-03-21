using gpdSW.Models.Entities;

namespace gpdSW.Areas.Admin.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Productos> ListaProductos { get; set; }

        public Productos productitos { get; set; }

        public IEnumerable<Productos> ListaParaStock { get; set; }
    }
}
