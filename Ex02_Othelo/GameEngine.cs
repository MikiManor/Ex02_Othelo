using System;
using System.Collections.Generic;
using System.Text;
public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

namespace Ex02_Othelo
{
    public class GameEngine
    {
        private Player m_Player1, m_Player2;
        private string m_ComputerName = "KOKO";
        private OtheloBoard m_Board;

        public Player Player1
        {
            get { return m_Player1; }
        }

        public Player Player2
        {
            get { return m_Player2; }
        }

        public Piece[,] Board
        {
            get { return m_Board.Matrix; }
        }

        public int BoardSize
        {
            get { return m_Board.BoardSize; }
        }

        public string ComputerName
        {
            get { return m_ComputerName; }
        }

        public void CreateBoard(int i_MatrixSize)
        {
            m_Board = new OtheloBoard(i_MatrixSize);
        }

		public void  CreateFirstPlayer(string i_PlayerName)
        {
            m_Player1 = new Player(Piece.Black, i_PlayerName);
        }
        
        public void CreateSecondPlayer(string i_PlayerName)
        {
            m_Player2 = new Player(Piece.White, i_PlayerName);
        }

        public void CreateComputerPlayer()
        {
            m_Player2 = new Player(Piece.White, m_ComputerName);
        }
                
        public bool IsUserInputPointInBoundaries(Point i_UserInputPoint)
        {
            if(i_UserInputPoint.X >= BoardSize || i_UserInputPoint.X < 0 || i_UserInputPoint.Y >= BoardSize || i_UserInputPoint.Y < 0)
            {
                return false;
            }else
            {
                return true;
            }
        }

        public bool HumanMove(Point i_PlayerPoint, bool i_IsFirstPlayer)
        {
            
            Piece symbolOfCurrentPlayer = Piece.Empty;
            Piece symbolOfOtherPlayer = Piece.Empty;
            if (i_IsFirstPlayer)
            {
                symbolOfCurrentPlayer = Piece.Black;
                symbolOfOtherPlayer = Piece.White;
            }else
            {
                symbolOfCurrentPlayer = Piece.White;
                symbolOfOtherPlayer = Piece.Black;
            }
            if (ValidateMove(i_PlayerPoint, m_Board.Matrix, symbolOfCurrentPlayer, symbolOfOtherPlayer))
            {
                MakeMove(i_PlayerPoint, m_Board.Matrix, symbolOfCurrentPlayer, symbolOfOtherPlayer);
                return true;
            }else
            {
                return false;
            }
        }

        public Point PcAI(Piece[,] board, Point[] validpointlist)
        {
            Point tempscore = new Point();
            tempscore.X = BoardSize * BoardSize;
            tempscore.Y = 0;
            Point goodplay = new Point();
            for (int k = 0; k < validpointlist.GetLength(0); k++)
            {
                Piece[,] tempboard = new Piece[BoardSize,BoardSize];
                for (int i = 0; i < BoardSize; i++) //duplicate board
                {
                    for (int j = 0; j < BoardSize; j++)
                    {
                        tempboard[i,j] = board[i,j];
                    }
                }
                MakeMove(validpointlist[k], tempboard, Piece.White, Piece.Black);

                if ((ScoreCount(tempboard).Y > tempscore.Y) && (ScoreCount(tempboard).X < tempscore.X))
                {
                    goodplay.X = validpointlist[k].X;
                    goodplay.Y = validpointlist[k].Y;
                }

            }
            return goodplay;
        }

        public bool ValidateMove(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer)
        {
            if (board[Move.Y,Move.X] != Piece.Empty)
                return false;
            else if (ValidateUp(Move, board, CurrentPlayer, OtherPlayer) || ValidateUpRight(Move, board, CurrentPlayer, OtherPlayer) || ValidateRight(Move, board, CurrentPlayer, OtherPlayer) || ValidateDownRight(Move, board, CurrentPlayer, OtherPlayer) || ValidateDown(Move, board, CurrentPlayer, OtherPlayer) || ValidateDownLeft(Move, board, CurrentPlayer, OtherPlayer) || ValidateLeft(Move, board, CurrentPlayer, OtherPlayer) || ValidateUpLeft(Move, board, CurrentPlayer, OtherPlayer))
            {
                return true;
            }
            else 
                return false;
        }

