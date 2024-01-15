using ConwayApp.WebApi.Application.BoardStates;
using ConwayApp.WebApi.Application.Interfaces;
using ConwayApp.WebApi.Domain.Entities;
using ConwayApp.WebApi.Models;
using MongoDB.Bson;
using NetTopologySuite.Utilities;
using System.Xml.Linq;

namespace ConwayApp.WebApi.Application.Game
{
    public class GameService : IGameService
    {
        private readonly IBoardService _boardService;
        private readonly IBoardStateService _boardStateService;

        private int BoardSizeX { get; set; }
        private int BoardSizeY { get; set; }
        private Boards CurrentBoardState { get; set; } = new Boards();
        private Boards NextBoardState { get; set; } = new Boards();


        public GameService(IBoardService boardService, IBoardStateService boardStateService)
        {
            _boardService = boardService;
            _boardStateService = boardStateService;
        }


        public void SetUp(Boards boards)
        {
            BoardSizeX = boards.BoardSizeX;
            BoardSizeY = boards.BoardSizeY;
        }


        public Boards InitBoard(Boards board)
        {
            SetUp(board);

            var currentCellStatus = _boardStateService.GetCellStateByBoardId(board.Id);

            if (currentCellStatus != null && currentCellStatus.State > 0)
            {
                board.Cells.Clear();
                board.Cells = currentCellStatus.Cells;
            }

            for (int i = 0; i < BoardSizeX; i++)
            {
                for (int j = 0; j < BoardSizeY; j++)
                {
                    var cells = new Cell();

                    cells.X = i;
                    cells.Y = j;
                    cells.isAlive = board.Cells.Any(w => w.X.Equals(cells.X) && w.Y.Equals(cells.Y));

                    CurrentBoardState.Cells.Add(cells);
                }
            }

            CurrentBoardState.Id = board.Id;
            CurrentBoardState.BoardSizeX = BoardSizeX;
            CurrentBoardState.BoardSizeY = BoardSizeY;
            CurrentBoardState.Description = board.Description;
            CurrentBoardState.IsCompleted = board.IsCompleted;

            // Process new state 
            ProcessBoardNextState(CurrentBoardState);

            return CurrentBoardState;

        }


        public void ProcessBoardNextState(Boards board)
        {
            NewSpawnNextGeneration();
        }
        

        private int ScanCellsNeighbours(int cellX, int cellY)
        {
            // Calculate live neighours
            int liveNeighbours = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    // Out of bounds
                    if (cellX + i < 0 || cellX + i >= BoardSizeX)   
                        continue;
                    // Out of bounds
                    if (cellY + j < 0 || cellY + j >= BoardSizeY)   
                        continue;
                    // Same Cell
                    if (cellX + i == cellX && cellY + j == cellY)       
                        continue;

                    liveNeighbours += Convert.ToInt16(CurrentBoardState.Cells
                        .Where(w => w.X.Equals(cellX + i) && w.Y.Equals(cellY + j))
                        .First().isAlive);
                }
            }

            return liveNeighbours;
        }


        public Boards NewSpawnNextGeneration()
        {
            var board = new Boards();

            for (int x = 0; x < BoardSizeX; x++)
            {
                for (int y = 0; y < BoardSizeY; y++)
                {
                    int liveNeighbours = ScanCellsNeighbours(x, y);

                    if (GetCellStatus(x, y) == true && liveNeighbours < 2)
                        NextBoardState.Cells.Add(CreateCell(x, y, false));


                    else if (GetCellStatus(x, y) == true && liveNeighbours > 3)
                        NextBoardState.Cells.Add(CreateCell(x, y, false));


                    else if (GetCellStatus(x, y) == false && liveNeighbours == 3)
                        NextBoardState.Cells.Add(CreateCell(x, y, true));

                    else
                        NextBoardState.Cells.Add(GetCurrentCell(x,y));
                }
            }

            if (IsBoardCompleted(NextBoardState,CurrentBoardState.Id))
            {
                board.Id = CurrentBoardState.Id;
                _boardService.Update(board);
            }
            else 
            {
                _boardStateService.Save(NextBoardState, CurrentBoardState);
            }

            return board;
        }


        private bool GetCellStatus(int x, int y) 
        {
            var status = CurrentBoardState.Cells.Where(w => w.X.Equals(x) && w.Y.Equals(y)).First();
            return status.isAlive;
        }


        private Cell GetCurrentCell(int x, int y)
        {
            var cell = CurrentBoardState.Cells.Where(w => w.X.Equals(x) && w.Y.Equals(y)).First();
            return cell;
        }


        private Cell CreateCell(int x, int y, bool isAlive) 
        {
            var newCell = new Cell();
            newCell.X = x; 
            newCell.Y = y;
            newCell.isAlive = isAlive;

            return newCell;
        }


        private bool IsBoardCompleted(Boards next, ObjectId boardId) 
        {
            var isCompleted = false;
            var lastCellStatus = _boardStateService.GetCellStateByBoardId(boardId);
            if (lastCellStatus != null) 
            {
                // is the current board status is equal to the next one then the board is completed
                var cl = next.Cells.Where(w => w.isAlive == true).ToList();
                isCompleted = lastCellStatus.Cells.Select(s => new { s.X, s.Y }).ToHashSet().SetEquals(cl.Select(s => new { s.X, s.Y }));
            }

            return isCompleted;
        }
    }
}