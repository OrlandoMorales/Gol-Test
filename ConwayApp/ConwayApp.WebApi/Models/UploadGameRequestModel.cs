using ConwayApp.WebApi.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ConwayApp.WebApi.Models
{
    public class UploadGameRequestModel
    {
        [Required]
        public int BoardSizeX { get; set; }

        [Required]
        public int BoardSizeY { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required]
        public List<Cell> LiveCells { get; set; } = new List<Cell>();

    }
}