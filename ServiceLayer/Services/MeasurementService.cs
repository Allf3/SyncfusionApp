using RepoLayer.Constants;
using RepoLayer.Models;
using RepoLayer.Repositories.Measurements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyIoC;

namespace ServiceLayer.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IGenericRepository _repo;

        public MeasurementService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public async Task<IList<Measurement>> GetMeasurementsAsync()
        {
            UriBuilder builder = new UriBuilder(APIConstants.BaseApiUrl)
            {
                Path = APIConstants.MeasurementEndPoint
            };

            string urlkey = builder.ToString();

            Root ThingSpeak = await _repo.GetAsync<Root>(urlkey);

            List<Measurement> measurements = new List<Measurement>();

            foreach (var item in ThingSpeak.feeds)
            {
                if (item.entry_id > 0)
                {
                    measurements.Add(new Measurement
                    {
                        Date = item.created_at,
                        ID = item.entry_id,
                        Humidity = item.field2,
                        Temperatur = item.field1
                    });
                }

            }

            return measurements;
        }

        public async Task<Measurement> GetMeasurementAsync(Guid Id)
        {
            UriBuilder builder = new UriBuilder(APIConstants.BaseApiUrl)
            {
                Path = $"{APIConstants.MeasurementEndPoint}/{Id.ToString()}"
            };
            return await _repo.GetAsync<Measurement>(builder.ToString());
        }

        public async Task<bool> AddMeasurementAsync(Measurement measurement)
        {
            UriBuilder builder = new UriBuilder(APIConstants.BaseApiUrl)
            {
                Path = APIConstants.MeasurementEndPoint
            };
            await _repo.PostAsync(builder.ToString(), measurement);
            return true;
        }

        public async Task<bool> UpdateMeasurementAsync(Measurement measurement)
        {
            UriBuilder builder = new UriBuilder(APIConstants.BaseApiUrl)
            {
                Path = $"{APIConstants.MeasurementEndPoint}/{measurement.ID.ToString()}"
            };
            await _repo.PutAsync(builder.ToString(), measurement);
            return true;
        }


        public async Task<bool> DeleteMeasurementAsync(Guid Id)
        {
            UriBuilder builder = new UriBuilder(APIConstants.BaseApiUrl)
            {
                Path = $"{APIConstants.MeasurementEndPoint}/{Id.ToString()}"
            };
            await _repo.DeleteAsync(builder.ToString());
            return true;
        }
    }
}
