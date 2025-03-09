using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace randevuSistemi.Models
{
    public class Kullanici
    {
        public  int Id { get; set; } 
        public required string TcKimlikNo { get; set; } 
        public required string Name { get; set; }

        public required string Surname { get; set; } 

        [EnumDataType(typeof(CinsiyetEnum))]
        public required string Cinsiyet { get; set; } 
        public required string AnneName { get; set; } 
        public required string BabaName { get; set; } 

        public required string DogumYeri { get; set; } 

        public required string DogumTarihi { get; set; } 
        public required string Telefon { get; set; } 
        public required string Sifre { get; set; } 
        public required bool IsAdmin { get; set;}


    }

      public enum CinsiyetEnum
    {
        Erkek,
        Kadın
    }
}
