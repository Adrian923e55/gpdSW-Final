namespace gpdSW.Models.ViewModels
{
    public class VerEventosViewModel
    {
        public int Id { get; set; }

        public string Descripcion { get; set; } = null!;

        public string Nombre { get; set; } = null!;
        public int? IdEvento { get; set; }

    }
}
