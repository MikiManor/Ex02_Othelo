﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public enum Piece
    {
        Empty,
        Black,
        White
    }
    public class OtheloBoard
    {
        private readonly int m_MatrixSize;
        public static Piece[,] m_MatrixCells;
        public OtheloBoard(int i_MatrixSize)
        {
            m_MatrixSize = i_MatrixSize;
            BoardFirstInitialization();
        }

        private void BoardFirstInitialization()
        {
            m_MatrixCells = new Piece[m_MatrixSize,m_MatrixSize];
            for (int rowsCounter = 0; rowsCounter < m_MatrixSize; rowsCounter++)
                for (int columnsCounter = 0; columnsCounter < m_MatrixSize; columnsCounter++)
                {
                    m_MatrixCells[rowsCounter, columnsCounter]= Piece.Empty;
                }
            m_MatrixCells[(m_MatrixSize / 2) - 1, (m_MatrixSize / 2) - 1 ] = Piece.Black;
            m_MatrixCells[(m_MatrixSize / 2) - 1, (m_MatrixSize / 2) ] = Piece.White;
            m_MatrixCells[(m_MatrixSize / 2), (m_MatrixSize / 2) - 1 ] = Piece.White;
            m_MatrixCells[(m_MatrixSize / 2), (m_MatrixSize / 2) ] = Piece.Black;
        }

        private Piece GetCellValue(int i_RowNumber, int i_ColumnNumber)
        {
            return m_MatrixCells[i_RowNumber, i_ColumnNumber];
        }
        public void SetCellValue(Point i_CellLoaction, Piece i_CellValue)
        {
            int cellRow = i_CellLoaction.Y;
            int cellColumn = i_CellLoaction.X;
            m_MatrixCells[cellRow, cellColumn] = i_CellValue;
        }
        public void BoardPrint()
        {
            for (int rowsCounter = 0; rowsCounter <= m_MatrixSize; rowsCounter++)
            {
                for (int columnsCounter = 0; columnsCounter <= m_MatrixSize; columnsCounter++)
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
                    else if (m_MatrixCells[rowsCounter - 1, columnsCounter - 1] != Piece.Empty)
                    {
                        string symbol = " ";
                        Piece cellValue = m_MatrixCells[rowsCounter - 1, columnsCounter - 1];
                        if (cellValue == Piece.Black)
                        {
                            symbol = "X";
                        }else if (cellValue == Piece.White)
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
                for (int columnsCounter = 0; columnsCounter <= m_MatrixSize; columnsCounter++)
                {
                    lineSeparator.Append("====");
                }
                Console.WriteLine(lineSeparator);

                    
            }
        }

        public int Matrix
        {
            get { return m_MatrixSize; }
            
        }
        


    }
}
