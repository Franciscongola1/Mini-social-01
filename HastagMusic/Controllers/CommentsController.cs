using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HastagMusic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HastagMusic.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IUserManagerServices _Usermanager;

        public CommentsController(IUserManagerServices usermanager)
        {
            _Usermanager = usermanager;
        }

        //* Method | Post
        public IActionResult Comment(string userId)
        {
            ViewBag.idpost = userId;
            
            //
            var d =new List<PostViewModel>();
            foreach (var item in  _Usermanager.GetAllPost().Where(id =>id.Id == userId))
            {
                d.Add(new PostViewModel{
                    UserID = item.UserID,
                    UserName = item.UserName,
                    Content = item.Content,
                    FileName = item.FileName,
                    Id = item.Id,
                    Hora = item.Hora,
                    Type = item.Type
                });
            }
            var c = new Comments{
                ListPost = d,
                ListComments = UserManagerServices.GetCommets(),
            };
            return View(c);
        }
        [HttpPost]
        public IActionResult Add(Comments co,string userId)
        {
            
            string _userid = $"{HttpContext.Request.Cookies["userid"]}";

            var username =  _Usermanager.GetUserByID(_userid);

            var relacao =new Comments{Comentarios = co.Comentarios,UserID = _userid, Nomeuser = username.UserName,PostID = userId,Hora = DateTime.Now.ToShortDateString()};
            _Usermanager.AddNewComment(relacao);
            return RedirectToAction("Comment",new{userId});
        }

    }
}