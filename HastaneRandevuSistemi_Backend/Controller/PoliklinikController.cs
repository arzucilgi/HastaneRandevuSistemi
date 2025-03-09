
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using randevuSistemi.Dtos;
using randevuSistemi.Models;
using randevuSistemi.Services;

namespace randevuSistemi.Controllers
{
    [ApiController]
     
    [Route("api/[controller]")]
    public class PoliklinikController : ControllerBase
    {
        private AppDBContext dbContext;
        public PoliklinikController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [Authorize]
        [HttpGet]
        public IActionResult getAllPoliklinik()
        {
            var poliklinikler = dbContext.Poliklinikler
            .Include(s => s.Hastane)
            .Include(d =>d.Doktorlar)
            .ToList();
            return Ok(poliklinikler);
        }

        [Authorize]
        [HttpGet]
        [Route("{PoliklinikId:int}")]
        public IActionResult getPoliklinikById(int PoliklinikId)
        {
            //var poliklinikObj = dbContext.Poliklinikler.Find(PoliklinikId);
             var poliklinikObj = dbContext.Poliklinikler
                              .Include(h => h.Hastane)
                              .Include(d=>d.Doktorlar)
                              .FirstOrDefault(h => h.Id == PoliklinikId);
            if (poliklinikObj is null)
                return NotFound();
            return Ok(poliklinikObj);
        }
        [Authorize]
        [HttpGet]
        [Route("Hastane/{HastaneId:int}")]
        public IActionResult GetPolikliniklerByHastaneId(int HastaneId)
        {
             // Seçilen HastaneId'ye göre poliklinikleri filtreleyerek alın
             var poliklinikler = dbContext.Poliklinikler
                                .Include(p => p.Hastane)  // Hastane bilgilerini de dahil et
                                .Include(p => p.Doktorlar)  // Doktor bilgilerini de dahil et
                                .Where(p => p.HastaneId == HastaneId)  // HastaneId'ye göre filtreleme
                                .ToList();

             if (poliklinikler == null || !poliklinikler.Any())
                  return NotFound();  // Poliklinik bulunamadıysa 404 döner

             return Ok(poliklinikler);  // Poliklinikleri başarıyla döndür
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult createPoliklinik(PoliklinikDtos dto)
        {
            var poliklinikObj = new Poliklinik()  
            {
                PoliklinikName = dto.PoliklinikName,
                HastaneId=dto.HastaneId,
            };

            dbContext.Poliklinikler.Add(poliklinikObj);

            dbContext.SaveChanges();

            return Ok(poliklinikObj);
        }

        [HttpPost]
        [Route("{poliklinikId:int}/Doktor")]
        public IActionResult addDoktor(int poliklinikId,DoktorPoliklinikDtos dto)
        {
            var poliklinikObj = dbContext.Poliklinikler.Include(p=>p.Doktorlar).FirstOrDefault(p=>p.Id==poliklinikId);
            var doktorObj=dbContext.Doktorlar.Find(dto.DoktorId);
            if(doktorObj is null || poliklinikObj is null){
                 return NotFound();
            }
           

            poliklinikObj.Doktorlar.Add(doktorObj);

            dbContext.SaveChanges();

            return Ok(poliklinikObj);
        }


        [HttpPut]
        [Route("{PoliklinikId:int}")]
        public IActionResult updatePoliklinik(int PoliklinikId, PoliklinikDtos dto)
        {
            var poliklinikObj = dbContext.Poliklinikler.Find(PoliklinikId);
            if (poliklinikObj is null)
                return NotFound();

            poliklinikObj.PoliklinikName = dto.PoliklinikName;
            poliklinikObj.HastaneId = dto.HastaneId;


            dbContext.SaveChanges();

            return Ok(poliklinikObj);
        }


        [HttpDelete]
        [Route("{PoliklinikId:int}")]
        public IActionResult updatePoliklinik(int PoliklinikId)
        {
            var poliklinikObj = dbContext.Poliklinikler.Find(PoliklinikId);
            if (poliklinikObj is null)
                return NotFound();

            dbContext.Poliklinikler.Remove(poliklinikObj);

            dbContext.SaveChanges();

            return Ok(poliklinikObj);
        }

    }
}