using AvvaMobile.Core.Logging.Abstractions;
using AvvaMobile.Core.Logging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AvvaMobile.Core.Logging;

public class LoggingQueue<TLogModel> : ILoggingQueue<TLogModel>
    where TLogModel : ILogModel
{
    private readonly Channel<TLogModel> _queue;

    public LoggingQueue(BoundedChannelOptions options)
    {
        _queue = Channel.CreateBounded<TLogModel>(options);
    }

    public async Task Enqueue(TLogModel logModel)
    {
        await _queue.Writer.WriteAsync(logModel);
    }

    public async Task<TLogModel> Dequeue()
    {
        var logModel = await _queue.Reader.ReadAsync();

        return logModel;
    }

    public async Task<List<TLogModel>> DequeueAll()
    {
        var logModelsEnumerable = _queue.Reader.ReadAllAsync();

        var logModels = new List<TLogModel>();

        await foreach (var logModel in logModelsEnumerable)
        {
            logModels.Add(logModel);
        }

        return logModels;
    }
}