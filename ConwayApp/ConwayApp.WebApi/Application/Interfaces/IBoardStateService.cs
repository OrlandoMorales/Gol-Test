using ConwayApp.WebApi.Domain.Entities;
using ConwayApp.WebApi.Models;
using MongoDB.Bson;

namespace ConwayApp.WebApi.Application.Interfaces
{
    public interface IBoardStateService
    {
        void Save(Boards nextBoardState, Boards currentBoardState);
        long TotalStates(ObjectId boardId);
        BoardState GetCellStateByBoardId(ObjectId Id);
        BoardStatusModel GetAllBoardStatus(string id);
    }
}
