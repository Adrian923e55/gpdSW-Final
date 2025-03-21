namespace gpdSW.Models.ViewModels
{
    public class EventosViewModel
    {
        public IEnumerable<eventitos> listaeventos {  get; set; }   
    }
    public class eventitos
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
