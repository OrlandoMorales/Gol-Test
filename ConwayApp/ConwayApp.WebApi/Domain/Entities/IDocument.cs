using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ConwayApp.WebApi.Domain.Entities
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        ObjectId Id { get; set; }

        DateTime CreatedAt { get; }
    }
}
