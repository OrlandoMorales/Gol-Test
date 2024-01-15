using ConwayApp.WebApi.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ConwayApp.WebApi.Domain.Entities
{
    public class Boards : Document
    {
        public int BoardSizeX { get; set; }
        public int BoardSizeY { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<Cell> Cells { get; set; } = new List<Cell>();
        public Boolean IsCompleted { get; set; }
    }
}