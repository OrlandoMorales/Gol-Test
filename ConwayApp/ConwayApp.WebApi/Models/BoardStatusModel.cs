using ConwayApp.WebApi.Domain.Entities;

namespace ConwayApp.WebApi.Models
{
    public class BoardStatusModel
    {
        public Boards Board { get; set; }
        public List<BoardState> BoardStatus { get; set; } = new List<BoardState>();
    }
}
