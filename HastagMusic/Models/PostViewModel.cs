using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HastagMusic.Models
{
    public enum FileType
    {
        Image,
        Video
    }
    public class PostViewModel
    {
        public string? Id{get;set;}
        public string? UserID{get;set;}
        public string? Content{get;set;}
        public string? UserName{get;set;}
        public string? FileName{get;set;}
        public string? Type{get;set;}
        public DateTime Hora{get;set;}

        [Required(ErrorMessage = "Por favor, selecione um arquivo.")]
        [DataType(DataType.Upload)]  
        public IFormFile? File{get;set;}

        [Required(ErrorMessage = "Por favor, selecione o tipo de arquivo.")]
        public FileType Tipo{get;set;}
    }
}