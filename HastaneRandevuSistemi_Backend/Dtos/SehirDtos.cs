

using System.ComponentModel.DataAnnotations;

namespace  randevuSistemi.Dtos
{
    public class SehirDtos
    {
        [Required(ErrorMessage = "Isim Alani Gereklidir")]
        public required string SehirName{ get; set; }

    }
}