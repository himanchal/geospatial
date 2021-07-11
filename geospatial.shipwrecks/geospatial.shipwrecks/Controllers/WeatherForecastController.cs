using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace geospatial.shipwrecks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ShipWreck> Get()
        {
            //create a mongo client
            var mongoClient = new MongoClient("mongodb+srv://live2021:live2021pass@cluster0.uv0z8.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            var database = mongoClient.GetDatabase("sample_geospatial");
            var collection = database.GetCollection<ShipWreck>("shipwrecks");
            try
            {
                return collection.Find(x => x.FeatureType == "Wrecks - Visible").ToList().Take(5);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
    