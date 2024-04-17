using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastagMusic.Models
{
    public class Post
    {
        public string? Id{get;set;}
        public string? UserID{get;set;}
        public string? Content{get;set;}
        public string? UserName{get;set;}
        public string? FileName{get;set;}
        public DateTime Hora{get;set;}
    }
}