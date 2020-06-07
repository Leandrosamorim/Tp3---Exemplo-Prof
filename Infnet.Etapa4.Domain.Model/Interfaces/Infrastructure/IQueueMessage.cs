using System.Threading.Tasks;

namespace Infnet.Etapa4.Domain.Model.Interfaces.Infrastructure
{
    public interface IQueueMessage
    {
        Task SendAsync(string messageText);
    }
}
