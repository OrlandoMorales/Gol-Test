using ConwayApp.WebApi.Application.Game;
using ConwayApp.WebApi.Application.Interfaces;
using ConwayApp.WebApi.Domain.Entities;
using ConwayApp.WebApi.Infrastructure.Repositories;
using ConwayApp.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Newtonsoft.Json;
using System;

namespace ConwayApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;

        private readonly IGameService _gameService;
        private readonly IBoardService _boardService;


        public GameController(ILogger<GameController> logger, IGameService gameService, IBoardService boardService)
        {
            _logger = logger;
            _gameService = gameService;
            _boardService = boardService;
        }


        [Route("UploadBoard")]
        [HttpPost]
        public async Task<IActionResult> UploadBoard(UploadGameRequestModel uploadGameRequestModel)
        {
            try
            {
                var response = _boardService.Save(uploadGameRequestModel);
                return Ok($"Board Created with ID: {response.Id.ToString()}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"UPLOADBOARD-ERROR: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }


        [Route("NextBoardState/{boardId}")]
        [HttpPost]
        public async Task<IActionResult> NextBoardState(string boardId)
        {
            try
            {
                var uploadedBoard = _boardService.FindById(boardId);

                if (!uploadedBoard.IsCompleted)
                {
                    _gameService.InitBoard(uploadedBoard);
                }
                else
                {
                    return Ok($"Board-ID: {uploadedBoard.Id} is Completed, please check the status");
                }

                return Ok($"A board state was generated for BoardID: {uploadedBoard.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"NEXTBOARDSTATE-ERROR: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }


    }
}