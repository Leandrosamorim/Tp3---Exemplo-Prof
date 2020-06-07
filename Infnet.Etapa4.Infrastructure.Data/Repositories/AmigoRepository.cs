using Infnet.Etapa4.Domain.Model.Entities;
using Infnet.Etapa4.Domain.Model.Interfaces.Repositories;
using Infnet.Etapa4.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infnet.Etapa4.Infrastructure.Data.Repositories
{
    public class AmigoRepository : IAmigoRepository
    {
        private readonly AmizadeContext _context;

        public AmigoRepository(AmizadeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AmigoEntity>> GetAllAsync()
        {
            return await _context.AmigoEntity.ToListAsync();
        }

        public async Task<AmigoEntity> GetByIdAsync(int id)
        {
            return await _context.AmigoEntity.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task InsertAsync(AmigoEntity amigoEntity)
        {
            _context.Add(amigoEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AmigoEntity amigoEntity)
        {
            _context.Update(amigoEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AmigoEntity amigoEntity)
        {
            _context.Remove(amigoEntity);
            await _context.SaveChangesAsync();
        }

    }
}
