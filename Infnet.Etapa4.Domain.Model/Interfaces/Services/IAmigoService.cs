using Infnet.Etapa4.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Infnet.Etapa4.Domain.Model.Interfaces.Services
{
    public interface IAmigoService
    {
        Task<IEnumerable<AmigoEntity>> GetAllAsync();
        Task<AmigoEntity> GetByIdAsync(int id);
        Task InsertAsync(AmigoEntity amigoEntity, Stream stream);
        Task UpdateAsync(AmigoEntity amigoEntity, Stream stream);
        Task DeleteAsync(AmigoEntity amigoEntity);
    }
}
