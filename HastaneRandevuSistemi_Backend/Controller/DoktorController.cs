
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
    public class DoktorController : ControllerBase
    {
        private AppDBContext dbContext;
        public DoktorController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [Authorize]
        [HttpGet]
        public IActionResult getAllDoktor()
        {
            var doktor = dbContext.Doktorlar
            .Include(s => s.Poliklinikler)
            .ToList();
            return Ok(doktor);
        }

          [Authorize]
        [HttpGet]
        [Route("{PoliklinikId:int}")]
        public IActionResult GetDoktorlarByPoliklinikId(int PoliklinikId)
        {
             var doktorlar = dbContext.Doktorlar
                              .Where(d => d.PoliklinikId == PoliklinikId)  // PoliklinikId'ye göre filtreleme
                              .ToList();

            if (doktorlar == null || !doktorlar.Any())
            {
                return NotFound();  // Doktor bulunamazsa 404 döner
            }

            return Ok(doktorlar);  // Doktorları başarıyla döndürür
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult createDoktor(DoktorDtos dto)
        {
            var doktorObj = new Doktor()  
            {
                DoktorName = dto.DoktorName,
                DoktorSurname=dto.DoktorSurname,
                PoliklinikId=dto.PoliklinikId,
            };

            dbContext.Doktorlar.Add(doktorObj);

            dbContext.SaveChanges();

            return Ok(doktorObj);
        }



        [HttpPut]
        [Route("{DoktorId:int}")]
        public IActionResult updateDoktor(int DoktorId, DoktorDtos dto)
        {
            var doktorObj = dbContext.Doktorlar.Find(DoktorId);
            if (doktorObj is null)
                return NotFound();

            doktorObj.DoktorName = dto.DoktorName;
            doktorObj.DoktorSurname = dto.DoktorSurname;
            doktorObj.PoliklinikId = dto.PoliklinikId;

            dbContext.SaveChanges();

            return Ok(doktorObj);
        }


        [HttpDelete]
        [Route("{DoktorId:int}")]
        public IActionResult updateDoktor(int DoktorId)
        {
            var doktorObj = dbContext.Doktorlar.Find(DoktorId);
            if (doktorObj is null)
                return NotFound();

            dbContext.Doktorlar.Remove(doktorObj);

            dbContext.SaveChanges();

            return Ok(doktorObj
            
            );
        }

    }
}