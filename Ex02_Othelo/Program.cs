using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class Program
    {
        public static void Main()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            //System.Console.WriteLine("Welcome to Othelo game, have fun!\n\n"); //change "/n"
            // OtheloUI OtheloGame = new OtheloUI();
            // OtheloGame.StartPlay();
            
            OtheloBoard board = new OtheloBoard(8);
            
            board.BoardPrint();
            System.Console.WriteLine("2nd round.... enter");//
            System.Console.ReadLine();
            Ex02.ConsoleUtils.Screen.Clear();
            board.SetCellValue(1, 1, 2);
            board.BoardPrint();

            //int a = Random.Next(1, 11);
            System.Console.ReadLine();
            //matan miki manor
            
        }
    }
}
