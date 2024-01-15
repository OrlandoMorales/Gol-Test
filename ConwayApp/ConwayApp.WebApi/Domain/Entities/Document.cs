using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConwayApp.WebApi.Domain.Entities
{
    public abstract class Document : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
