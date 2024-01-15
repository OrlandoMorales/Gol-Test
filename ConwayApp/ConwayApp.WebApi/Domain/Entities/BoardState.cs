using MongoDB.Bson;

namespace ConwayApp.WebApi.Domain.Entities
{
    public class BoardState : Document
    {
        public ObjectId BoardId { get; set; }
        public long State { get; set; }
        public List<Cell> Cells { get; set; } = new List<Cell>();  
    }
}