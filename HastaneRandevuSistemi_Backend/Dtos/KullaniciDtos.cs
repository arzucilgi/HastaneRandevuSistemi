
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Any;
using randevuSistemi.Models;

namespace randevuSistemi.Dtos
{
    public class KullaniciDtos
    {
        
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik Numarası 11 haneli olmalıdır.")]
        public required string TcKimlikNo { get; set; } 
        public required string Name { get; set; }

        public required string Surname { get; set; } 
         
        [EnumDataType(typeof(CinsiyetEnum))]
        public required string Cinsiyet { get; set; } 
        public required string AnneName { get; set; } 
        public required string BabaName { get; set; } 

        public required string DogumYeri { get; set; } 
        
        
        public required string DogumTarihi { get; set; }

        [Phone(ErrorMessage = "Geçersiz telefon numarası.")]
        public required string Telefon { get; set; }  

        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
         [Required(ErrorMessage = "Şifre alanı gereklidir")]
        public required string Sifre { get; set; } 

         public required bool IsAdmin { get; set;}
    
    }
}

