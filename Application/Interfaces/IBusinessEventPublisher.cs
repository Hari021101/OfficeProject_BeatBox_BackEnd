using System.Threading.Tasks;
using Application.Common.Events;

namespace Application.Interfaces;

public interface IBusinessEventPublisher
{
    Task PublishAsync(BusinessEvent businessEvent);
}
