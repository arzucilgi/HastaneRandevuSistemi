

using System.Text.Json.Serialization;

namespace randevuSistemi.Models
{
    public class Sehir{
         
        public int Id {get; set;}

        public required string  SehirName { get; set; }
        
        [JsonIgnore]
        public virtual List<Hastane> Hastaneler { get; set; } = [];
    
    }
    
}