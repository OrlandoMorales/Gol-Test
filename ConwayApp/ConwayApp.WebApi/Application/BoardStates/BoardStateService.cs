using ConwayApp.WebApi.Application.Interfaces;
using ConwayApp.WebApi.Domain.Entities;
using ConwayApp.WebApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using static NRedisStack.Search.Query;

namespace ConwayApp.WebApi.Application.BoardStates
{
    public class BoardStateService : IBoardStateService
    {
        private readonly INoSqlRepository<Boards> _boardRepository;
        private readonly INoSqlRepository<BoardState> _boardStateRepository;


        public BoardStateService(INoSqlRepository<BoardState> boardStateRepository, INoSqlRepository<Boards> boardRepository)
        {
            _boardStateRepository = boardStateRepository;
            _boardRepository = boardRepository;
        }

        public void Save(Boards nextBoardState, Boards currentBoardState)
        {
            var state = TotalStates(currentBoardState.Id);
            var newBoardState = new BoardState();

            newBoardState.BoardId = currentBoardState.Id;
            newBoardState.Cells = nextBoardState.Cells.Where(w=> w.isAlive == true).ToList();
            newBoardState.State = state;

            _boardStateRepository.InsertOne(newBoardState);
        }

        public long TotalStates(ObjectId boardId)
        {
            var count = _boardStateRepository.CountDocuments(eq=> eq.BoardId == boardId);
            return count == 0 ? 1 : count + 1;
        }

        public BoardState GetCellStateByBoardId(ObjectId Id)
        {
            var currentState = _boardStateRepository.AsQueryable()
                .Where(w => w.BoardId.Equals(Id))
                .OrderByDescending(o => o.State).Take(1).FirstOrDefault();

            return currentState;
        }

        public BoardStatusModel GetAllBoardStatus(string id) 
        {
            var allBoardStatus = new BoardStatusModel();
            allBoardStatus.Board = _boardRepository.FindById(id);
            allBoardStatus.BoardStatus = _boardStateRepository.FilterBy(f => f.BoardId.Equals(new ObjectId(id))).ToList();

            return allBoardStatus;
        }
    }
}
