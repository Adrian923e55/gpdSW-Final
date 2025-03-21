namespace gpdSW.Models.ViewModels.AccountVM
{
    public class RestContViewModel
    {
        public string? Correo { get; set; }
        public string[] OTP { get; set; } = new string[5];
    }
}
