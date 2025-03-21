namespace gpdSW.Models.ViewModels
{
    public class ReseñasViewModel
    {
        public int Estrellas { get; set; }
        public string Descripcion { get; set; } = null!;

        public IEnumerable<resenenonas> listitaresenas { get; set; }

    }
    public class resenenonas
    {
        public int Id { get; set; }

        public int Estrellas { get; set; }

        public string Descripcion { get; set; } = null!;

        public string NombreUsuario { get; set; } = null!;
        public DateTime? Fecha { get; set; }


    }
}
