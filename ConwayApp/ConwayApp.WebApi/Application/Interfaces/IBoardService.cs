using ConwayApp.WebApi.Domain.Entities;
using ConwayApp.WebApi.Models;

namespace ConwayApp.WebApi.Application.Interfaces
{
    public interface IBoardService 
    {
        void Update(Boards board);

        Boards Save(UploadGameRequestModel board);

        Boards FindById(string Id);
    }
}
