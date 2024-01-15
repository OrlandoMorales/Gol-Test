using ConwayApp.WebApi.Domain.Entities;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConwayApp.WebApi.Models
{
    public class UploadGameResponseModel
    {
        public ObjectId Id { get; set; }
        public int BoardSizeX { get; set; }
        public int BoardSizeY { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<Cell> Cells { get; set; } = new List<Cell>();
        public Boolean IsCompleted { get; set; }
    }
}