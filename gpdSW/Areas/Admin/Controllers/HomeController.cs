using gpdSW.Areas.Admin.Models.ViewModels;
using gpdSW.Models.Entities;
using gpdSW.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NuGet.Protocol.Core.Types;

namespace gpdSW.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    [Route("/Admin/[controller]/[action]/{id?}")]
    public class HomeController : Controller
    {
        CheeseandmoreContext context;
        Repository<Productos> productosRepository;
        Repository<Catalogo> catalogoRepository;
        public HomeController()
        {
            context = new CheeseandmoreContext();
            productosRepository = new(context);
            catalogoRepository = new(context);
        }
        [Route("/Admin")]
        public IActionResult Index()
        {
            IndexViewModel model = new()
            {
                ListaProductos = productosRepository.GetAll().OrderBy(x => x.IdCategoria),
                ListaParaStock = productosRepository.GetAll().Where(x => x.Stock <= 5).OrderBy(x=>x.Stock)
            };

            return View(model);
        }
        [Route("/Admin/AgregarProducto")]
        [HttpGet]
        public IActionResult AgregarProducto()
        {

            AgregarProductoViewModel vm = new AgregarProductoViewModel();
            vm.catalogo = catalogoRepository.GetAll();

            return View(vm);
        }
        [HttpPost]
        public IActionResult AgregarProducto(AgregarProductoViewModel vm)
        {
            ModelState.Clear();

            if (string.IsNullOrWhiteSpace(vm.productitos.Nombre))
            {
                ModelState.AddModelError("", "EL *Nombre* ES INCORRECTO");
            }
            if (vm.productitos.Precio <= 0)
            {
                ModelState.AddModelError("", "EL *precio* NO PUEDE SER <=0");
            }
            if (string.IsNullOrWhiteSpace(vm.productitos.Descripcion))
            {
                ModelState.AddModelError("", "LA *Descripcion* ES INCORRECTA");
            }
            if (vm.productitos.Stock <= 0)
            {
                ModelState.AddModelError("", "EL *Stock* ES INCORRECTO");
            }
            if (ModelState.IsValid)
            {
                productosRepository.Insert(vm.productitos);

                var ruta = $"wwwroot/imagenes/productos/{vm.productitos.Id}.png";
                if (vm.Imagen != null)
                {
                    FileStream fs = System.IO.File.Create(ruta);
                    vm.Imagen.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(vm);
            }

        }
        [HttpGet]
        public IActionResult EliminarProducto(int? id)
        {
            var datos = productosRepository.Get(id);
            if (datos == null)
            {
                return RedirectToAction("Index");
            }
            return View(datos);

        }
        [HttpPost]
        public IActionResult EliminarProducto(Productos vm)
        {
            var datos = productosRepository.Get(vm.Id);

            if (datos == null)
            {
                return RedirectToAction("Index");
            }
            productosRepository.Delete(datos);
            var ruta = $"wwwroot/imagenes/productos/{vm.Id}.png";
            if (System.IO.File.Exists(ruta))
            {
                System.IO.File.Delete(ruta);
            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult EditarProducto(int id)
        {
            var datos = productosRepository.Get(id);

            var cate = catalogoRepository.GetAll();
            if (datos == null)
            {
                return RedirectToAction("Index");
            }

            var vm = new AgregarProductoViewModel
            {
                productitos = datos,
                catalogo = cate
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult EditarProducto(AgregarProductoViewModel vm)
        {
            ModelState.Clear();
            if (string.IsNullOrWhiteSpace(vm.productitos.Nombre))
            {
                ModelState.AddModelError("", "EL *Nombre* ES INCORRECTO");
            }
            if (vm.productitos.Precio <= 0)
            {
                ModelState.AddModelError("", "EL *precio* NO PUEDE SER <=0");
            }
            if (string.IsNullOrWhiteSpace(vm.productitos.Descripcion))
            {
                ModelState.AddModelError("", "LA *Descripcion* ES INCORRECTA");
            }
            if (ModelState.IsValid)
            {
                var datos = productosRepository.Get(vm.productitos.Id);
                if (datos == null)
                {
                    return RedirectToAction("Index");
                }
                datos.Nombre = vm.productitos.Nombre;
                datos.Descripcion = vm.productitos.Descripcion;
                datos.Precio = vm.productitos.Precio;
                datos.Descripcion = vm.productitos.Descripcion;

                var ruta = $"wwwroot/imagenes/productos/{vm.productitos.Id}.png";
                if (vm.Imagen != null)
                {
                    FileStream fs = System.IO.File.Create(ruta);
                    vm.Imagen.CopyTo(fs);
                    fs.Close();
                }
                productosRepository.Update(datos);
                return RedirectToAction("Index");
            }
            return View(vm);
        }
        [HttpGet]
        public IActionResult Stockypromociones(int? id)
        {
            if (id != null)
            {
                var dato = productosRepository.Get(id);
                return View(dato);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Stockypromociones(Productos vm)
        {
            var datos = productosRepository.Get(vm.Id);
            ModelState.Clear();
            if (vm.Stock <= 0)
            {
                ModelState.AddModelError("", "EL *stock* NO PUEDE SER 0");
               
            }
            //if (vm.Stock < datos.Stock)
            //{
            //    ModelState.AddModelError("", "AUMENTE EL *stock*, A MAS DE "+ datos.Stock +"");

            //}
            if (vm.Preciopromocion >= datos.Precio)
            {
                ModelState.AddModelError("", "EL *precio* es de $" + datos.Precio + ",DEBE SER MENOR");
            }
            if (ModelState.IsValid)
            {
                if (datos == null)
                {
                    return RedirectToAction("Index");
                }


                datos.Stock = vm.Stock;
                datos.Preciopromocion = vm.Preciopromocion;

                productosRepository.Update(datos);
                return RedirectToAction("Index");

            }
            return View(vm);
        }

        public IActionResult Feedbacks()
        {
            ReseñasViewModel vm = new();

            vm.listareseñita = context.Recomendaciones.Select(x => new reseñita
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                Estrellas = x.Estrellas,
                NombreUsuario = x.IdUsuariosNavigation.NombreUsuario,
                Correo=x.IdUsuariosNavigation.Correo
            });
            return View(vm);
        }
    }
}
