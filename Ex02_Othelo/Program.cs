using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class Program
    {
        public static void Main()
        {
            System.Console.WriteLine("Welcome to Othelo game, have fun!");
            // OtheloUI OtheloGame = new OtheloUI();
            // OtheloGame.StartPlay();
            OtheloBoard board = new OtheloBoard(8);
            // board.BoardPrint();
            OtheloUI.OtheloUIMenu();
            //board.BoardPrint();
            System.Console.ReadLine();
            //matan miki manor
        }
    }
}
