using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HastagMusic.Models
{
    public class UserManagerServices : IUserManagerServices
    {
        private static ConcurrentBag<Users>GetListUsers() => AcessoBancoDados.GetUsersToBD();
        private static ConcurrentBag<PostViewModel>GetPost() => AcessoBancoDados.GetPostToBD();
        private static List<Seguir>ListSeguidor() => AcessoBancoDados.ConsultaSeguidorToBD();
        private static ConcurrentBag<Gostar>ListGostos() =>  AcessoBancoDados.ConsultaGostosToBD();
        private static ConcurrentBag<Comments>ListComments() => AcessoBancoDados.ConsultarComments();
        public void AddNewComment(Comments comment)
        {
            AcessoBancoDados.AddComentariosToBD(comment);
        }
        public static ConcurrentBag<Comments>GetCommets() => ListComments();
        public void CreateNewPost(PostViewModel post)
        {
           AcessoBancoDados.AddPostToBD(post);
        }
               public void AddNewFollower(Seguir seguindo)
        {
            AcessoBancoDados.AddSeguidorToBD(seguindo);
        }
        //Metodo Deixar de seguir
        public void RemoveFollower(int seguidoID)
        {
            //var id = seguidoID.Id;
            AcessoBancoDados.DeletarSeguidorToBD(seguidoID);
        }
        public void CreateNewUser(Users user)
        {
            AcessoBancoDados.AddUserToBD(user);
        }

                //Metodo responsavel pelos gostos
        public void AddGosto(Gostar gosto)
        {
            AcessoBancoDados.AddGostosToBD(gosto);
        }

        public void RemoverGosto(int gostoID)
        {
            AcessoBancoDados.DeletarGostoToBD(gostoID);
        }


        //obter todos os seguidores
        public static List<Seguir> GetSeguidores() =>ListSeguidor();
        public ConcurrentBag<Users>GetAllUsers() =>GetListUsers();
        public ConcurrentBag<PostViewModel>GetAllPost() =>GetPost();
        public static ConcurrentBag<PostViewModel>GetPosts() =>GetPost();
        public static ConcurrentBag<Gostar>GetGostos() => ListGostos();

        public Users GetUserByID(string? userID)=> GetListUsers().FirstOrDefault(i =>i.Id == userID);
        public PostViewModel GetPostByID(string? postid) =>GetPost().FirstOrDefault(p =>p.Id ==postid);

    }
}