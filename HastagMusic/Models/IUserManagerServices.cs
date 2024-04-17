using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastagMusic.Models
{
    public interface IUserManagerServices
    {
        ConcurrentBag<Users>GetAllUsers();
        ConcurrentBag<PostViewModel>GetAllPost();

        Users GetUserByID(string? userID);
        PostViewModel GetPostByID(string? postid);
        void AddNewComment(Comments comment);
        void CreateNewUser(Users user);
        void AddNewFollower(Seguir seguindo);
        void RemoveFollower(int seguidoID);
        void CreateNewPost(PostViewModel post);
        void RemoverGosto(int gostoID);
        void AddGosto(Gostar gosto);
    }
}