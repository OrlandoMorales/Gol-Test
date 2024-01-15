using ConwayApp.WebApi.Application.Interfaces;

namespace ConwayApp.WebApi.Application.Core.Configuration
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}