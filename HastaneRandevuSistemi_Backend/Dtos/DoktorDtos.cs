

namespace randevuSistemi.Dtos
{
    public class DoktorDtos{

        public required string  DoktorName { get; set; }

        // Foreign key: Bu hastane hangi şehre ait?
        public required string DoktorSurname { get; set; }

        public  int PoliklinikId { get; set; }
    }
}