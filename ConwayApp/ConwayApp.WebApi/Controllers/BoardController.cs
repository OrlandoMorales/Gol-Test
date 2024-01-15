using ConwayApp.WebApi.Application.Interfaces;
using ConwayApp.WebApi.Domain.Entities;
using ConwayApp.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConwayApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly ILogger<BoardController> _logger;

        private readonly IGameService _gameService;
        private readonly INoSqlRepository<Boards> _boardRepository;
        private readonly IBoardService _boardService;


        public BoardController(ILogger<BoardController> logger, IGameService gameService, INoSqlRepository<Boards> boardRepository, IBoardService boardService)
        {
            _logger = logger;
            _gameService = gameService;
            _boardRepository = boardRepository;
            _boardService = boardService;
        }


        [HttpGet]
        [Route("Find/{id}")]
        public async Task<IActionResult> FindBoard(string id)
        {
            try 
            {
                return Ok(_boardService.FindById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"FINDBOARD-ERROR: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}