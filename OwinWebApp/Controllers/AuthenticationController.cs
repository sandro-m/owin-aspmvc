using Microsoft.Owin.Security;
using OwinWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace OwinWebApp.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        [HttpGet]
        // GET: Authentication
        public ActionResult Login(string ReturnUrl)
        {
            var model = new Login
            {
                ReturnUrl= ReturnUrl
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {
            if (!ModelState.IsValid) {
                return View(model);
            }

            if (model.User == "teste" && model.Senha == "123") {
                var identity = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.Name, "Teste Sobrenome"),
                    new Claim(ClaimTypes.Email, "teste@gmail.com"),
                    new Claim(ClaimTypes.Country, "Brasil"),
                    new Claim(ClaimTypes.Role, "Admin")
                },
                "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            // Falha na autenticação
            return View();
        }

        private string GetRedirectUrl(string returnUrl)
        {
            return string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl) ? Url.Action("Index", "Home") : returnUrl;
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignOut("ApplicationCookie");

            return RedirectToAction("Index", "Home");
        }
    }
}