        public void MakePcMove(Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer)
        {
            Point[] validpointlist;
            validpointlist = AvalibleMoves(board, CurrentPlayer, OtherPlayer); // list pc moves
            if (validpointlist.Length == 0)
            {
                Console.WriteLine("No moves!");
                return;
            }
            Point pcmove = new Point();
            pcmove = PcAI(board, validpointlist);
            MakeMove(pcmove, board, CurrentPlayer, OtherPlayer);//pc choose play
            Console.WriteLine("[PC will Play : {0},{1}] ", pcmove.Y +1, pcmove.X +1);
            System.Console.ReadLine();
        }
        public Point[] AvalibleMoves(Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer)
        {
            Point[] Tempvalidpoint = new Point[BoardSize * BoardSize];
            Point Testpoint = new Point();
            int k = 0;
            int counter = 0;
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (i == 4 && j == 6)
                    {
                        Console.Write("");
                    }
                    Testpoint.X = j;
                    Testpoint.Y = i;
                    if (ValidateMove(Testpoint, board, CurrentPlayer, OtherPlayer))
                    {
                        Tempvalidpoint[k].X = Testpoint.X;
                        Tempvalidpoint[k].Y = Testpoint.Y;
                        counter++;
                        k++;
                    }
                }
            }
            Point[] NewValidPoint = new Point[counter];
            for (int i = 0; i < counter; i++)
            {
                NewValidPoint[i].X = Tempvalidpoint[i].X;
                NewValidPoint[i].Y = Tempvalidpoint[i].Y;
            }
            return NewValidPoint;
        }

        public void MakeMove(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer)
        {
            board[Move.Y,Move.X] = CurrentPlayer;

            if (ValidateUp(Move, board, CurrentPlayer, OtherPlayer))
            {
                for (int i = Move.Y - 1; i >= 0; i--)
                {
                    if (board[i,Move.X] == OtherPlayer)
                        board[i,Move.X] = CurrentPlayer;
                    else
                        break;
                }
            }
            if (ValidateUpRight(Move, board, CurrentPlayer, OtherPlayer))
            {
                for (int i = Move.Y - 1, j = Move.X + 1; i >= 0 && j < BoardSize; i--, j++)
                {
                    if (board[i,j] == OtherPlayer)
                        board[i,j] = CurrentPlayer;
                    else
                        break;
                }
            }
            if (ValidateRight(Move, board, CurrentPlayer, OtherPlayer))
            {
                for (int j = Move.X + 1; j < BoardSize; j++)
                {
                    if (board[Move.Y,j] == OtherPlayer)
                        board[Move.Y,j] = CurrentPlayer;
                    else
                        break;
                }
            }
            if (ValidateDownRight(Move, board, CurrentPlayer, OtherPlayer))
            {
                for (int i = Move.Y + 1, j = Move.X + 1; i < BoardSize && j < BoardSize; i++, j++)
                {
                    if (board[i,j] == OtherPlayer)
                        board[i,j] = CurrentPlayer;
                    else
                        break;
                }
            }
            if (ValidateDown(Move, board, CurrentPlayer, OtherPlayer))
            {
                for (int i = Move.Y + 1; i < BoardSize; i++)
                {
                    if (board[i,Move.X] == OtherPlayer)
                        board[i,Move.X] = CurrentPlayer;
                    else
                        break;
                }
            }
            if (ValidateDownLeft(Move, board, CurrentPlayer, OtherPlayer))
            {
                for (int i = Move.Y + 1, j = Move.X - 1; i < BoardSize && j >= 0; i++, j--)
                {
                    if (board[i,j] == OtherPlayer)
                        board[i,j] = CurrentPlayer;
                    else
                        break;
                }
            }
            if (ValidateLeft(Move, board, CurrentPlayer, OtherPlayer))
            {
                for (int j = Move.X - 1; j >= 0; j--)
                {
                    if (board[Move.Y,j] == OtherPlayer)
                        board[Move.Y,j] = CurrentPlayer;
                    else
                        break;
                }
            }
            if (ValidateUpLeft(Move, board, CurrentPlayer, OtherPlayer))
            {
                for (int i = Move.Y - 1, j = Move.X - 1; i >= 0 && j >= 0; i--, j--)
                {
                    if (board[i,j] == OtherPlayer)
                        board[i,j] = CurrentPlayer;
                    else
                        break;
                }
            }
        }

        public static bool ValidateUp(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer)
        {
            if (Move.Y <= 0)
                return false;
            else if (board[Move.Y - 1,Move.X] == OtherPlayer)
                for (int i = Move.Y - 2; i >= 0; i--)
                {
                    if (board[i,Move.X] == CurrentPlayer)
                    {
                       // Console.WriteLine("ValidateUp");
                        return true;
                    }
                    else if (board[i,Move.X] == Piece.Empty)
                        return false;
                }
             return false;
        }
		
        public bool ValidateUpRight(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer )
        {
            if (Move.Y <= 0 || Move.X >= BoardSize-1)
                return false;
            else if (board[Move.Y-1,Move.X+1] == OtherPlayer)
                for (int i = Move.Y - 2, j = Move.X+2; i >= 0 && j < BoardSize; i--, j++)
                {
                    if (board[i,j] == CurrentPlayer)
                    {
                    //    Console.WriteLine("ValidateUpRight");
                        return true;
                    }
                    else if (board[i,j] == Piece.Empty)
                        return false;
                }
            return false;
        }
		
        public bool ValidateRight(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer )
        {
            if (Move.X >= BoardSize - 1)
                return false;
            else if (board[Move.Y,Move.X+1] == OtherPlayer)
                for (int j = Move.X + 2; j < BoardSize; j++)
                {
                    if (board[Move.Y,j] == CurrentPlayer)
                    {
                      //  Console.WriteLine("ValidateRight");
                        return true;
                    }
                    else if (board[Move.Y,j] == Piece.Empty)
                        return false;
                }
            return false;
        }
		
        public bool ValidateDownRight(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer )
        {
            if (Move.Y >= BoardSize - 1 || Move.X >= BoardSize - 1)
                return false;
            else if (board[Move.Y+1,Move.X+1] == OtherPlayer)
                for (int i = Move.Y + 2, j = Move.X + 2; i < BoardSize && j < BoardSize; i++, j++)
                {
                    if (board[i,j] == CurrentPlayer)
                    {
                      //  Console.WriteLine("ValidateDownRight");
                        return true;
                    }
                    else if (board[i,j] == Piece.Empty)
                        return false;
                }
            return false;
        }
		
        public  bool ValidateDown(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer )
        {
            if (Move.Y >= BoardSize - 1)
                return false;
            else if (board[Move.Y+1,Move.X] == OtherPlayer)
                for (int i = Move.Y + 2; i < BoardSize; i++)
                {
                    if (board[i,Move.X] == CurrentPlayer)
                    {
                     //   Console.WriteLine("ValidateDown");
                        return true;
                    }
                    else if (board[i,Move.X] == Piece.Empty)
                        return false;
                }
            return false;
        }
		
        public bool ValidateDownLeft(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer )
        {
            if (Move.Y >= BoardSize - 1 || Move.X <= 0)
                return false;
           else if (board[Move.Y+1,Move.X-1] == OtherPlayer)
                for (int i = Move.Y + 2, j = Move.X - 2; i < BoardSize && j >= 0; i++, j--)
                {
                    if (board[i,j] == CurrentPlayer)
                    {
                    //    Console.WriteLine("ValidateDownLeft");
                        return true;
                    }
                    else if (board[i,j] == Piece.Empty)
                        return false;
                }
            return false;
        }
		
        public bool ValidateLeft(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer )
        {
            if (Move.X <= 0)
                return false;
            else if (board[Move.Y,Move.X-1] == OtherPlayer)
                for (int j = Move.X - 2; j >= 0; j--)
                {
                    if (board[Move.Y,j] == CurrentPlayer)
                    {
                     //   Console.WriteLine("ValidateLeft");
                        return true;
                    }
                    else if (board[Move.Y,j] == Piece.Empty)
                        return false;
                }
            return false;
        }
		
        public bool ValidateUpLeft(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer )
        {
            if (Move.Y <= 0 || Move.X <= 0)
                return false;
            else if (board[Move.Y-1,Move.X-1] == OtherPlayer)
                for (int i = Move.Y - 2,  j = Move.X - 2; i >= 0 && j >= 0; i--, j--)
                {
                    if (board[i,j] == CurrentPlayer)
                    {
                    //    Console.WriteLine("ValidateUpLeft");
                        return true;
                    }
                    else if (board[i,j] == Piece.Empty)
                        return false;
                }
            return false;
        }
		

        public Point ScoreCount(Piece[,] board)
        {
            Point score = new Point();
            score.X = 0;
            score.Y = 0;
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (board[i,j] == Piece.Black)
                    {
                        score.X++;
                    }else if (board[i,j] == Piece.White)
                    {
                        score.Y++;
                    }
                }
            }
            return score;
        }
    }
}
