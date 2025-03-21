using gpdSW.Models.Entities;
using gpdSW.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gpdSW.Areas.User.Controllers
{
    [Authorize]
    [Area("User")]
    [Route("/User/[controller]/[action]/{id?}")]
    public class HomeController : Controller
    {
        CheeseandmoreContext context;
        Repository<Usuarios> usuarioRepository;
        public HomeController()
        {
            context = new CheeseandmoreContext();
            usuarioRepository = new(context);
        }
        [Route("/User")]
        public IActionResult Index()
        {
            var idusuario = User.FindFirst("Id")?.Value;

            var datos = usuarioRepository.GetAll().Where(x => x.Id == Convert.ToInt32(idusuario)).FirstOrDefault();



            return View(datos);
        }
    }
}

