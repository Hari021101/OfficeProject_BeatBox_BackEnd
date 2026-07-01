using Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class TransactionActionQueue : ITransactionActionQueue
{
    private readonly List<Func<Task>> _actions = new();
    private readonly ILogger<TransactionActionQueue> _logger;

    public TransactionActionQueue(ILogger<TransactionActionQueue> logger)
    {
        _logger = logger;
    }

    public void QueueAction(Func<Task> action)
    {
        lock (_actions)
        {
            _actions.Add(action);
        }
    }

    public async Task RunAllAsync()
    {
        List<Func<Task>> actionsToRun;
        lock (_actions)
        {
            actionsToRun = new List<Func<Task>>(_actions);
            _actions.Clear();
        }

        foreach (var action in actionsToRun)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                // Safety guard: prevent background task failures from disrupting request completion
                _logger.LogError(ex, "Error executing post-transaction action");
            }
        }
    }
}
