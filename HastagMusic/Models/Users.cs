using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HastagMusic.Models
{
    public class Users
    {
        public string?  Id{get;set;}

        [Required]
        public string? FirstName{get;set;}

        [Required]
        public string? LastName{get;set;}

        [Required]
        public string? UserName{get;set;}

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email{get;set;}

        [Required]
        [DataType(DataType.Password)]
        public string? Senha{get;set;}


        public string? Estilo{get;set;}
        public string? Modo{get;set;}
    }
}