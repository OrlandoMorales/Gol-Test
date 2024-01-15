using ConwayApp.WebApi.Application.Interfaces;
using ConwayApp.WebApi.Domain.Entities;
using ConwayApp.WebApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ConwayApp.WebApi.Application.Board
{
    public class BoardService : IBoardService
    {
        private readonly INoSqlRepository<Boards> _boardRepository;
        private readonly IBoardStateService _boardStateService;

        public BoardService(INoSqlRepository<Boards> boardRepository, IBoardStateService boardStateService)
        {
            _boardRepository = boardRepository;
            _boardStateService = boardStateService;
        }

        public Boards FindById(string Id)
        {
            return _boardRepository.FindById(Id);
        }

        public Boards Save(UploadGameRequestModel newBoard)
        {
            var board = new Boards();
            board.Description = newBoard.Description;
            board.BoardSizeX = newBoard.BoardSizeX;
            board.BoardSizeY = newBoard.BoardSizeY;
            board.Cells = newBoard.LiveCells;

            _boardRepository.InsertOne(board);

            return board;
        }

        public void Update(Boards board)
        {
            var updateDef = Builders<Boards>.Update
                .Set(up => up.IsCompleted, true);
            _boardRepository.UpdateOne(board, updateDef);
        }
    }
}