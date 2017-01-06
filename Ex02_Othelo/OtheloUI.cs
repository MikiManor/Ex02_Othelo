using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class OtheloUI
    {
        public static void OtheloUIMenu()
        {
            int boardSize=8;
            int menu = 9;
            Ex02.ConsoleUtils.Screen.Clear();
            do
            {
               
                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine("{0}{0}{0}Board size is: {1}{0}{0}{0}", Environment.NewLine, boardSize);
                Console.WriteLine("(1) play vs human.{0}(2) play vs pc.{0}(3) Change board size.{0}(0) Exit.{0}{0}{0}Please Choose :/> ", Environment.NewLine);
                try
                {
                    menu = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Input ERROR. press any key...");
                    menu = 9;
                    throw;
                }
                
                if (menu == 3)
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
                if (menu > 3 || menu < 0)
                {
                    Console.WriteLine("Can't do that.... please make a Valide choise. press any key...");
                    Console.Beep();
                    Console.ReadLine();
                   
                }
                
            } while (menu >2 || menu < 0);
            Ex02.ConsoleUtils.Screen.Clear();
            switch (menu)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    StartPlay(boardSize);//vs human
                    break;
                case 2:
                    StartPlay(boardSize);//vs pc
                    break;
                default:
                    Environment.Exit(1);
                    break;
            }

        }
        public static void StartPlay(int boardSize)
        {
            bool playernum=false;
            int rowChoise;
            int colChoise;
            OtheloBoard board = new OtheloBoard(boardSize);
            while (GameEngine.CanBeAMove())
            {
                playernum = !playernum;
                board.BoardPrint();
                if (playernum)
                {
                    Console.WriteLine("Player 1: Make your move [row,col] : ");
                }
                else
                {
                    Console.WriteLine("Player 2: Make your move [row,col] : ");
                }
                try
                {
                    rowChoise = int.Parse(Console.ReadLine());
                    colChoise = int.Parse(Console.ReadLine());
                    Console.WriteLine("is: [{0},{1}]", rowChoise, colChoise);
                }
                catch (Exception)
                {
                    Console.WriteLine("Input ERROR. press any key...");
                    throw;
                }

   
            }
           
        }

    }
}
