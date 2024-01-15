namespace ConwayApp.WebApi.Application.Interfaces
{
    public interface IMongoDBSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}