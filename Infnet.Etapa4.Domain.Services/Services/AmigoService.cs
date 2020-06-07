using Infnet.Etapa4.Domain.Model.Entities;
using Infnet.Etapa4.Domain.Model.Interfaces.Infrastructure;
using Infnet.Etapa4.Domain.Model.Interfaces.Repositories;
using Infnet.Etapa4.Domain.Model.Interfaces.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infnet.Etapa4.Domain.Services.Services
{
    public class AmigoService : IAmigoService
    {
        private readonly IAmigoRepository _repository;
        private readonly IBlobService _blobService;
        private readonly IQueueMessage _queueService;

        public AmigoService(IAmigoRepository repository, IBlobService blobService, IQueueMessage queueService)
        {
            _repository = repository;
            _blobService = blobService;
            _queueService = queueService;
        }

        public async Task<IEnumerable<AmigoEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<AmigoEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task InsertAsync(AmigoEntity amigoEntity, Stream stream)
        {
            var newUri = await _blobService.UploadAsync(stream);
            amigoEntity.ImageUri = newUri;

            await _repository.InsertAsync(amigoEntity);
        }

        public async Task UpdateAsync(AmigoEntity amigoEntity, Stream stream)
        {
            if (stream != null)
            {
                await _blobService.DeleteAsync(amigoEntity.ImageUri);

                var newUri = await _blobService.UploadAsync(stream);
                amigoEntity.ImageUri = newUri;
            }

            await _repository.UpdateAsync(amigoEntity);
        }

        public async Task DeleteAsync(AmigoEntity amigoEntity)
        {
            await _blobService.DeleteAsync(amigoEntity.ImageUri);

            var email = new
            {
                Assunto = $"Exclusão de amigo {amigoEntity.Nome}",
                Corpo = $"Um amigo seu foi excluído. O nome do seu amigo excluído é: {amigoEntity.Nome}",
                EmailPara = "jcguimaraes@gmail.com"
            };

            //primeira forma de serializar objeto json/base64 (usando package Newtonsoft.Json)
            var jsonEmail = JsonConvert.SerializeObject(email);
            var bytesJsonEmail = UTF8Encoding.UTF8.GetBytes(jsonEmail);
            string jsonEmailBase64 = Convert.ToBase64String(bytesJsonEmail);

            //segunda forma de serializar objeto json/base64 (usando package System.Text.Json)
            //var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(email);
            //string jsonEmailBase64 = Convert.ToBase64String(jsonBytes);

            await _queueService.SendAsync(jsonEmailBase64);

            await _repository.DeleteAsync(amigoEntity);
        }

    }
}
