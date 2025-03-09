
using System.Text.Json.Serialization;

namespace randevuSistemi.Models
{
    public class Doktor
    {
        public int Id { get; set; }
        public required string DoktorName { get; set; }
        public required string DoktorSurname { get; set; }

        public  int PoliklinikId{ get; set;}

        [JsonIgnore]
        public virtual List<Poliklinik> Poliklinikler { get; set; } = [];
        
      
    }
}
