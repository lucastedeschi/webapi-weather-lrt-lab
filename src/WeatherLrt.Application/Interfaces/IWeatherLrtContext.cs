using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherLrt.Domain.Entities;

namespace WeatherLrt.Application.Interfaces
{
    public interface IWeatherLrtContext
    {
        DbSet<SystemUser> SystemUsers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
