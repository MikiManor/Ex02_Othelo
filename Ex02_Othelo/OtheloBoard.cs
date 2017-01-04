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
            for (int rowsCounter = 0; rowsCounter < m_MatrixSize; rowsCounter++)
            {
                for (int columnsCounter = 0; columnsCounter < m_MatrixSize; columnsCounter++)
                {
                    if (m_MatrixCells[rowsCounter, columnsCounter].HasValue)
                    {
                        int cellValue = m_MatrixCells[rowsCounter, columnsCounter].Value;
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
