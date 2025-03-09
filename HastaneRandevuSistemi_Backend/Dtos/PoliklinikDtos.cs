

namespace  randevuSistemi.Dtos
{
    public class PoliklinikDtos// bu alanda kullandıklarımız database e istekte buunurken doldurulması zorunlu alanlar olarak belirlendi.
    {
        
        public required string  PoliklinikName { get; set; }

        public  int HastaneId { get; set; }

    }
}