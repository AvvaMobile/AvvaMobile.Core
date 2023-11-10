using AvvaMobile.Core.Logging.Abstractions;
using AvvaMobile.Core.Logging.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvvaMobile.Core.Logging;

public class LoggingHostedService<TLogModel, TDataContext> : BackgroundService
    where TLogModel : ILogModel
    where TDataContext : DbContext
{
    private readonly ILoggingQueue<TLogModel> _loggingQueue;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public LoggingHostedService(ILoggingQueue<TLogModel> loggingQueue, IServiceScopeFactory serviceScopeFactory)
    {
        _loggingQueue = loggingQueue;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var logModel = await _loggingQueue.Dequeue();

            using var scope = _serviceScopeFactory.CreateScope();
            using var dataContext = scope.ServiceProvider.GetRequiredService<TDataContext>();

            logModel.CreatedDate = DateTime.Now;

            await dataContext.AddAsync(logModel, stoppingToken);
            await dataContext.SaveChangesAsync(stoppingToken);
        }
    }
}