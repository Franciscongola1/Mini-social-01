using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HastagMusic.Models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HastagMusic.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManagerServices _Usermanager;

        public AccountController(IUserManagerServices usermanager)
        {
            _Usermanager = usermanager;
        }


        public IActionResult SignIn()
        {
            if(HttpContext.Request.Cookies.ContainsKey("userid"))
            {
                return RedirectToAction("Index","home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(Users _user)
        {
            await Task.Run(() =>
            {
            // Simula uma operação de I/O assíncrona
            Task.Delay(100).Wait();
            });

            
            string msg = "";
           if(!String.IsNullOrWhiteSpace(_user.UserName) && !String.IsNullOrWhiteSpace(_user.Senha))
           {
                foreach (var item in _Usermanager.GetAllUsers().Where(u => u.UserName ==_user.UserName  && u.Senha == _user.Senha))
                {
                    if(item.UserName ==_user.UserName && item.Senha == _user.Senha)
                    {
                        //! Configuração do Cookie
                        CookieOptions configCookie = new()
                        {
                            Expires = new DateTimeOffset(DateTime.Now.AddHours(2)),
                            IsEssential = true,
                        };
                        Response.Cookies.Append("userid",$"{item.Id}",configCookie);
                        return RedirectToAction("Index","Home");
                    }
                }
           }else
           {
             msg= "Preencha  os formularios!";
           }
           ViewBag.erro = msg;
            return View();
        }

        //----------------------------------------------
        public IActionResult SignUp()
        {
            //Verificar primeiro se ja esta logado
            if(HttpContext.Request.Cookies.ContainsKey("userid"))
            {
                return RedirectToAction("Index","home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(Users _user)
        {
            await Task.Run(() =>
            {
            // Simula uma operação de I/O assíncrona
            Task.Delay(100).Wait();
            });


            Random rd = new();
            //!gerando id 
            _user.Id = Guid.NewGuid().ToString()[..15].Replace("-","");

            //*Criando um username randomicamente
            string newUsername = _user.FirstName.ToLower() + rd.Next(2,1000);
             _user.UserName = newUsername.Replace(" ","_");

             //? Pesquisar no banco caso exista um usuario com mesmo nome que pretende usar
             bool verifyUserNamesEqual = _Usermanager.GetAllUsers().Any(u =>u.UserName == _user.UserName);
             if(verifyUserNamesEqual)
             {
                newUsername = _user.FirstName.ToLower() + rd.Next(3,2000);//*Gera outro username
             }
            ViewBag.username = _user.UserName;
            //faz a falidação para os nomes user não colidirem
            //Adicionado no Banco
            _Usermanager.CreateNewUser(_user);

            return RedirectToAction("signin");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}