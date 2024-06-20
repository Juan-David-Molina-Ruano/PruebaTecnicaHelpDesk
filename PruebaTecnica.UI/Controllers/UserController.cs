using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using PruebaTecnica.BusinessEntities;
using PruebaTecnica.BusinessLogic;
using System.Security.Claims;

namespace PruebaTecnica.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly UserBL _userBL;
        public UserController(UserBL userBL)
        {
            _userBL = userBL;
        }


        [AllowAnonymous]
        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Create(User user)
        {
            string result = await _userBL.createUser(user);

            if (result == "Username already exists")
            {

                ViewBag.Error = "Username already exists";
                return View(user);
            }
            else if (result == "User created successfully")
            {
                // Handle successful creation
                return RedirectToAction("Login", "User");
            }
            else
            {
                // Handle unexpected errors
                ViewBag.Error = "An unexpected error occurred";
                return View(user);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Login(string ReturnUrl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login([Bind("UserName,UserPassword")] User user, string ReturnUrl)
        {
            if (user == null)
            {
                ViewBag.Error = "No se encuentra registrado";
                ViewBag.pReturnUrl = ReturnUrl;
                return View(user);
            }

            var usuarioAut = await _userBL.Login(user);

            if (usuarioAut != null && usuarioAut.Id > 0 && usuarioAut.UserName == user.UserName)
            {
                var claims = new[] {
                    new Claim(ClaimTypes.Name, usuarioAut.UserName),
                    new Claim("Id", usuarioAut.Id.ToString())
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true });

                if (!string.IsNullOrWhiteSpace(ReturnUrl))
                    return Redirect(ReturnUrl);
                else
                    return RedirectToAction("Index", "Question");
            }
            else
            {
                ViewBag.Error = "Credenciales incorrectas";
                ViewBag.pReturnUrl = ReturnUrl;
                return View(user);
            }
        }

    }
}
