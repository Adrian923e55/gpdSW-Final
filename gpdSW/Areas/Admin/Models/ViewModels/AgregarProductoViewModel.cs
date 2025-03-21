using gpdSW.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace gpdSW.Areas.Admin.Models.ViewModels
{
    public class AgregarProductoViewModel
    {

        public Productos productitos { get; set; }

        public IEnumerable<Catalogo> catalogo { get; set; }

        public IFormFile Imagen { get; set; }


        //editar
        public Catalogo editarcatalogo { get; set; }
    }
}
