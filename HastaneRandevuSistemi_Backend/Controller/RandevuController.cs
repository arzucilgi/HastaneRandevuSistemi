
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using randevuSistemi.Dtos;
using randevuSistemi.Models;
using randevuSistemi.Services;

namespace randevuSistemi.Controllers
{    
    [ApiController]
       [Authorize]
    [Route("api/[controller]")]
    public class RandevuController : ControllerBase
    {
        private AppDBContext dbContext;
        public RandevuController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
     
        [HttpGet]
        public IActionResult getAllRandevu()
        {
            var randevular = dbContext.Randevular
            .Include(s=>s.Sehir)
            .Include(h => h.Hastane)
            .Include(p => p.Poliklinik)
            .Include(d => d.Doktor)
            .Include(k => k.Kullanici)
            .ToList();
            return Ok(randevular);
        }


        [HttpGet]
        [Route("{RandevuId:int}")]
        public IActionResult getRandevuById(int RandevuId)
        {
            //var randevuObj = dbContext.Randevular.Find(RandevuId);
             var randevuObj = dbContext.Randevular
                              .Include(s => s.Sehir)
                              .Include(h => h.Hastane)
                              .Include(p => p.Poliklinik)
                              .Include(d=>d.Doktor)
                              .Include(k => k.Kullanici)
                              .FirstOrDefault(h => h.Id == RandevuId);
            if (randevuObj is null)
                return NotFound();
            return Ok(randevuObj);
        }

        [HttpGet]
        [Route("user/{KullaniciId:int}")]
        public IActionResult getRandevuByUserId(int KullaniciId)
        {
            var randevuObj = dbContext.Randevular
                              .Include(s => s.Sehir)
                              .Include(h => h.Hastane)
                              .Include(p => p.Poliklinik)
                              .Include(d => d.Doktor)
                              .Include(k => k.Kullanici)
                              .Where(r => r.KullaniciId == KullaniciId)
                              .ToList();
    
            if (randevuObj is null)
                return NotFound();
    
            return Ok(randevuObj);
        }



        [HttpPost]
        public IActionResult createHastane(RandevuDtos dto)
        {
            var randevuObj = new Randevu()  
            {
                SehirId=dto.SehirId,
                KullaniciId=dto.KullaniciId,
                DoktorId=dto.DoktorId,
                PoliklinikId=dto.PoliklinikId,
                HastaneId=dto.HastaneId,
            };

            dbContext.Randevular.Add(randevuObj);

            dbContext.SaveChanges();

            return Ok(randevuObj);
        }


        [HttpPut]
        [Route("{RandevuId:int}")]
        public IActionResult updateRandevu(int RandevuId, RandevuDtos dto)
        {
            var randevuObj = dbContext.Randevular.Find(RandevuId);
            if (randevuObj is null)
                return NotFound();

            randevuObj.KullaniciId = dto.KullaniciId;
            randevuObj.DoktorId=dto.DoktorId;
            randevuObj.PoliklinikId=dto.DoktorId;
            randevuObj.HastaneId = dto.HastaneId;
            randevuObj.SehirId=dto.SehirId;


            dbContext.SaveChanges();

            return Ok(randevuObj);
        }


        [HttpDelete]
        [Route("{RandevuId:int}")]
        public IActionResult updateRandevu(int RandevuId)
        {
            var randevuObj = dbContext.Randevular.Find(RandevuId);
            if (randevuObj is null)
                return NotFound();

            dbContext.Randevular.Remove(randevuObj);

            dbContext.SaveChanges();

            return Ok(randevuObj);
        }

    }
}