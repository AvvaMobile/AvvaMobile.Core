using AvvaMobile.Core.Business;
using AvvaMobile.Core.Caching;
using AvvaMobile.Core.CustomDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvvaMobile.Core.Services
{
    public interface IAppInitializerService
    {
        Task<ServiceResult> LoadAllSettingsToCache();
        Task<ServiceResult> SeedDefaultData();
    }
    public class AppInitializerService : IAppInitializerService
    {
        private readonly ICacheManager _cacheManager;
        private readonly ApplicationDbContext _context;
        public AppInitializerService(ICacheManager cacheManager, ApplicationDbContext context)
        {
            _cacheManager = cacheManager;
            _context = context;
        }

        public async Task<ServiceResult> LoadAllSettingsToCache()
        {
            var result = new ServiceResult();

            var appSettings = await(from ap in _context.AppSettings
                                    orderby ap.Order
                                    select ap).ToListAsync();

            foreach (var item in appSettings)
            {
                _cacheManager.SetNeverRemove(item.Key, item.Value);
            }

            return result;
        }

        public Task<ServiceResult> SeedDefaultData()
        {
            throw new NotImplementedException();
        }
    }
}
