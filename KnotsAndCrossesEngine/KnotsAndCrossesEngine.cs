using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace KnotsAndCrossesEngine
{
    public enum WinningMove
    {
        Row1,
        Row2,
        Row3,
        Col1,
        Col2,
        Col3,
        Diag1,
        Diag2
    }

    [Serializable]
    public class KnotsAndCrossesEngine
    {
        public int TurnCount { get; set; }
        public int PlayerSwitch { get; set; }
        public int NextPlayerSwitch { get; set; } 
        public bool GameComplete { get; set; }
        public int WinningPlayer { get; set; }
        public static bool TwoPlayerMode { get; set; } = false;
        public string[] GameBoard { get; set; }
        public static string[] Player { get; set; } = { "X", "O" };
        public string GameStatusMessage { get; set; }
        public bool CurrentPlayerIsUser { get; set; }
        public List<string> WinningMoves = new List<string>();
        public string PlayerTurnMessage { get; set; }
        public Dictionary<string, int> GameBoardPositions = new Dictionary<string, int>()
        {
            { "0,0", 0 },
            { "0,1", 1 },
            { "0,2", 2 },
            { "1,0", 3 },
            { "1,1", 4 },
            { "1,2", 5 },
            { "2,0", 6 },
            { "2,1", 7 },
            { "2,2", 8 },
        };
        private string PlayerTurnMessages = "Good move, Ooh controversial, Wow didn't see that one coming, Nice one, Wow this is tense, Where did you pull that one out of, Your on-fire";
        private int ComputerMoveTimeInMilliseconds = 500;

        public KnotsAndCrossesEngine()
        {
            TurnCount = 1;
            PlayerSwitch = 0;
            NextPlayerSwitch = 0;
            GameComplete = false;
            WinningPlayer = 0;
            GameBoard = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8" };
            GameStatusMessage = "NEW GAME.  Please take your turn";
            CurrentPlayerIsUser = true;
            WinningMoves.Clear();
        }


        public string PlayerMove(int rowIndex, int colIndex)
        {
            return PlayerMove(GameBoardPositions[$"{rowIndex},{colIndex}"]);
        }

        public string PlayerMove(int gameInput)
        {
            bool gamePositionAlreadyTaken = GameBoard[gameInput] != gameInput.ToString();
            bool invalidGameInput = gameInput >= 9;

            if (invalidGameInput || gamePositionAlreadyTaken)
            {
                if (invalidGameInput)
                {
                    return "Invalid position on the game board.Position must be between 0 and 8.";
                }
                else
                {
                    return $"\nPosition { gameInput } is already taken.";
                }
            }

            PlayerSwitch = NextPlayerSwitch;
            string knotOrCross = Player[PlayerSwitch];
            GameBoard[gameInput] = Player[PlayerSwitch];

            SetNextPlayer(PlayerSwitch);

            return knotOrCross;
        }


        public MoveWinsGameCheck MoveWinsGame()
        {
            var moveWinsGameCheck = CheckMoveWinsGame();
            moveWinsGameCheck = GameIsADraw(moveWinsGameCheck);

            return moveWinsGameCheck;
        }

        private MoveWinsGameCheck CheckMoveWinsGame()
        {
            WinningMove winningMovePos = WinningMove.Row1;

            bool horizontalCheck = false;

            if (GameBoard[0] == GameBoard[1] && GameBoard[1] == GameBoard[2])
            {
                winningMovePos = WinningMove.Row1;
                horizontalCheck = true;
                WinningMoves = new List<string>()
                {
                    "0,0",
                    "0,1",
                    "0,2"
                };
            }
            else if (GameBoard[3] == GameBoard[4] && GameBoard[4] == GameBoard[5])
            {
                winningMovePos = WinningMove.Row2;
                horizontalCheck = true;
                WinningMoves = new List<string>()
                {
                    "1,0",
                    "1,1",
                    "1,2"
                };
            }
            else if (GameBoard[6] == GameBoard[7] && GameBoard[7] == GameBoard[8])
            {
                winningMovePos = WinningMove.Row3;
                horizontalCheck = true;
                WinningMoves = new List<string>()
                {
                    "2,0",
                    "2,1",
                    "2,2"
                };
            }

            bool verticalCheck = false;

            if (GameBoard[0] == GameBoard[3] && GameBoard[3] == GameBoard[6])
            {
                winningMovePos = WinningMove.Col1;
                verticalCheck = true;
                WinningMoves = new List<string>()
                {
                    "0,0",
                    "1,0",
                    "2,0"
                };
            }
            else if (GameBoard[1] == GameBoard[4] && GameBoard[4] == GameBoard[7])
            {
                winningMovePos = WinningMove.Col2;
                verticalCheck = true;
                WinningMoves = new List<string>()
                {
                    "0,1",
                    "1,1",
                    "2,1"
                };
            }
            else if (GameBoard[2] == GameBoard[5] && GameBoard[5] == GameBoard[8])
            {
                winningMovePos = WinningMove.Col3;
                verticalCheck = true;
                WinningMoves = new List<string>()
                {
                    "0,2",
                    "1,2",
                    "2,2"
                };
            }

            bool diagonalCheck = false;

            if (GameBoard[0] == GameBoard[4] && GameBoard[4] == GameBoard[8])
            {
                winningMovePos = WinningMove.Diag1;
                diagonalCheck = true;
                WinningMoves = new List<string>()
                {
                    "0,0",
                    "1,1",
                    "2,2"
                };
            }
            else if (GameBoard[2] == GameBoard[4] && GameBoard[4] == GameBoard[6])
            {
                winningMovePos = WinningMove.Diag2;
                diagonalCheck = true;
                WinningMoves = new List<string>()
                {
                    "0,2",
                    "1,1",
                    "2,0"
                };
            }

            string winningGameMessage = string.Empty;
            bool moveWinsGame = false;

            if(horizontalCheck || verticalCheck || diagonalCheck)
            {
                moveWinsGame = true;
                WinningPlayer = PlayerSwitch + 1;
                winningGameMessage = $"Congratulations!!  Player { WinningPlayer } wins!";
                GameStatusMessage = CurrentPlayerIsUser ? "Congratulations you win" : "Unlucky the computer wins";
            }

            var moveWinsGameCheck = new MoveWinsGameCheck()
            {
                MoveWinsGame = moveWinsGame,
                WinningMovePos = winningMovePos,
                WinningGameMessage = winningGameMessage,
                GameComplete = moveWinsGame,
            };

            return moveWinsGameCheck;
        }

        private MoveWinsGameCheck GameIsADraw(MoveWinsGameCheck moveWinsGameCheck)
        {
            if (GameBoard.All(x => KnotsAndCrossesEngine.Player.Any(y => x == y)))
            {
                moveWinsGameCheck.GameIsADraw = true;
                moveWinsGameCheck.GameComplete = true;
                GameStatusMessage = "Cats game, game ends in a draw!";
            }
            else
            {
                moveWinsGameCheck.GameIsADraw = false;
            }

            return moveWinsGameCheck;
        }

        public int CheckNextMoveWinsGame(string player)
        {
            int nextMove = -1;

            if (GameBoard[0] == GameBoard[1] && GameBoard[2] == "2" && GameBoard[0] == player)
            {
                nextMove = 2;
            }
            else if (GameBoard[1] == GameBoard[2] && GameBoard[0] == "0" && GameBoard[1] == player)
            {
                nextMove = 0;
            }
            else if (GameBoard[0] == GameBoard[2] && GameBoard[1] == "1" && GameBoard[0] == player)
            {
                nextMove = 1;
            }
            else if (GameBoard[3] == GameBoard[4] && GameBoard[5] == "5" && GameBoard[3] == player)
            {
                nextMove = 5;
            }
            else if (GameBoard[3] == GameBoard[5] && GameBoard[4] == "4" && GameBoard[3] == player)
            {
                nextMove = 4;
            }
            else if (GameBoard[4] == GameBoard[5] && GameBoard[3] == "3" && GameBoard[4] == player)
            {
                nextMove = 3;
            }
            else if (GameBoard[6] == GameBoard[7] && GameBoard[8] == "8" && GameBoard[6] == player)
            {
                nextMove = 8;
            }
            else if (GameBoard[6] == GameBoard[8] && GameBoard[7] == "7" && GameBoard[6] == player)
            {
                nextMove = 7;
            }
            else if (GameBoard[7] == GameBoard[8] && GameBoard[6] == "6" && GameBoard[7] == player)
            {
                nextMove = 6;
            }
            else if (GameBoard[0] == GameBoard[3] && GameBoard[6] == "6" && GameBoard[0] == player)
            {
                nextMove = 6;
            }
            else if (GameBoard[0] == GameBoard[6] && GameBoard[3] == "3" && GameBoard[0] == player)
            {
                nextMove = 3;
            }
            else if (GameBoard[3] == GameBoard[6] && GameBoard[0] == "0" && GameBoard[3] == player)
            {
                nextMove = 0;
            }
            else if (GameBoard[1] == GameBoard[4] && GameBoard[7] == "7" && GameBoard[1] == player)
            {
                nextMove = 7;
            }
            else if (GameBoard[1] == GameBoard[7] && GameBoard[4] == "4" && GameBoard[1] == player)
            {
                nextMove = 4;
            }
            else if (GameBoard[4] == GameBoard[7] && GameBoard[1] == "1" && GameBoard[4] == player)
            {
                nextMove = 1;
            }
            else if (GameBoard[2] == GameBoard[5] && GameBoard[8] == "8" && GameBoard[2] == player)
            {
                nextMove = 8;
            }
            else if (GameBoard[5] == GameBoard[8] && GameBoard[2] == "2" && GameBoard[5] == player)
            {
                nextMove = 2;
            }
            else if (GameBoard[2] == GameBoard[8] && GameBoard[5] == "5" && GameBoard[1] == player)
            {
                nextMove = 5;
            }
            else if (GameBoard[0] == GameBoard[4] && GameBoard[8] == "8" && GameBoard[0] == player)
            {
                nextMove = 8;
            }
            else if (GameBoard[4] == GameBoard[8] && GameBoard[0] == "0" && GameBoard[4] == player)
            {
                nextMove = 0;
            }
            else if (GameBoard[0] == GameBoard[8] && GameBoard[4] == "4" && GameBoard[0] == player)
            {
                nextMove = 4;
            }
            else if (GameBoard[2] == GameBoard[4] && GameBoard[6] == "6" && GameBoard[2] == player)
            {
                nextMove = 6;
            }
            else if (GameBoard[2] == GameBoard[6] && GameBoard[4] == "4" && GameBoard[2] == player)
            {
                nextMove = 4;
            }
            else if (GameBoard[4] == GameBoard[6] && GameBoard[2] == "2" && GameBoard[6] == player)
            {
                nextMove = 2;
            }

            return nextMove;
        }

        public CompuerUserMove ComputerUserNextMove()
        {
            int computerNextMove = 0;

            PlayerSwitch = NextPlayerSwitch;
            SetNextPlayer(PlayerSwitch);

            int moveFromOWinsGame = CheckNextMoveWinsGame("O");
            int moveFromXWinsGame = CheckNextMoveWinsGame("X");

            if (moveFromOWinsGame > -1)
            {
                computerNextMove = moveFromOWinsGame;
            }
            else if (moveFromXWinsGame > -1)
            {
                computerNextMove = moveFromXWinsGame;
            }
            else
            {
                var availablePositions = GameBoard.ToList()
                        .Where(x => x != "O" && x != "X")
                        .ToList();

                var availablePositionArr = availablePositions.ToArray();

                Random rnd = new Random();
                int computerEntryPostion = rnd.Next(0, availablePositions.Count);
                computerNextMove = Convert.ToInt32(availablePositionArr[computerEntryPostion]);
            }

            GameBoard[computerNextMove] = Player[PlayerSwitch];

            var computerUserNextMove = new CompuerUserMove()
            {
                ComputerUserMovePos = computerNextMove,
                ComputerUserMove = Player[PlayerSwitch],
            };

            return computerUserNextMove;
        }

        public void SetNextPlayer(int currentPlayer)
        {
            NextPlayerSwitch = currentPlayer == 0 ? 1 : 0;
        }

        public string GetKnotOrCross(int rowIndex, int colIndex)
        {
            string knotOrCross = GameBoard[GameBoardPositions[$"{rowIndex},{colIndex}"]];
            return int.TryParse(knotOrCross, out int knotOrCrossPosition) ? string.Empty : knotOrCross;
        }

        public async Task MakePlayerMove(int rowIndex, int colIndex)
        {
            PlayerMove(rowIndex, colIndex);

            var moveWinsGameCheck = MoveWinsGame();

            if (!moveWinsGameCheck.GameComplete)
            {
                CurrentPlayerIsUser = false;
                GameStatusMessage = $"{ NextPlayerTurnMessage() }!  Computer takes it's turn...";
                await ComputerMove();
            }
        }

        private string NextPlayerTurnMessage()
        {
            var rnd = new Random();

            var playerTurnMessages = PlayerTurnMessages.Split(',');

            string currentPlayerTurnMessage = playerTurnMessages[rnd.Next(0, playerTurnMessages.Length)];

            while (currentPlayerTurnMessage == PlayerTurnMessage)
            {
                currentPlayerTurnMessage = playerTurnMessages[rnd.Next(0, playerTurnMessages.Length)];
            }

            PlayerTurnMessage = currentPlayerTurnMessage;

            return currentPlayerTurnMessage;
        }

        private async Task ComputerMove()
        {
            await Task.Delay(ComputerMoveTimeInMilliseconds);

            var computerUserNextMove = ComputerUserNextMove();

            GameBoard[computerUserNextMove.ComputerUserMovePos] = computerUserNextMove.ComputerUserMove;

            var computerWinsGameCheck = MoveWinsGame();

            if (!computerWinsGameCheck.GameComplete)
            {
                CurrentPlayerIsUser = true;
                GameStatusMessage = "Please take your turn";
            }
        }
    }

    public class CompuerUserMove
    {
        public int ComputerUserMovePos { get; set; }
        public string ComputerUserMove { get; set; }
    }

    public class MoveWinsGameCheck
    {
        public bool MoveWinsGame { get; set; }
        public WinningMove WinningMovePos { get; set; }
        public string WinningGameMessage { get; set; }
        public bool GameComplete { get; set; }
        public bool GameIsADraw { get; set; }
    }
}
