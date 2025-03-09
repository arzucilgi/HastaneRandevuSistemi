
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
    public class HastaneController : ControllerBase
    {
        private AppDBContext dbContext;
        public HastaneController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [Authorize]
        [HttpGet]
        public IActionResult getAllHastane()
        {
            var hastaneler = dbContext.Hastaneler
            .Include(s => s.Sehir)
            .ToList();
            return Ok(hastaneler);
        }

        [Authorize]
        [HttpGet]
        [Route("{SehirId:int}")]
        public IActionResult getHastaneById(int SehirId)
        {
    
            var hastaneler = dbContext.Hastaneler
                              .Where(h => h.SehirId == SehirId) // ŞehirId'ye göre filtreledim
                              .Include(h => h.Sehir)           // İlgili şehir bilgisini dahil ettim
                              .ToList();                     

             if (hastaneler == null || !hastaneler.Any())  
                  return NotFound();

             return Ok(hastaneler);  
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult createHastane(HastaneDtos dto)
        {
            var hastaneObj = new Hastane()  
            {
                HastaneName = dto.HastaneName,
                SehirId=dto.SehirId,
            };

            dbContext.Hastaneler.Add(hastaneObj);

            dbContext.SaveChanges();

            return Ok(hastaneObj);
        }


        [HttpPut]
        [Route("{HastaneId:int}")]
        public IActionResult updateHastane(int HastaneId, HastaneDtos dto)
        {
            var hastaneObj = dbContext.Hastaneler.Find(HastaneId);

            if (hastaneObj is null)
                return NotFound();

            hastaneObj.HastaneName = dto.HastaneName;

            dbContext.SaveChanges();

            return Ok(hastaneObj);
        }


        [HttpDelete]
        [Route("{HastaneId:int}")]
        public IActionResult updateHastane(int HastaneId)
        {
            var hastaneObj = dbContext.Hastaneler.Find(HastaneId);
            if (hastaneObj is null)
                return NotFound();

            dbContext.Hastaneler.Remove(hastaneObj);

            dbContext.SaveChanges();

            return Ok(hastaneObj);
        }

    }
}