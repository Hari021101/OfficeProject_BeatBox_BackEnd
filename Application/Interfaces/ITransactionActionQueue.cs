using System;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ITransactionActionQueue
{
    void QueueAction(Func<Task> action);
    Task RunAllAsync();
}
