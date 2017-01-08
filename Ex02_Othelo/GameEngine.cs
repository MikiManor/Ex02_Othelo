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
        public Player m_Player1, m_Player2;
        private string m_ComputerName = "KOKO";
        public static List<string> NextMove()
        {
            List<string> moves = null;
            return moves;
        }//check avalible moves
        
            public static bool CanBeAMove()
        {
            return true;
        }//check if can be moves

        
        public bool HumanMove(string i_UserInput,bool i_IsFirstPlayer)
        {
            i_UserInput = i_UserInput.ToUpper();
            Piece symbolOfCurrentPlayer = Piece.Empty;
            Piece symbolOfOtherPlayer = Piece.Empty;
            int rowChoise;
            int colChoise;
            if (i_IsFirstPlayer)
            {
                symbolOfCurrentPlayer = Piece.Black;
                symbolOfOtherPlayer = Piece.White;
            }else
            {
                symbolOfCurrentPlayer = Piece.White;
                symbolOfOtherPlayer = Piece.Black;
            }
            string[] chosenCell = i_UserInput.Split(',');
            int.TryParse(chosenCell[0], out rowChoise);
            colChoise = (char.Parse(chosenCell[1]) - 64);
            Point playerPoint = new Point();
            playerPoint.X = colChoise - 1 ;
            playerPoint.Y = rowChoise - 1;
            if (ValidateMove(playerPoint, OtheloBoard.m_MatrixCells, symbolOfCurrentPlayer, symbolOfOtherPlayer))
            {
                MakeMove(playerPoint, OtheloBoard.m_MatrixCells, symbolOfCurrentPlayer, symbolOfOtherPlayer);
                return true;
            }else
            {
                return false;
            }
        }
        

        public void  CreateFirstPlayer(string i_PlayerName)
        {
            m_Player1 = new Player(Piece.Black, i_PlayerName);
        }

        public string Player1
        {
            get { return m_Player1.PlayerName; }
        }

        public void CreateSecondPlayer(string i_PlayerName)
        {
            m_Player2 = new Player(Piece.White, i_PlayerName);
        }

        public string Player2
        {
            get { return m_Player2.PlayerName; }
        }
        public void CreateComputerPlayer()
        {
            m_Player2 = new Player(Piece.White, m_ComputerName);
        }

        public static bool ValidateMove(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer)
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
        public static bool ValidateUp(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer)
        {
            if (Move.Y <= 0)
                return false;
            else if (board[Move.Y - 1,Move.X] == OtherPlayer)
                for (int i = Move.Y - 2; i >= 0; i--)
                {
                    if (board[i,Move.X] == CurrentPlayer)
                        return true;
                    else if (board[i,Move.X] == Piece.Empty)
                        return false;
                }
            return false;
        }

        public static void MakeMove(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer)
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
                for (int i = Move.Y - 1, j = Move.X + 1; i >= 0 && j < board.GetLength(0); i--, j++)
                {
                    if (board[i,j] == OtherPlayer)
                        board[i,j] = CurrentPlayer;
                    else
                        break;
                }
            }
            if (ValidateRight(Move, board, CurrentPlayer, OtherPlayer))
            {
                for (int j = Move.X + 1; j < board.GetLength(0); j++)
                {
                    if (board[Move.Y,j] == OtherPlayer)
                        board[Move.Y,j] = CurrentPlayer;
                    else
                        break;
                }
            }
            if (ValidateDownRight(Move, board, CurrentPlayer, OtherPlayer))
            {
                for (int i = Move.Y + 1, j = Move.X + 1; i < board.GetLength(0) && j < board.GetLength(0); i++, j++)
                {
                    if (board[i,j] == OtherPlayer)
                        board[i,j] = CurrentPlayer;
                    else
                        break;
                }
            }
            if (ValidateDown(Move, board, CurrentPlayer, OtherPlayer))
            {
                for (int i = Move.Y + 1; i < board.GetLength(0); i++)
                {
                    if (board[i,Move.X] == OtherPlayer)
                        board[i,Move.X] = CurrentPlayer;
                    else
                        break;
                }
            }
            if (ValidateDownLeft(Move, board, CurrentPlayer, OtherPlayer))
            {
                for (int i = Move.Y + 1, j = Move.X - 1; i < board.GetLength(0) && j >= 0; i++, j--)
                {
                    if (board[i,Move.X] == OtherPlayer)
                        board[i,Move.X] = CurrentPlayer;
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

        public static bool ValidateUpRight(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer)
        {
            if (Move.Y <= 0 || Move.X >= board.GetLength(0) - 1)
                return false;
            else if (board[Move.Y - 1,Move.X + 1] == OtherPlayer)
                for (int i = Move.Y - 2, j = Move.X + 2; i >= 0 && j < board.GetLength(0); i--, j++)
                {
                    if (board[i,j] == CurrentPlayer)
                        return true;
                    else if (board[i,Move.X] == Piece.Empty)
                        return false;
                }
            return false;
        }
        public static bool ValidateRight(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer)
        {
            if (Move.X >= board.GetLength(0) - 1)
                return false;
            else if (board[Move.Y,Move.X + 1] == OtherPlayer)
                for (int j = Move.X + 2; j < board.GetLength(0); j++)
                {
                    if (board[Move.Y,j] == CurrentPlayer)
                        return true;
                    else if (board[Move.Y,Move.X] == Piece.Empty)
                        return false;
                }
            return false;
        }
        public static bool ValidateDownRight(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer)
        {
            if (Move.Y >= board.GetLength(0) - 1 || Move.X >= board.GetLength(0) - 1)
                return false;
            else if (board[Move.Y + 1,Move.X + 1] == OtherPlayer)
                for (int i = Move.Y + 2, j = Move.X + 2; i < board.GetLength(0) && j < board.GetLength(0); i++, j++)
                {
                    if (board[i,j] == CurrentPlayer)
                        return true;
                    else if (board[i,j] == Piece.Empty)
                        return false;
                }
            return false;
        }
        public static bool ValidateDown(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer)
        {
            if (Move.Y >= board.GetLength(0) - 1)
                return false;
            else if (board[Move.Y + 1,Move.X] == OtherPlayer)
                for (int i = Move.Y + 2; i < board.GetLength(0); i++)
                {
                    if (board[i,Move.X] == CurrentPlayer)
                        return true;
                    else if (board[i,Move.X] == Piece.Empty)
                        return false;
                }
            return false;
        }
        public static bool ValidateDownLeft(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer)
        {
            if (Move.Y >= board.GetLength(0) - 1 || Move.X <= 0)
                return false;
            else if (board[Move.Y + 1,Move.X - 1] == OtherPlayer)
                for (int i = Move.Y + 2, j = Move.X - 2; i < board.GetLength(0) && j >= 0; i++, j--)
                {
                    if (board[i,Move.X] == CurrentPlayer)
                        return true;
                    else if (board[i,j] == Piece.Empty)
                        return false;
                }
            return false;
        }
        public static bool ValidateLeft(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer)
        {
            if (Move.X <= 0)
                return false;
            else if (board[Move.Y,Move.X - 1] == OtherPlayer)
                for (int j = Move.X - 2; j >= 0; j--)
                {
                    if (board[Move.Y,j] == CurrentPlayer)
                        return true;
                    else if (board[Move.Y,j] == Piece.Empty)
                        return false;
                }
            return false;
        }
        public static bool ValidateUpLeft(Point Move, Piece[,] board, Piece CurrentPlayer, Piece OtherPlayer)
        {
            if (Move.Y <= 0 || Move.X <= 0)
                return false;
            else if (board[Move.Y - 1,Move.X - 1] == OtherPlayer)
                for (int i = Move.Y - 2, j = Move.X - 2; i >= 0 && j >= 0; i--, j--)
                {
                    if (board[i,j] == CurrentPlayer)
                        return true;
                    else if (board[i,j] == Piece.Empty)
                        return false;
                }
            return false;
        }
        public static Point[] AvalibleMoves(Piece[,] board)
        {
            Point[] valpoint = new Point[board.GetLength(0) * board.GetLength(0)];

            return valpoint;

        }
    }
}
