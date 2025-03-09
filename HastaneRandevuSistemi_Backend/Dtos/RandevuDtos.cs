using System.ComponentModel.DataAnnotations;


namespace randevuSistemi.Dtos
{
    public class RandevuDtos
{
    
    public int KullaniciId{get; set;}
   // public required virtual  Kullanici Kullanici { get; set; }
    public int SehirId{get; set;}
    public  int HastaneId { get; set; }
   // public required virtual  Hastane Hastane { get; set; }

    public int PoliklinikId { get; set; }
   // public  required virtual  Poliklinik Poliklinik { get; set; }

    public  int DoktorId { get; set; }
    //public required virtual  Doktor Doktor { get; set; }
}
}