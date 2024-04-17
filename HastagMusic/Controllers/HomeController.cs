using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HastagMusic.Models;
using Microsoft.AspNetCore.Http.Connections;

namespace HastagMusic.Controllers;

public class HomeController : Controller
{
        private readonly IUserManagerServices _Usermanager;

        public HomeController(IUserManagerServices usermanager)
        {
            _Usermanager = usermanager;
        }

    public async Task<IActionResult> Index()
    {
        await Task.Run(() =>
        {
            Task.Delay(100).Wait();
        });

        if(!HttpContext.Request.Cookies.ContainsKey("userid"))
        {
           return RedirectToAction("SignIn","Account");
        }
        ViewBag.id =  $"{HttpContext.Request.Cookies["userid"]}";
        return View(_Usermanager.GetAllPost());
    }

    public async Task<IActionResult> Friends()
    {
        await Task.Run(() =>
        {
            Task.Delay(100).Wait();
        });

        if(!HttpContext.Request.Cookies.ContainsKey("userid"))
        {
           return RedirectToAction("SignIn","Account");
        }
        string _userid = $"{HttpContext.Request.Cookies["userid"]}";
        ViewBag.UserID = _userid;
        return View(_Usermanager.GetAllUsers());
    }

    public async Task<IActionResult> Seguir(string? userId)
    {
        await Task.Run(() =>
        {
            Task.Delay(100).Wait();
        });
        //Obter id do usuario que esta logado do cookie
        string? userID =  $"{HttpContext.Request.Cookies["userid"]}";

        // seguidor(eu) = meuID, seguido(ele) = idDelemesmo
        var realacaoSeguir = new Seguir{SeguidorID = userID, SeguidoID = userId};

        //gravando dados no banco
        _Usermanager.AddNewFollower(realacaoSeguir);  

        return RedirectToAction("Friends",new { userId });
    }

    public async Task<IActionResult> DeixarDeSeguir(string? userId)
    {
        await Task.Run(() =>
        {
            Task.Delay(100).Wait();
        });
         //Obter id do usuario que esta logado do cookie
        string? userID = $"{HttpContext.Request.Cookies["userid"]}";
        int id = 0;
        
        // buscar seguido
       foreach (var item in UserManagerServices.GetSeguidores().Where(r =>r.SeguidorID == userID && r.SeguidoID ==userId))
       {
          id = item.ID;
       }

        // adicionar ao banco
        _Usermanager.RemoveFollower(id);
     
        return RedirectToAction("Friends",new {userID});
    }

    //* pesquisar users
    public async Task<IActionResult> FindFor(string user)
    {
        await Task.Run(() =>
        {
            Task.Delay(100).Wait();
        });

        var itemEncotrado = _Usermanager.GetAllUsers().Where(i => i.UserName.Contains(user) || i.Email.Contains(user));
        return View("Friends",itemEncotrado);
    }

    //* Method | GET
    public IActionResult Post()
    {
        string _userid = $"{HttpContext.Request.Cookies["userid"]}";
        var username =  _Usermanager.GetUserByID(_userid);
        ViewBag.UserName = username.UserName;
        return View();
    }

    //? Method | POST
    [HttpPost]
    public async Task<IActionResult> Post(PostViewModel model)
    {
        //verifica 
        if(ModelState.IsValid)
        {
            // verifica se os tipo de arquivo é valido
            if((model.Tipo == FileType.Image && !model.File.ContentType.StartsWith("image")) || (model.Tipo == FileType.Video && !model.File.ContentType.StartsWith("video")))
            {
                ModelState.AddModelError("Arquivo","tipo de arquivo inválido");
                return View(model);
            }

            //obtendo id do usuario logado
            string _userid = $"{HttpContext.Request.Cookies["userid"]}";

            //caminho das pasta imagens e videos
            var uploadPath = model.Tipo == FileType.Image? "wwwroot/uploads/imagens": "wwwroot/uploads/videos";
            
            //Obtendo raiz da wwwroot
            var basePath = Path.Combine(Directory.GetCurrentDirectory(),uploadPath,model.File.FileName);
            using FileStream fileStream = new(basePath,FileMode.Create);
                await model.File.CopyToAsync(fileStream);
                var relacaoPost =new PostViewModel{Id = Guid.NewGuid().ToString(),Content = model.Content,UserID = _userid,UserName =_Usermanager.GetUserByID(_userid).UserName,FileName = model.File.FileName,Hora = DateTime.Now,Tipo = model.Tipo};
                _Usermanager.CreateNewPost(relacaoPost);

            return RedirectToAction("Index","Home");
        }
        return View(model);
    }


    public IActionResult Gostar(string  userId)
    {
        //obtendo id do usuario
        string? usuarioID =  $"{HttpContext.Request.Cookies["userid"]}";

        //fornecendo dados
        var relacaoGostos = new Gostar{ UserID = usuarioID, PostID = userId};
 
        //!adicionado os dados no metodo que vai gravar no banco
        _Usermanager.AddGosto(relacaoGostos);

        //?redirecionado a view para Index
         return RedirectToAction("index");
    }
    public IActionResult NaoGostar(string userId)
    {
        //obtendo id do usuario
        string? usuarioID =  $"{HttpContext.Request.Cookies["userid"]}";
        int  id =0;
         // buscar seguido
       foreach (var item in UserManagerServices.GetGostos().Where(g => g.UserID == usuarioID  && g.PostID == userId))
       {
          id = item.ID;
       }
       //metodo que vai remover o gosto de acardo  com id fornecido acima
       _Usermanager.RemoverGosto(id);


        return RedirectToAction("index");
    }
    public async Task<IActionResult>Explorar()
    {
        //simulacão de I/O
        await Task.Run(() =>
        {
            Task.Delay(100).Wait();
        });

        //Verifica se usuario esta ou não logado
        if(!HttpContext.Request.Cookies.ContainsKey("userid"))
        {
           return RedirectToAction("SignIn","Account");
        }
        // Obtendo ID do usuario logado
        ViewBag.id =  $"{HttpContext.Request.Cookies["userid"]}";
        return View(_Usermanager.GetAllPost());
    }

    public IActionResult Logout()
    {
        //* Verifica se realmente exite usuario logado
        if(HttpContext.Request.Cookies.ContainsKey("userid"))
        {   
            // se tiver remove o cookie    
            Response.Cookies.Delete("userid");

            //redireciona-o para tela de login
            return RedirectToAction("signIn","account");
        }

        return View("signIn","account");
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
