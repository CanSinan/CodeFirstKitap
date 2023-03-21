using CodeFirstKitap.Data;
using CodeFirstKitap.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstKitap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KitapController : ControllerBase
    {
        ApplicationDbContext _context; // global değişken

        // Kitapcontroller class ı ile ilgili bir işlem yaptığımda ApplicationDbcontext class ı tipinde bir nesneyi de oluştur.
        //Constructor Injection
        public KitapController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kitap>>> KitaplarıGetir() // async metotlardan gelen değeri yakalamak için await keywordunu kullanıyoruz.
        {
            List<Kitap> kitapListesi;
            // select * from Kitap yaptı.
            kitapListesi =await _context.Kitap.ToListAsync();
            return kitapListesi;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Kitap>>> KitapEkle(Kitap kitap) // kitap tipinde bir kitap objesine dönüştür
            // Liste aslında serialize edilmiş objeyi deserialize ederek kitap objesine dönüştürüyor
        {
            
            try
            {
                _context.Kitap.Add(kitap); // insert into
                await _context.SaveChangesAsync(); // ekleme işlemini kaydet.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Ok();// 200 durum kodu döncek.
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<Kitap>> KitapDetayGetir(int id) // id si "..." olan kaydı getir....
        {
            
            Kitap kitap = await _context.Kitap.FindAsync(id); // select * from Kitap where Id = id

            if (kitap == null)
            {
                return NotFound();
            }

            return kitap;
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<Kitap>>> KitapGunelle(Kitap kitap)
        {
            _context.Entry(kitap).State= EntityState.Modified; // Databasedeki verimi güncelliycem anlamına geliyor.
            try
            {
                await _context.SaveChangesAsync(); // yaptığımız değişiklikleri algıla // update komutunu yapıyor.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Kitap>> KitapSil(int id)
        {
            Kitap silinecekKitap = await _context.Kitap.FindAsync(id);
            _context.Kitap.Remove(silinecekKitap);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
