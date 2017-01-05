using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public struct BoardCell
    {
        private bool m_IsCellInUse;
        private int m_CellValue;
        public int CellValue
        {
            get { return m_CellValue; }
            set { m_CellValue = value; }
        }
        public bool isCellUsed
        {
            get { return m_IsCellInUse; }
            set { m_IsCellInUse = value; }
        }

    }
    class OtheloBoard
    {
        private readonly int m_MatrixSize;
        private int?[,] m_MatrixCells;
        char alphabet = 'A';
        public OtheloBoard(int i_MatrixSize)
        {
            m_MatrixSize = i_MatrixSize;
            BoardFirstInitialization();
        }

        private void BoardFirstInitialization()
        {
            m_MatrixCells = new int?[m_MatrixSize,m_MatrixSize];
            for (int rowsCounter = 0; rowsCounter < m_MatrixSize; rowsCounter++)
                for (int columnsCounter = 0; columnsCounter < m_MatrixSize; columnsCounter++)
                {
                    m_MatrixCells[rowsCounter, columnsCounter]= null;
                }
            m_MatrixCells[(m_MatrixSize / 2)-1, (m_MatrixSize / 2)-1] = 0;
            m_MatrixCells[(m_MatrixSize / 2)-1, (m_MatrixSize / 2)] = 1;
            m_MatrixCells[(m_MatrixSize / 2), (m_MatrixSize / 2)-1] = 1;
            m_MatrixCells[(m_MatrixSize / 2), (m_MatrixSize / 2)] = 0;
        }

        private int? GetCellValue(int i_RowNumber, int i_ColumnNumber)
        {
            return m_MatrixCells[i_RowNumber, i_ColumnNumber];
        }
        public void SetCellValue(int i_RowNumber, int i_ColumnNumber, int i_Value)
        {
            m_MatrixCells[i_RowNumber, i_ColumnNumber] = i_Value;
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
                        Console.Write(alphabet);
                        alphabet++;
                        Console.Write(" , ");
                    }
                    else if (rowsCounter != 0 && columnsCounter == 0)
                    {
                        Console.Write("{0}", (rowsCounter));
                        Console.Write(" | ");
                    }
                    else if (m_MatrixCells[rowsCounter - 1, columnsCounter - 1].HasValue)
                    {
                        int cellValue = m_MatrixCells[rowsCounter - 1, columnsCounter - 1].Value;
                        Console.Write(cellValue);
                        Console.Write(" , ");
                    }
                    else
                    {
                        Console.Write("_");
                        Console.Write(" , ");
                    }
                }
                Console.WriteLine(Environment.NewLine);
            }
        }

        public int Matrix
        {
            get { return m_MatrixSize; }
        }
        


    }
}
