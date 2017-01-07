using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class OtheloUI
    {
        private GameEngine m_GameEngine;
        private static OtheloBoard m_GameBoard;
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
                    Console.WriteLine("Please Choose a new board size :/> ");

                    try
                    {
                        boardSize = int.Parse(Console.ReadLine()); //add limit to max size 'Z' 
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Input ERROR. press any key...");
                        boardSize = 8;
                        throw;
                    }
                }
                if (menuSelection > 3 || menuSelection < 0)
                {
                    Console.WriteLine("Can't do that.... please enter valid option. press any key...");
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
            OtheloBoard board = new OtheloBoard(i_boardSize);
            if (i_menuSelection == 1)
            {
                PlayerVsPlayer();
            }else if (i_menuSelection == 2)
            {
                PlayerVsComputer();
            }
            bool isPlayerOne = true;
            bool humanMoveResponse = true;
            do
            {
                
                do
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    
                    if (!humanMoveResponse)
                    {
                        Console.WriteLine("************ {0} is Not a Valid move! try again... ************\n", userInput);
                    }
                    board.BoardPrint();
                    if (isPlayerOne)
                    {
                        Console.WriteLine("{0} -  your symbole is  {1} - Make your move : row,col : ", m_GameEngine.m_Player1.PlayerName, m_GameEngine.m_Player1.Symbol);
                    }
                    else
                    {
                        Console.WriteLine("{0} -  your symbole is  {1} - Make your move : row,col : ", m_GameEngine.m_Player2.PlayerName, m_GameEngine.m_Player2.Symbol);
                    }
                    userInput = Console.ReadLine();
                    humanMoveResponse = m_GameEngine.HumanMove(userInput, isPlayerOne);
                } while (!humanMoveResponse);
                    board.BoardPrint();
                    isPlayerOne = !isPlayerOne;
            } while (true);
            
            

        }
        private void PlayerVsPlayer()
        {
            Console.WriteLine("Player 1 - What is your name? ");
            m_GameEngine.CreateFirstPlayer(Console.ReadLine());
            Console.WriteLine("Player 2 - What is your name? ");
            m_GameEngine.CreateSecondPlayer(Console.ReadLine());




            
        }
        private  void PlayerVsComputer()
        {
            Console.WriteLine("Player 1 - What is your name? ");
            m_GameEngine.CreateFirstPlayer(Console.ReadLine());
            m_GameEngine.CreateComputerPlayer();
        }

        

        private void PrintBoard()
        {
            
        }
    }
}
