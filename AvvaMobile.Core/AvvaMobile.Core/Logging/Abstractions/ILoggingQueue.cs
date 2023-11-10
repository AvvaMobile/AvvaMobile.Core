using AvvaMobile.Core.Logging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvvaMobile.Core.Logging.Abstractions;

public interface ILoggingQueue { }

public interface ILoggingQueue<T> : ILoggingQueue
    where T : ILogModel
{
    Task Enqueue(T logModel);
    Task<T> Dequeue();
    Task<List<T>> DequeueAll();
}