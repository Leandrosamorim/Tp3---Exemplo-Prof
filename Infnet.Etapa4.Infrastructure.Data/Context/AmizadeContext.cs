using Infnet.Etapa4.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infnet.Etapa4.Infrastructure.Data.Context
{
    public class AmizadeContext : DbContext
    {
        public AmizadeContext (DbContextOptions<AmizadeContext> options)
            : base(options)
        {
        }

        public DbSet<AmigoEntity> AmigoEntity { get; set; }
    }
}
