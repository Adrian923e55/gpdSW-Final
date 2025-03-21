using gpdSW.Helpers;
using gpdSW.Models.Entities;
using gpdSW.Models.ViewModels.AccountVM;
using gpdSW.repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace gpdSW.Controllers
{
    public class Account : Controller
    {
        CheeseandmoreContext context;
        Repository<Usuarios> repositoryUsuarios;

        public Account()
        {
            context = new CheeseandmoreContext();
            repositoryUsuarios = new Repository<Usuarios>(context);
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(RegistroViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Nombre))
                ModelState.AddModelError("", "Ingrese su nombre de usuario.");
            else if (string.IsNullOrWhiteSpace(vm.Correo))
                ModelState.AddModelError("", "Ingrese su correo.");
            else if (repositoryUsuarios.GetAll().Any(x => x.Correo.ToLower() == vm.Correo.ToLower())) // --
                ModelState.AddModelError("", "El correo ingresado ya se encuentra registrado.");
            else if (string.IsNullOrWhiteSpace(vm.Contraseña))
                ModelState.AddModelError("", "Ingrese su contraseña.");
            else if (!Regex.IsMatch(vm.Contraseña, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
                ModelState.AddModelError("", "La contraseña debe tener al menos 8 caracteres, una minúscula, una mayúscula y un número.");
            // --
            else if (string.IsNullOrWhiteSpace(vm.RepetirContraseña))
                ModelState.AddModelError("", "Repita la contraseña.");
            else if (vm.Contraseña != vm.RepetirContraseña)
                ModelState.AddModelError("", "Las contraseñas no coinciden, intente de nuevo.");

            if (ModelState.IsValid)
            {
                Usuarios user = new Usuarios
                {
                    NombreUsuario = vm.Nombre,
                    Correo = vm.Correo,
                    Contrasena = Encriptacion.Encriptar(vm.Contraseña),
                    Rol = 2 // cliente por defecto
                };

                repositoryUsuarios.Insert(user);

                return RedirectToAction("RegistroCompleto");
            }
            else
                return View(vm);
        }

        public IActionResult RegistroCompleto()
        {
            return View();
        }

        // --

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Correo))
                ModelState.AddModelError("", "Ingrese su correo.");
            if (string.IsNullOrWhiteSpace(vm.Contraseña))
                ModelState.AddModelError("", "Ingrese su contraseña.");

            if (ModelState.IsValid)
            {
                var user = repositoryUsuarios.GetAll().FirstOrDefault(x => x.Correo == vm.Correo && x.Contrasena == Encriptacion.Encriptar(vm.Contraseña));

                if (user == null)
                {
                    ModelState.AddModelError("", "El nombre o contraseña son incorrectos");
                    return View(vm);
                }

                // Claims
                List<Claim> claims =
                [
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Nombre", user.NombreUsuario),
                    new Claim(ClaimTypes.Email, user.Correo),
                    new Claim("Rol", user.Rol.ToString()),
                ];

                ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new(identity);

                this.HttpContext.SignInAsync(principal);

                // Redireccionar según su rol 
                if (user.Rol == 2)
                    return RedirectToAction("Index", "Home", new { area = "User" }); // user
                                                                                     //return RedirectToAction("Index", "Home");

                else if (user.Rol == 1)
                    return RedirectToAction("Index", "Home", new { area = "Admin" }); // admin
                else
                    return RedirectToAction("Index", "Home");
            }
            return View(vm);
        }

        // --

        [HttpGet]
        public IActionResult RestablecerContraseña()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RestablecerContraseña(RestContViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Correo))
                ModelState.AddModelError("", "Ingrese su correo, por favor.");
            else if (repositoryUsuarios.GetAll().Any(x => x.Correo.ToLower() == vm.Correo.ToLower()))
            {
                string otpGenerado = new Random().Next(10000, 99999).ToString();

                TempData["OTP"] = otpGenerado;
                TempData["OTPExpiracion"] = DateTime.Now.AddMinutes(5);
                TempData["Correo"] = vm.Correo;

                // Enviar correo
                string correoDestino = vm.Correo;
                string asunto = "Código de Restablecimiento de Contraseña";
                string mensaje = $"Tu código de verificación es: {otpGenerado}";

                Correo.EnviarCorreo(correoDestino, asunto, mensaje);


                return RedirectToAction("ValidarOTP");
            }
            else
                ModelState.AddModelError("", "El correo ingresado no se encuentra registrado.");

            return View();
        }

        [HttpGet]
        public IActionResult ValidarOTP()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ValidarOTP(RestContViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.OTP.ToString()))
                ModelState.AddModelError("", "Ingrese el código enviado a su correo.");

            if (ModelState.IsValid)
            {
                string otpIngresado = string.Join("", vm.OTP);
                string otpGenerado = TempData["OTP"]?.ToString();
                DateTime? otpExpiracion = TempData["OTPExpiracion"] as DateTime?;

                if (otpIngresado == otpGenerado && otpExpiracion.HasValue && otpExpiracion.Value > DateTime.Now)
                    return RedirectToAction("CambiarContraseña");
                else
                    return RedirectToAction("ValidacionFallida");
            }
            return View();
        }

        [HttpGet]
        public IActionResult CambiarContraseña()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CambiarContraseña(RegistroViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Contraseña))
                ModelState.AddModelError("", "Ingrese su nueva contraseña.");
            else if (!Regex.IsMatch(vm.Contraseña, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
                ModelState.AddModelError("", "La contraseña debe tener al menos 8 caracteres, una minúscula, una mayúscula y un número.");
            // --
            else if (string.IsNullOrWhiteSpace(vm.RepetirContraseña))
                ModelState.AddModelError("", "Repita la nueva contraseña.");
            else if (vm.Contraseña != vm.RepetirContraseña)
                ModelState.AddModelError("", "Las contraseñas no coinciden, intente de nuevo.");

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            string correo = TempData["Correo"]?.ToString();

            if (string.IsNullOrEmpty(correo))
            {
                ModelState.AddModelError("", "Correo no encontrado.");
            }

            var user = repositoryUsuarios.GetAll().FirstOrDefault(x => x.Correo.ToLower() == correo.ToLower());

            if (user != null)
            {
                // Actualizar la contraseña del usuario
                user.Contrasena = Encriptacion.Encriptar(vm.Contraseña);
                repositoryUsuarios.Update(user);

                return RedirectToAction("ValidacionCorrecta");
            }
            else
            {
                ModelState.AddModelError("", "Usuario no encontrado.");
                return View(vm);
            }
        }

        public IActionResult ValidacionCorrecta()
        {
            return View();
        }
        public IActionResult ValidacionFallida()
        {
            return View();
        }

        // --

        public IActionResult CerrarSesion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}

