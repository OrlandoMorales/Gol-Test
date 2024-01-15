using ConwayApp.WebApi.Application.Board;
using ConwayApp.WebApi.Application.Interfaces;
using ConwayApp.WebApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConwayApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardStateController : ControllerBase
    {
        private readonly ILogger<BoardStateController> _logger;


        private readonly IBoardStateService _boardStateService;

        public BoardStateController(ILogger<BoardStateController> logger, IBoardStateService boardStateService)
        {
            _logger = logger;
            _boardStateService = boardStateService;

        }

        [HttpGet]
        [Route("GetAll/{id}")]
        public async Task<IActionResult> GetAllBoardStatus(string id)
        {
            try
            {
                return Ok(_boardStateService.GetAllBoardStatus(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"GET-ALLBOARD-STATUS-ERROR: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }



    }
}
