using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastagMusic.Models
{
    public class Comments
    {
        public int ID{get;set;}
        public string? UserID{get;set;}
        public string? PostID{get;set;}
        public string? Comentarios{get;set;}
        public string? Hora{get;set;}
        public string? Nomeuser{get;set;}
        public ConcurrentBag<Comments>?ListComments {get;set;}
        public  List<PostViewModel>?ListPost {get;set;}
        public List<PostViewModel>?ListP =new();
    }
}