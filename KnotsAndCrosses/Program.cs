using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnotsAndCrosses
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to knots and crosses");
            Console.WriteLine("\nPlayer1:X and Player2:O");

            int gameCount = 0;
            int gameInput = 0;
            string[] player = { "X", "O" };

            string[] gameBoard = new string[9];
            string anotherGameInput = string.Empty;

            do
            {
                int turnCount = 1;
                int playerSwitch = 0;
                bool gameComplete = false;
                int winningPlayer = 0;
                bool twoPlayerMode = false;

                for (int i = 0; i < 9; i++)
                {
                    gameBoard[i] = i.ToString();
                }

                string twoPlayerInput = string.Empty;
                do
                {
                    Console.WriteLine($"\nIs this a two player game (y/n)?");
                    twoPlayerInput = Console.ReadLine();
                }
                while (twoPlayerInput != "y" && twoPlayerInput != "n");

                twoPlayerMode = twoPlayerInput == "y";

                Console.WriteLine($"\nStarting game { gameCount + 1 }");

                do
                {
                    bool player2IsUser = playerSwitch == 1 && twoPlayerMode;
                    bool player2IsComputer = playerSwitch == 1 && !twoPlayerMode;

                    if (playerSwitch == 0 || player2IsUser)
                    {
                        bool invalidGameInput = false;
                        bool gamePositionAlreadyTaken = false;

                        do
                        {
                            Console.WriteLine($"\nPlayer { playerSwitch + 1 } take your turn. Enter a position between 0 and 8.");
                            bool isGameInputANumber = int.TryParse(Console.ReadLine(), out gameInput);

                            if (gameInput == 99)
                            {
                                break;
                            }

                            invalidGameInput = !isGameInputANumber || gameInput >= 9;
                            gamePositionAlreadyTaken = gameBoard[gameInput] != gameInput.ToString();

                            if(invalidGameInput || gamePositionAlreadyTaken)
                            {
                                if (invalidGameInput)
                                {
                                    Console.WriteLine("\nIvalid position on the game board.  Position must be between 0 and 8.");
                                }
                                else
                                {
                                    Console.WriteLine($"\nPosition { gameInput } is already taken.");
                                }
                            }
                        }
                        while (invalidGameInput || gameInput == 99 || gamePositionAlreadyTaken);
                    }
                    else
                    {
                        if(CheckNextMoveWinsGame(gameBoard, "X") > -1)
                        {
                            gameInput = CheckNextMoveWinsGame(gameBoard, "X");
                        }
                        else if (CheckNextMoveWinsGame(gameBoard, "O") > -1)
                        {
                            gameInput = CheckNextMoveWinsGame(gameBoard, "O");
                        }
                        else
                        {
                            var availablePositions = gameBoard.ToList()
                                  .Where(x => x != "O" && x != "X")
                                  .ToList();

                            var availablePositionArr = availablePositions.ToArray();

                            Random rnd = new Random();
                            int computerEntryPostion = rnd.Next(0, availablePositions.Count);
                            gameInput = Convert.ToInt32(availablePositionArr[computerEntryPostion]);
                        }

                        Console.WriteLine($"\nPlayer { playerSwitch + 1 } chose position { gameInput }.");
                    }

                    if(gameInput == 99)
                    {
                        break;
                    }

                    gameBoard[gameInput] = player[playerSwitch];

                    if (CheckMoveWinsGame(gameBoard))
                    {
                        gameComplete = true;
                        winningPlayer = playerSwitch + 1;
                        break;
                    }

                    turnCount++;

                    if (turnCount % 2 == 0)
                    {
                        playerSwitch = 1;
                    }
                    else
                    {
                        playerSwitch = 0;
                    }
                } while (gameInput != 99 && !gameComplete && turnCount < 10);

                if (gameInput == 99)
                {
                    Console.WriteLine($"\nPlayer { playerSwitch + 1 } quit the game!");
                }
                else
                {
                    int counter = 0;
                    foreach (string entry in gameBoard)
                    {
                        Console.WriteLine($"Item in { counter } is { entry }");
                        counter++;
                    }

                    if (gameComplete)
                    {
                        Console.WriteLine($"\nCongratulation player { winningPlayer } wins!");
                    }
                    else
                    {
                        Console.WriteLine($"\nCats game, game ended in a draw!");
                    }
                }

                gameCount++;

                do
                {
                    Console.WriteLine("\nWould you like another game (y/n)?");
                    anotherGameInput = Console.ReadLine();
                }
                while (anotherGameInput != "y" && anotherGameInput != "n");

            } while (anotherGameInput == "y" && gameInput != 99);

            Console.WriteLine("\nPress any playerNextMoveWinsGamekey to continue...");
            Console.ReadLine();
        }

        public static int CheckNextMoveWinsGame(string[] gameBoard, string player)
        {
            int nextMove = -1;

            if(gameBoard[0] == gameBoard[1] && gameBoard[2] == "2" && gameBoard[0] == player)
            {
                nextMove = 2;
            }
            else if (gameBoard[1] == gameBoard[2] && gameBoard[0] == "0" && gameBoard[1] == player)
            {
                nextMove = 0;
            }
            else if (gameBoard[0] == gameBoard[2] && gameBoard[1] == "1" && gameBoard[0] == player)
            {
                nextMove = 1;
            }
            else if (gameBoard[3] == gameBoard[4] && gameBoard[5] == "5" && gameBoard[3] == player)
            {
                nextMove = 5;
            }
            else if (gameBoard[3] == gameBoard[5] && gameBoard[4] == "4" && gameBoard[3] == player)
            {
                nextMove = 4;
            }
            else if (gameBoard[4] == gameBoard[5] && gameBoard[3] == "3" && gameBoard[4] == player)
            {
                nextMove = 3;
            }
            else if (gameBoard[6] == gameBoard[7] && gameBoard[8] == "8" && gameBoard[6] == player)
            {
                nextMove = 8;
            }
            else if (gameBoard[6] == gameBoard[8] && gameBoard[7] == "7" && gameBoard[6] == player)
            {
                nextMove = 7;
            }
            else if (gameBoard[7] == gameBoard[8] && gameBoard[6] == "6" && gameBoard[7] == player)
            {
                nextMove = 6;
            }
            else if (gameBoard[0] == gameBoard[3] && gameBoard[6] == "6" && gameBoard[0] == player)
            {
                nextMove = 6;
            }
            else if (gameBoard[0] == gameBoard[6] && gameBoard[3] == "3" && gameBoard[0] == player)
            {
                nextMove = 3;
            }
            else if (gameBoard[3] == gameBoard[6] && gameBoard[0] == "0" && gameBoard[3] == player)
            {
                nextMove = 0;
            }
            else if (gameBoard[1] == gameBoard[4] && gameBoard[7] == "7" && gameBoard[1] == player)
            {
                nextMove = 7;
            }
            else if (gameBoard[1] == gameBoard[7] && gameBoard[4] == "4" && gameBoard[1] == player)
            {
                nextMove = 4;
            }
            else if (gameBoard[4] == gameBoard[7] && gameBoard[1] == "1" && gameBoard[4] == player)
            {
                nextMove = 1;
            }
            else if (gameBoard[2] == gameBoard[5] && gameBoard[8] == "8" && gameBoard[2] == player)
            {
                nextMove = 8;
            }
            else if (gameBoard[5] == gameBoard[8] && gameBoard[2] == "2" && gameBoard[5] == player)
            {
                nextMove = 2;
            }
            else if (gameBoard[2] == gameBoard[8] && gameBoard[5] == "5" && gameBoard[1] == player)
            {
                nextMove = 5;
            }
            else if (gameBoard[0] == gameBoard[4] && gameBoard[8] == "8" && gameBoard[0] == player)
            {
                nextMove = 8;
            }
            else if (gameBoard[4] == gameBoard[8] && gameBoard[0] == "0" && gameBoard[4] == player)
            {
                nextMove = 0;
            }
            else if (gameBoard[0] == gameBoard[8] && gameBoard[4] == "4" && gameBoard[0] == player)
            {
                nextMove = 4;
            }
            else if (gameBoard[2] == gameBoard[4] && gameBoard[6] == "6" && gameBoard[2] == player)
            {
                nextMove = 6;
            }
            else if (gameBoard[2] == gameBoard[6] && gameBoard[4] == "4" && gameBoard[2] == player)
            {
                nextMove = 4;
            }
            else if (gameBoard[4] == gameBoard[6] && gameBoard[2] == "2" && gameBoard[6] == player)
            {
                nextMove = 2;
            }

            return nextMove;
        }

        public static bool CheckMoveWinsGame(string[] gameBoard)
        {
            bool row1Check = gameBoard[0] == gameBoard[1] && gameBoard[1] == gameBoard[2];
            bool row2Check = gameBoard[3] == gameBoard[4] && gameBoard[4] == gameBoard[5];
            bool row3Check = gameBoard[6] == gameBoard[7] && gameBoard[7] == gameBoard[8];

            bool horizontalCheck = row1Check || row2Check || row3Check;

            bool col1Check = gameBoard[0] == gameBoard[3] && gameBoard[3] == gameBoard[6];
            bool col2Check = gameBoard[1] == gameBoard[4] && gameBoard[4] == gameBoard[7];
            bool col3Check = gameBoard[2] == gameBoard[5] && gameBoard[6] == gameBoard[8];

            bool verticalCheck = col1Check || col2Check || col3Check;

            bool diagonal1Check = gameBoard[0] == gameBoard[4] && gameBoard[4] == gameBoard[8];
            bool diagonal2Check = gameBoard[2] == gameBoard[4] && gameBoard[4] == gameBoard[6];

            bool diagonalCheck = diagonal1Check || diagonal2Check;

            return horizontalCheck || verticalCheck || diagonalCheck;
        }

    }
}
