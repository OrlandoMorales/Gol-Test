using ConwayApp.WebApi.Domain.Entities;
using ConwayApp.WebApi.Models;

namespace ConwayApp.WebApi.Application.Interfaces
{
    public interface IGameService
    {
        Boards InitBoard(Boards board);
    }
}
