using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace randevuSistemi.Dtos
{
    public class LoginDto
    {

        [Required(ErrorMessage = "TcKimlik  gereklidir")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik Numarası 11 haneli olmalıdır.")]
        public required string TcKimlik { get; set; }

        [Required(ErrorMessage = "Şifre alanı gereklidir")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public required string Password { get; set; }
    }
}