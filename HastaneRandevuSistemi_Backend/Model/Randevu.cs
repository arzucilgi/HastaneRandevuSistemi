using System.ComponentModel.DataAnnotations;


namespace randevuSistemi.Models
{
    public class Randevu
{
    public int Id { get; set; }
    
    public int KullaniciId{get; set;}
    public  virtual  Kullanici? Kullanici { get; set; }
    public int SehirId{get; set;}

     public  virtual  Sehir? Sehir { get; set; } 

    public  int HastaneId { get; set; }
    public  virtual  Hastane? Hastane { get; set; }

    public int PoliklinikId { get; set; }
    public   virtual  Poliklinik? Poliklinik { get; set; }

    public  int DoktorId { get; set; }
    public  virtual  Doktor? Doktor { get; set; }
    
}
}