using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Jmwoo.Common.Http;

namespace Assistant.Features.Surf
{
    public interface ISurfService
    {
        Task<List<SurfMoment>> GetSurfForecast(SurfSpot surfSpot);
        Task<SurfMoment> GetCurrentSurf(SurfSpot surfSpot);
        Task<string> GetCurrentSurfMessage(SurfSpot surfSpot);
        Task<string> GetCurrentOceanTemperatureMessage(SurfCounty surfCounty);

        Task<WaterTemperatureResult> GetCurrentWaterTemperature(SurfCounty surfCounty);
    }
    public class SurfService : ISurfService
    {
        readonly IHttpClientFactory _httpClientFactory;
        readonly ISurfForecastCache _surfForecastCache;
        readonly IWaterTemperatureCache _waterTemperatureCache;

        public SurfService(
          IHttpClientFactory httpClientFactory, ISurfForecastCache surfForecastCache, IWaterTemperatureCache waterTemperatureCache)
        {
            _waterTemperatureCache = waterTemperatureCache;
            _httpClientFactory = httpClientFactory;
            _surfForecastCache = surfForecastCache;
        }

        public async Task<List<SurfMoment>> GetSurfForecast(SurfSpot surfSpot)
        {
            var key = surfSpot.ToString();
            var result = await _surfForecastCache.Get(key);
            if (result == null)
            {
                result = await this.GetSurfForecastFromApi(surfSpot);
                await _surfForecastCache.Set(result, key);
            }
            return result;
        }

        public async Task<List<SurfMoment>> GetSurfForecastFromApi(SurfSpot surfSpot)
        {
            var url = HttpHelpers.BuildUrl("www-2019-2133843493.us-east-1.elb.amazonaws.com", $"/api/spot/forecast/{surfSpot.Value}", useHttps: false);
            var result = await _httpClientFactory.CreateClient().SendAndReceiveAs<List<SurfMoment>>(HttpMethod.Get, url);
            return result;
        }

        public async Task<string> GetCurrentSurfMessage(SurfSpot surfSpot)
        {
            var currentSurf = await this.GetCurrentSurf(surfSpot);
            var shape = currentSurf.shape_full.Replace("-", " to ");
            return $"The surf at {currentSurf.spot_name} is rated {shape} with a height of {Math.Round(currentSurf.size_ft, 1)} feet. ";
        }

        public async Task<SurfMoment> GetCurrentSurf(SurfSpot surfSpot)
        {
            var surfForecast = await this.GetSurfForecast(surfSpot);
            var utcNow = DateTime.UtcNow;

            var gmt = $"{utcNow.Year}-{utcNow.Month}-{utcNow.Day} {utcNow.Hour}";

            return surfForecast.FirstOrDefault(s => s.gmt == gmt);
        }

        public async Task<WaterTemperatureResult> GetCurrentWaterTemperature(SurfCounty surfCounty)
        {
            var result = await _waterTemperatureCache.Get(surfCounty.Value);

            if (result == null)
            {
                result = await this.GetWaterTempFromApi(surfCounty);
                await _waterTemperatureCache.Set(result, surfCounty.Value);
            }

            return result;
        }

        public async Task<WaterTemperatureResult> GetWaterTempFromApi(SurfCounty surfCounty)
        {
            var url = HttpHelpers.BuildUrl("www-2019-2133843493.us-east-1.elb.amazonaws.com", $"/api/county/water-temperature/{surfCounty.Value}", useHttps: false);
            return await _httpClientFactory.CreateClient().SendAndReceiveAs<WaterTemperatureResult>(HttpMethod.Get, url);
        }

        public async Task<string> GetCurrentOceanTemperatureMessage(SurfCounty surfCounty)
        {
            var waterTemp = await this.GetCurrentWaterTemperature(surfCounty);
            return $"The ocean temperature is {waterTemp.fahrenheit} degrees. ";
        }
    }
}