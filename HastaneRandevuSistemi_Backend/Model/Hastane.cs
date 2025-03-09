

using System.Text.Json.Serialization;

namespace randevuSistemi.Models
{
    public class Hastane{
         
        public int Id {get; set;}

        public required string  HastaneName { get; set; }

        public  required int SehirId { get; set; }
        public   Sehir? Sehir { get; set; }
    
       
      
    }
}