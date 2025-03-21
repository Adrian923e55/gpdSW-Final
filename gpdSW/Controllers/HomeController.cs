using gpdSW.Models.Entities;
using gpdSW.Models.ViewModels;
using gpdSW.repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace gpdSW.Controllers
{
    public class HomeController : Controller
    {
        CheeseandmoreContext context;
        Repository<Productos> productosRepository;
        Repository<Recomendaciones> recomendacionesRepository;
        Repository<Carrito> carritoRepository;


        public HomeController()
        {
            context = new CheeseandmoreContext();
            productosRepository = new(context);
            recomendacionesRepository = new(context);
            carritoRepository = new(context);
        }


        public IActionResult Index(string xd)
        {
            CheeseandmoreContext context = new CheeseandmoreContext();
            ReseñasViewModel vm = new();

            vm.listitaresenas = context.Recomendaciones.Select(x => new resenenonas
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                Estrellas = x.Estrellas,
                NombreUsuario = x.IdUsuariosNavigation.NombreUsuario

            });

            return View(vm);
        }


        //////////////////////////////////////////////  CATEGORIAS
        public IActionResult Catalogo(int? id, string? terminoBusqueda)
        {
            Random rnd = new Random();
            CheeseandmoreContext context = new CheeseandmoreContext();


            if (terminoBusqueda != null)
            {
                CatalogoViewModel vm = new CatalogoViewModel()
                {
                    listaproducttos = context.Productos.Where(x=>x.Nombre.Contains(terminoBusqueda)).Select(x => new poductoslist
                    {
                        Id = x.Id,
                        IdCategoria = x.IdCategoria,
                        Nombre = x.Nombre,
                        Precio = x.Precio,
                        Stock = x.Stock,
                        Preciopromocion = x.Preciopromocion
                    }).AsEnumerable() // Traer los datos a memoria
                .OrderBy(x => rnd.Next()), // Orden aleatorio en memoria


                    listacategorias = context.Catalogo.Select(x => new categCatalogo
                    {
                        Id = x.Id,
                        Categorias = x.Categorias,
                    })
                };
                return View(vm);
            }
            if (id == null)
            {
                CatalogoViewModel vm = new CatalogoViewModel()
                {
                    listaproducttos = context.Productos.Select(x => new poductoslist
                    {
                        Id = x.Id,
                        IdCategoria = x.IdCategoria,
                        Nombre = x.Nombre,
                        Precio = x.Precio,
                        Stock = x.Stock,
                        Preciopromocion = x.Preciopromocion
                    }).AsEnumerable() // Traer los datos a memoria
                .OrderBy(x => rnd.Next()), // Orden aleatorio en memoria


                    listacategorias = context.Catalogo.Select(x => new categCatalogo
                    {
                        Id = x.Id,
                        Categorias = x.Categorias,
                    })
                };
                return View(vm);
            }
            else
            {
                CatalogoViewModel vm = new CatalogoViewModel()
                {
                    listaproducttos = context.Productos.Where(x => x.IdCategoria == id).Select(x => new poductoslist
                    {
                        Id = x.Id,
                        IdCategoria = x.IdCategoria,
                        Nombre = x.Nombre,
                        Precio = x.Precio,
                        Stock = x.Stock,
                        Preciopromocion = x.Preciopromocion
                    }).AsEnumerable()
                .OrderBy(x => rnd.Next()),


                    listacategorias = context.Catalogo.Select(x => new categCatalogo
                    {
                        Id = x.Id,
                        Categorias = x.Categorias,
                    })
                };
                return View(vm);
            }

        }



        public IActionResult Productos(int id)
        {

            CheeseandmoreContext context = new CheeseandmoreContext();

            var datos = context.Productos.Where(x => x.Id == id).Select(x => x.Id);
            if (datos == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var vm = context.Productos.Where(x => x.Id == id).Select(x => new infoProductos
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    IdCategoria = x.IdCategoriaNavigation.Categorias,
                    catid = x.IdCategoria,
                    Precio = x.Precio,
                    Stock = x.Stock,
                    Descripcion = x.Descripcion,
                    Preciopromocion = x.Preciopromocion

                }).FirstOrDefault();

                if (vm == null)
                {
                    return NotFound();
                }
                return View(vm);
            }



        }

        public IActionResult VerProductoView(int? id)
        {
            CheeseandmoreContext context = new CheeseandmoreContext();
            var vm = context.Productos.Where(x => x.Id == id).Select(x => new infoProductos
            {
                Id = x.Id,
                Precio = x.Precio,
                Stock = x.Stock,
                Descripcion = x.Descripcion,
                Preciopromocion = x.Preciopromocion

            }).FirstOrDefault();


            return View(vm);
        }


        /////////////////////////////////////////////////////  EVENTOS
        public IActionResult Eventos()
        {
            CheeseandmoreContext context = new CheeseandmoreContext();
            EventosViewModel vm = new EventosViewModel();

            vm.listaeventos = context.Eventos.Select(x => new eventitos
            {
                Id = x.Id,
                Nombre = x.Nombre
            });
            return View(vm);
        }

        public IActionResult Detalleseventosv2(int id)
        {
            CheeseandmoreContext context = new CheeseandmoreContext();


            var vm = context.Detalleseventosv2.Where(x => x.IdEvento == id).Select(x => new VerEventosViewModel
            {
                Id = x.Id,
                Descripcion = x.Descripcion,

                Nombre = x.Nombre,

                IdEvento = x.IdEvento
            })
                  .FirstOrDefault();

            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }




        /////////////////////////////////////////////////////  EVENTOS

        [HttpGet]
        public IActionResult Contacto()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Contacto(ContactoViewModel vm)
        {

            return View();
        }

        /////////////////////////////////////////////////////  Reseñas
        [HttpGet]
        public IActionResult Reseñas()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Reseñas(ReseñasViewModel vm)
        {
            var idusuario = User.FindFirst("Id")?.Value;
            var resenaexi = recomendacionesRepository.GetAll().Where(x => x.IdUsuarios == Convert.ToInt32(idusuario)).Count();
            ModelState.Clear();
            if (string.IsNullOrWhiteSpace(vm.Descripcion))
            {
                ModelState.AddModelError("", "LA *Descripcion* ESTA VACIA");
            }
            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (resenaexi != 0)
            {
                ModelState.AddModelError("", "YA AÑADISTE UNA *Reseña*");
            }
            if (ModelState.IsValid)
            {
                Recomendaciones datos = new()
                {
                    Estrellas = vm.Estrellas,
                    Descripcion = vm.Descripcion,
                    IdUsuarios = Convert.ToInt32(idusuario),
                    Fecha = DateTime.Now,
                };

                recomendacionesRepository.Insert(datos);

            }
            return View();
        }

        public IActionResult Carrito(int? id)
        {

            var idusuario = User.FindFirst("Id")?.Value;
            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return View();
            }

            //var cant = carritoRepository.GetAll().Where(x => x.IdUsuario == Convert.ToInt32(idusuario) && x.IdProducto == id);

            var existe = carritoRepository.GetAll()
                .FirstOrDefault(x => x.IdUsuario == Convert.ToInt32(idusuario) && x.IdProducto == id); //-----Deja de ser una lista y

            var xd = productosRepository.GetAll().FirstOrDefault(x => x.Id == id);
            if (xd.Stock >= 1)
            {
                if (existe != null)
                {

                    existe.Cantidad += 1;
                    xd.Stock = xd.Stock - 1;

                    carritoRepository.Update(existe);
                    productosRepository.Update(xd);
                }
                else
                {
                    Carrito datos = new()
                    {
                        IdUsuario = Convert.ToInt32(idusuario),
                        IdProducto = id.Value,
                        Cantidad = 1
                    };
                    carritoRepository.Insert(datos);
                    xd.Stock = xd.Stock - 1;
                    productosRepository.Update(xd);
                }

            }
            else
                return RedirectToAction("Login", "Account");


            return RedirectPreserveMethod("/Home/Productos/" + id + "");
        }

        public IActionResult VerCarrito()
        {


            var idusuario = User.FindFirst("Id")?.Value;
            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var vm = context.Usuarios.Where(x => x.Id == Convert.ToInt32(idusuario)).Select(x => new VerCarrito
                {
                    NombreUsuario = x.NombreUsuario,
                    PrecioTotal = x.Carrito.Sum(c =>
                 c.Cantidad * (c.IdProductoNavigation.Preciopromocion.HasValue && c.IdProductoNavigation.Preciopromocion > 0
                     ? c.IdProductoNavigation.Preciopromocion.Value
                     : c.IdProductoNavigation.Precio)),

                    listitacarrito = x.Carrito.Select(x => new CarritoViewModel
                    {
                        Id = x.IdProductoNavigation.Id,
                        Nombre = x.IdProductoNavigation.Nombre,
                        Categorias = x.IdProductoNavigation.IdCategoriaNavigation.Categorias,
                        Precio = x.IdProductoNavigation.Precio,
                        Preciopromocion = x.IdProductoNavigation.Preciopromocion,
                        IdUsuario = Convert.ToInt32(idusuario),
                        Cantidad = x.Cantidad,
                        IdProducto = x.IdProducto
                    }).ToList()
                }).FirstOrDefault();

                // Formateamos el PrecioTotal a dos decimales
                if (vm?.PrecioTotal.HasValue == true)
                {
                    vm.PrecioTotal = Math.Round(vm.PrecioTotal.Value, 2);
                }

                return View(vm);
            }


        }
        public IActionResult carritostock(int? id)
        {

            var idusuario = User.FindFirst("Id")?.Value;
            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return View();
            }

            //var cant = carritoRepository.GetAll().Where(x => x.IdUsuario == Convert.ToInt32(idusuario) && x.IdProducto == id);

            var existe = carritoRepository.GetAll()
                .FirstOrDefault(x => x.IdUsuario == Convert.ToInt32(idusuario) && x.IdProducto == id); //-----Deja de ser una lista y

            var xd = productosRepository.GetAll().FirstOrDefault(x => x.Id == id);
            if (xd.Stock >= 1)
            {
                if (existe != null)
                {

                    existe.Cantidad += 1;
                    xd.Stock = xd.Stock - 1;

                    carritoRepository.Update(existe);
                    productosRepository.Update(xd);
                }
                else
                {
                    Carrito datos = new()
                    {
                        IdUsuario = Convert.ToInt32(idusuario),
                        IdProducto = id.Value,
                        Cantidad = 1
                    };
                    carritoRepository.Insert(datos);
                    xd.Stock = xd.Stock - 1;
                    productosRepository.Update(xd);
                }

            }
            else
                return RedirectToAction("Login", "Account");


            return RedirectPreserveMethod("/Home/VerCarrito");
        }
        public IActionResult eliminarcarritostock(int? id)
        {

            var idusuario = User.FindFirst("Id")?.Value;
            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return View();
            }

            //var cant = carritoRepository.GetAll().Where(x => x.IdUsuario == Convert.ToInt32(idusuario) && x.IdProducto == id);

            var existe = carritoRepository.GetAll()
                .FirstOrDefault(x => x.IdUsuario == Convert.ToInt32(idusuario) && x.IdProducto == id); //-----Deja de ser una lista y

            var xd = productosRepository.GetAll().FirstOrDefault(x => x.Id == id);
            if (existe.Cantidad >= 1)
            {
                existe.Cantidad -= 1;
                if (existe.Cantidad == 0)
                {
                    xd.Stock = xd.Stock + 1;
                    carritoRepository.Update(existe);

                    carritoRepository.Delete(existe);

                    return RedirectPreserveMethod("/Home/VerCarrito");
                }


                xd.Stock = xd.Stock + 1;




                carritoRepository.Update(existe);
                productosRepository.Update(xd);


            }



            return RedirectPreserveMethod("/Home/VerCarrito");
        }






    }
}
