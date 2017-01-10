using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class OtheloUI
    {
        private static GameEngine m_GameEngine;
        public OtheloUI()
        {
            m_GameEngine = new GameEngine();
        }
        public void OtheloUIMenu()
        {
            int boardSize = 8;
            int menuSelection = 9;
            Ex02.ConsoleUtils.Screen.Clear();
            do
            {

                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine("{0}{0}{0}Board size is: {1}{0}{0}{0}", Environment.NewLine, boardSize);
                Console.WriteLine("(1) play vs human.{0}(2) play vs pc.{0}(3) Change board size.{0}(0) Exit.{0}{0}{0}Please Choose :/> ", Environment.NewLine);
                try
                {
                    menuSelection = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Input ERROR. press any key...");
                    menuSelection = 9;
                    throw;
                }

                if (menuSelection == 3)
                {
                    bool isBoardSizeValid = false;

                    Ex02.ConsoleUtils.Screen.Clear();
                    while (!isBoardSizeValid)
                    {
                        
                        Console.WriteLine("Please Choose a new board size :/> ");
                        isBoardSizeValid = int.TryParse(Console.ReadLine(), out boardSize);
                        if(!isBoardSizeValid)
                        {
                            Console.WriteLine("Error! Board size should be a number!, press any key to try again...");
                            Console.ReadLine();
                            Ex02.ConsoleUtils.Screen.Clear();
                        }
                        else if(boardSize < 3 || boardSize > 20)
                        {
                            
                            Console.WriteLine("Error! Board size should be between 4 to 20, press any key to try again...");
                            Console.ReadLine();
                            Ex02.ConsoleUtils.Screen.Clear();
                            isBoardSizeValid = false;
                            boardSize = 0;
                        }
                    }
                    Ex02.ConsoleUtils.Screen.Clear();
                }
                if (menuSelection > 3 || menuSelection < 0)
                {
                    Console.WriteLine("Can't do that.... please enter valid option. press any key to continue...");
                    Console.Beep();
                    Console.ReadLine();

                }

            } while (menuSelection > 2 || menuSelection < 0);
            Ex02.ConsoleUtils.Screen.Clear();
            switch (menuSelection)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    StartPlay(boardSize, menuSelection);//vs human
                    break;
                case 2:
                    StartPlay(boardSize, menuSelection);//vs pc
                    break;
                default:
                    Environment.Exit(1);
                    break;
            }

        }
        private void StartPlay(int i_boardSize, int i_menuSelection)
        {
            string userInput = "";
            
            if (i_menuSelection == 1)
            {
                PlayerVsPlayer();
            }
            else if (i_menuSelection == 2)
            {
                PlayerVsComputer();
            }
            bool keepPlaying = true;
            do
            {
                m_GameEngine.CreateBoard(i_boardSize);
                bool gameOver = false;
                bool isPlayerOne = true;
                bool humanMoveResponse = true;
                Ex02.ConsoleUtils.Screen.Clear();
                BoardPrint();
                do
                {
                    int numOfValidMovesForPlayer1 = m_GameEngine.AvalibleMoves(m_GameEngine.Board, Piece.Black, Piece.White).Length;
                    int numOfValidMovesForPlayer2 = m_GameEngine.AvalibleMoves(m_GameEngine.Board, Piece.White, Piece.Black).Length;
                    do
                    {
                        if (!humanMoveResponse)
                        {
                            Ex02.ConsoleUtils.Screen.Clear();
                            Console.WriteLine("************ {0} is Not a Valid move! try again... ************\n", userInput);
                            BoardPrint();
                        }
                        if (isPlayerOne)
                        {
                            if (m_GameEngine.AvalibleMoves(m_GameEngine.Board, Piece.Black, Piece.White).Length != 0)
                            {
                                Console.WriteLine("{0} -  your symbol is  {1} - Make your move : row,col : ", m_GameEngine.Player1.PlayerName, m_GameEngine.Player1.Symbol);
                                userInput = Console.ReadLine();
                                humanMoveResponse = CheckAndSubmitUserInput(userInput, isPlayerOne);
                            }
                            else
                            {
                                Console.WriteLine("{0} - No More Options for you, turn goes to {1}, press any key to continue", m_GameEngine.Player1.PlayerName, m_GameEngine.Player2.PlayerName);
                                Console.ReadLine();
                                humanMoveResponse = true;
                            }
                        }
                        else
                        {
                            if (i_menuSelection == 1)
                            {
                                if (m_GameEngine.AvalibleMoves(m_GameEngine.Board, Piece.White, Piece.Black).Length != 0)
                                {
                                    Console.WriteLine("{0} -  your symbol is  {1} - Make your move : row,col : ", m_GameEngine.Player2.PlayerName, m_GameEngine.Player2.Symbol);
                                    userInput = Console.ReadLine();
                                    humanMoveResponse = CheckAndSubmitUserInput(userInput, isPlayerOne);
                                }
                                else
                                {
                                    Console.WriteLine("{0} - No More Options for you, turn goes to {1}, press any key to continue", m_GameEngine.Player2.PlayerName, m_GameEngine.Player1.PlayerName);
                                    Console.ReadLine();
                                    humanMoveResponse = true;
                                }
                            }
                            else if (i_menuSelection == 2)
                            {
                                m_GameEngine.MakePcMove(m_GameEngine.Board, Piece.White, Piece.Black);
                            }
                        }
                    } while (!humanMoveResponse);
                    Ex02.ConsoleUtils.Screen.Clear();
                    BoardPrint();
                    isPlayerOne = !isPlayerOne;
                    if (numOfValidMovesForPlayer1 == 0 && numOfValidMovesForPlayer2 == 0)
                    {
                        gameOver = true;
                    }
                } while (!gameOver);
                Point scoreOfPlayers = m_GameEngine.ScoreCount(m_GameEngine.Board);
                int scoreOfPlayer1 = scoreOfPlayers.X;
                int scoreOfPlayer2 = scoreOfPlayers.Y;
                string winnerPlayer = "None";
                if (scoreOfPlayer1 > scoreOfPlayer2)
                {
                    winnerPlayer = m_GameEngine.Player1.PlayerName;
                }
                else if (scoreOfPlayer1 < scoreOfPlayer2)
                {
                    winnerPlayer = m_GameEngine.Player2.PlayerName;
                }
                else
                {
                    winnerPlayer = "No winner here, this is even...";
                }
                Console.WriteLine("{0}GameOver!{0}Black Pcs : {1}, White Pcs : {2}{0}And the winner is : {3}", Environment.NewLine, m_GameEngine.ScoreCount(m_GameEngine.Board).X, m_GameEngine.ScoreCount(m_GameEngine.Board).Y, winnerPlayer);
                Console.WriteLine("Do you want another round?..(Y/N)");
                string isKeepPlaying = Console.ReadLine();
                isKeepPlaying = isKeepPlaying.ToUpper();
                if(isKeepPlaying == "Y")
                {
                    keepPlaying = true;
                }
                else
                {
                    keepPlaying = false;
                }
            } while (keepPlaying);
            }

        public bool CheckAndSubmitUserInput(string i_UserInput, bool i_IsPlayerOne)
        {
            int rowChoise;
            int colChoise;
            i_UserInput = i_UserInput.ToUpper();
            if (i_UserInput == "")
            {
                return false;
            }
            string[] chosenCell = i_UserInput.Split(',');
            if(chosenCell[1].Length > 1)
            {
                return false;
            }
            int.TryParse(chosenCell[0], out rowChoise);
            colChoise = (char.Parse(chosenCell[1]) - 64);
            Point playerPoint = new Point();
            playerPoint.X = colChoise - 1;
            playerPoint.Y = rowChoise - 1;
            if (!m_GameEngine.IsUserInputPointInBoundaries(playerPoint))
            {
                return false;
            }else
            {
                return m_GameEngine.HumanMove(playerPoint, i_IsPlayerOne);
            }
        }
        private void PlayerVsPlayer()
        {
            Console.WriteLine("Player 1 - What is your name? ");
            m_GameEngine.CreateFirstPlayer(Console.ReadLine());
            Console.WriteLine("Player 2 - What is your name? ");
            m_GameEngine.CreateSecondPlayer(Console.ReadLine());
        }
        private void PlayerVsComputer()
        {
            Console.WriteLine("Player 1 - What is your name? ");
            m_GameEngine.CreateFirstPlayer(Console.ReadLine());
            m_GameEngine.CreateComputerPlayer();
        }



        public void BoardPrint()
        {
            Piece[,] matrixCells = m_GameEngine.Board;
            int boardSize = m_GameEngine.BoardSize;
            for (int rowsCounter = 0; rowsCounter <= boardSize; rowsCounter++)
            {
                for (int columnsCounter = 0; columnsCounter <= boardSize; columnsCounter++)
                {
                    if (rowsCounter == 0 && columnsCounter == 0)
                        Console.Write("    ");
                    else if (rowsCounter == 0 && columnsCounter != 0)
                    {
                        Console.Write("{0}", (char)(columnsCounter + 64));
                        Console.Write("   ");
                    }
                    else if (rowsCounter != 0 && columnsCounter == 0)
                    {
                        Console.Write("{0}", (rowsCounter));
                        Console.Write(" | ");
                    }
                    else if (matrixCells[rowsCounter - 1, columnsCounter - 1] != Piece.Empty)
                    {
                        string symbol = " ";
                        Piece cellValue = matrixCells[rowsCounter - 1, columnsCounter - 1];
                        if (cellValue == Piece.Black)
                        {
                            symbol = "X";
                        }
                        else if (cellValue == Piece.White)
                        {
                            symbol = "O";
                        }

                        Console.Write(symbol);
                        Console.Write(" | ");
                    }
                    else
                    {
                        Console.Write(" ");
                        Console.Write(" | ");
                    }
                }
                StringBuilder lineSeparator = new StringBuilder();
                //Console.WriteLine(Environment.NewLine);
                lineSeparator.Append("\n");
                for (int columnsCounter = 0; columnsCounter <= boardSize; columnsCounter++)
                {
                    lineSeparator.Append("====");
                }
                Console.WriteLine(lineSeparator);
            }
        }
    }
}
