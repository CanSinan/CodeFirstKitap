using CodeFirstKitap.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CodeFirstKitap.Data
{
    public class ApplicationDbContext:DbContext
        // Benim database imdeki yapılacak tüm işlerden sorumlu
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options) // base = türediğin clasın konstruktoruna bunları gönder.
        {

        }
        public DbSet<Kitap> Kitap { get; set; }
    }
}
