namespace gpdSW.Areas.Admin.Models.ViewModels
{
    public class ReseñasViewModel
    {
        public IEnumerable<reseñita> listareseñita { get; set; }
    }
    public class reseñita
    {
        public int Id { get; set; }

        public int Estrellas { get; set; }

        public string Descripcion { get; set; } = null!;

        public string NombreUsuario { get; set; } = null!;
        public string Correo { get; set; } = null!;

    }
}
