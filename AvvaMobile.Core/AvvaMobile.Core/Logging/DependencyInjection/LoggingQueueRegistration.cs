using AvvaMobile.Core.Logging.Abstractions;
using AvvaMobile.Core.Logging.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AvvaMobile.Core.Logging.DependencyInjection;

public static class LoggingQueueRegistration
{
    public static IServiceCollection AddLogging<TLogModel, TDataContext>(this IServiceCollection services, int capacity, BoundedChannelFullMode mode)
        where TLogModel : ILogModel
        where TDataContext : DbContext
    {
        services.AddSingleton<ILoggingQueue<TLogModel>>(new LoggingQueue<TLogModel>(new BoundedChannelOptions(capacity)
        {
            FullMode = mode,
        }));

        services.AddHostedService<LoggingHostedService<TLogModel, TDataContext>>();

        return services;
    }
}