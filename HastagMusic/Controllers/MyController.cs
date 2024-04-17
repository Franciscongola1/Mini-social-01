using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HastagMusic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HastagMusic.Controllers
{
    public class MyController : Controller
    {
        private readonly IUserManagerServices _Usermanager;

        public MyController(IUserManagerServices usermanager)
        {
            _Usermanager = usermanager;
        }
        public IActionResult Perfil()
        {
            //*Verificar se o usuario está logado
            if(!HttpContext.Request.Cookies.ContainsKey("userid"))
            {
                // se não estiver redireciona para tela de login
                return RedirectToAction("SignIn","Account");
            }
            //* Obter id do ususario logado
            string _userid = $"{HttpContext.Request.Cookies["userid"]}";

            //* passando numa viewbag 
            ViewBag.UserID = _userid;
            return View(_Usermanager.GetAllUsers());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}