
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
    public class SehirlerController : ControllerBase
    {
        private AppDBContext dbContext;
        public SehirlerController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult getAllSehirler()
        {
            var sehirler = dbContext.Sehirler
             .Include(s => s.Hastaneler) // Sehir ile Hastane ilişkisini dahil et
             .ToList();
            return Ok(sehirler);
        }


        [HttpGet]
        [Route("{SehirId:int}")]
        public IActionResult getSehirById(int SehirId)
        {
            var sehirObj = dbContext.Sehirler.Find(SehirId);
            // var sehirOBJ = dbContext.Sehirler.FirstOrDefault(p => p.Id == SehirId);
            if (sehirObj is null)
                return NotFound();
            return Ok(sehirObj);
        }

         [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult createSehir(SehirDtos dto)
        {
            var sehiryObj = new Sehir()
            {
                SehirName = dto.SehirName,
            };

            dbContext.Sehirler.Add(sehiryObj);

            dbContext.SaveChanges();

            return Ok(sehiryObj);
        }


        [HttpPut]
        [Route("{SehirId:int}")]
        public IActionResult updateSehir(int SehirId, SehirDtos dto)
        {
            var sehirObj = dbContext.Sehirler.Find(SehirId);
            if (sehirObj is null)
                return NotFound();

            sehirObj.SehirName = dto.SehirName;

            dbContext.SaveChanges();

            return Ok(sehirObj);
        }


        [HttpDelete]
        [Route("{SehirId:int}")]
        public IActionResult updateCategory(int SehirId)
        {
            var sehirObj = dbContext.Sehirler.Find(SehirId);
            if (sehirObj is null)
                return NotFound();

            dbContext.Sehirler.Remove(sehirObj);

            dbContext.SaveChanges();

            return Ok(sehirObj);
        }

    }
}