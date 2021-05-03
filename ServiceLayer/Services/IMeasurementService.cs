using RepoLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IMeasurementService
    {
        Task<IList<Measurement>> GetMeasurementsAsync();
        Task<bool> AddMeasurementAsync(Measurement measurement);
        Task<bool> UpdateMeasurementAsync(Measurement measurement);
        Task<bool> DeleteMeasurementAsync(Guid Id);
        Task<Measurement> GetMeasurementAsync(Guid Id);
    }
}
