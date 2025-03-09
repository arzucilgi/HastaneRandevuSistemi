

using System.Text.Json.Serialization;

namespace randevuSistemi.Models
{
    public class Poliklinik
    {
        public int Id { get; set; }
        public required string PoliklinikName { get; set; }
        public int HastaneId { get; set; }
        public  virtual Hastane? Hastane { get; set; }

       public virtual List<Doktor> Doktorlar { get; set; } = [];
    
    }
}
