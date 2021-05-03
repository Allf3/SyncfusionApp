using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly ApiDBContext _context;

        public MeasurementService(ApiDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a true/false if there was any changes to the DB.
        /// </summary>
        /// <returns>True / false</returns>
        private async Task<bool> SaveDbAsync()
        {
            if (await _context.SaveChangesAsync() < 0)
                return true;
            else
                return false;
        }

        public async Task<bool> AddMeasurementAsync(Measurement measurement)
        {
            _context.Measurements.Add(measurement);

            return await SaveDbAsync();
        }

        public async Task<bool> DeleteMeasurementAsync(Guid Id)
        {
            Measurement measurement =  _context.Measurements.Where(m => m.ID == Id).FirstOrDefault();
            _context.Measurements.Remove(measurement);

            return await SaveDbAsync();
        }

        public async Task<Measurement> GetMeasurementAsync(Guid Id)
        {
            return await _context.Measurements.Where(m => m.ID == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Measurement>> GetMeasurementsAsync()
        {
            return await _context.Measurements.ToListAsync<Measurement>();
        }

        public async Task<bool> UpdateMeasurementAsync(Measurement measurement)
        {
            _context.Measurements.Update(measurement);

            return await SaveDbAsync();
        }

        public async Task<bool> MeasurementExists(Guid id)
        {
            return await _context.Measurements.AnyAsync(m => m.ID == id);
        }
    }
}
