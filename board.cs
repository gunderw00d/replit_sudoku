using System;
using System.IO;

internal class Board
{
    // the sudoku board - Row Major
    private int[,] theBoard;

    const int rowCount = 9;
    const int colCount = 9;
    const int squareLength = 9;
    const int numSquares = 9;

    public Board(bool testConfig)
    {
      theBoard = new int[colCount, rowCount];

      // Iterate through each row
      for (int i = 0; i < rowCount; i++)
      {
        // Iterate through each column
        for (int j = 0; j < colCount; j++)
        {
          // row[i] column[j] = value
          if (testConfig) {
            theBoard[i, j] = (i * colCount) + j;  
          }
          else {
            theBoard[i, j] = 0;
          }
        }
      }
    }

    public bool readFile(string fileName) {
      // open the file
      // read 9 lines with 9 ints, separated by spaces
      string[] lines = System.IO.File.ReadAllLines(fileName);
      for (int i = 0; i < lines.Length; i++) {
        //Console.WriteLine(lines[i]);
        string[] subs = lines[i].Split(' ');
        for (int j = 0; j < subs.Length; j++) {
          //Console.WriteLine(subs[j]);
          int v = Int32.Parse(subs[j]);
          theBoard[i, j] = v;
        }
      }
      
      return true;
    }
  
    // Some debug prints
    public void printBoard() {
      Console.WriteLine("--- The Board ---");
      for (int i = 0; i < rowCount; i++) {
        string aRow = "";
        for (int j = 0; j < colCount; j++) {
          aRow += " ";
          if ((j == 3) || (j == 6)) {
            aRow += "  ";
          }
          aRow += theBoard[i,j];
        }
        Console.WriteLine(aRow);
        if ((i == 2) || (i == 5)) {
          Console.WriteLine();
        }
      }
      Console.WriteLine("--- --- ----- ---");
    }

    public void printRow(int rowNum)
    {
      if ((rowNum < 0) || (rowNum >= rowCount)) {
        Console.WriteLine(
          "Board::printRow(rowNum = {0:D}) - invalid rowNum, must be between 0 and {1:D}",
          rowNum, rowCount - 1);
      }

      int [] theRow = this.getRow(rowNum);
      
      Console.WriteLine("--- Row {0:D} ---", rowNum);
      string aRow = "";
      for (int i = 0; i < colCount; i++) {
        aRow += " ";
        if ((i == 3) || (i == 6)) {
          aRow += "  ";
        }
        aRow += theRow[i];
      }
      Console.WriteLine(aRow);
    }

    public void printColumn(int columnNum)
    {
      if ((columnNum < 0) || (columnNum >= colCount)) {
        Console.WriteLine(
          "Board::printColumn(columnNum = {0:D}) - invalid colNum, must be between 0 and {1:D}",
          columnNum, colCount - 1);
      }

      int [] theCol = this.getColumn(columnNum);
      
      Console.WriteLine("--- Column {0:D} ---", columnNum);
      string aCol = "";
      for (int i = 0; i < rowCount; i++) {
        aCol += " ";
        if ((i == 3) || (i == 6)) {
          aCol += "  ";
        }
        aCol += theCol[i];
      }
      Console.WriteLine(aCol);
    }

  
    public void printSquare(int squareNum)
    {
      if ((squareNum < 0) || (squareNum >= numSquares)) {
        Console.WriteLine(
          "Board::printSquare(squareNum = {0:D}) - invalid squareNum, must be between 0 and {1:D}",
          squareNum, numSquares - 1);
      }

      int [] theSquare = this.getSquare(squareNum);
      
      Console.WriteLine("--- Square {0:D} ---", squareNum);
      string aSquare = "";
      for (int i = 0; i < squareLength; i++) {
        aSquare += " " + theSquare[i];
        if ((i == 2) || (i == 5) || (i == 8)) {
          Console.WriteLine(aSquare);
          aSquare = "";
        }
      }
    }
  
    /*
      return the contents of the row indicated by rowNum.
      Rows are 0-indexed, starting at the top of the board

      Parameters
        rowNum:  int, from 0 - 8.

      Returns:
        array of ints for the row, in order from left to right.
        0 indicates empty, 1-9 represent values for Sudoku puzzle.
    */
    public int[] getRow(int rowNum)
    {
      int[] row = new int[colCount];
      if ((rowNum < 0) || (rowNum >= rowCount)) {
        Console.WriteLine(
          "Board::getRow(rowNum = (0:D)) - invalid rowNum, must be between 0 and (1:D)",
          rowNum, rowCount - 1);
        return row;
      }
      int offset = rowNum * colCount;

      for (int j = 0; j < colCount; j++) {
        row[j] = theBoard[rowNum, j];
      }

      return row;
    }

    /*
      return the contents of the column indicated by columnNum.
      Columns are 0-indexed, starting at the left of the board

      Parameters
        colNum:  int, from 0 - 8.

      Returns:
        Array of ints for the column, in order from top to bottom.
        0 indicates empty, 1-9 represent values for Sudoku puzzle.
    */
    public int[] getColumn(int columnNum)
    {
      int[] col = new int[rowCount];
      
      if ((columnNum < 0) || (columnNum >= colCount)) {
        Console.WriteLine(
          "Board::getColumn(columnNum = {0:D}) - invalid colNum, must be between 0 and {1:D}",
          columnNum, colCount - 1);
        return col;
      }

      // walk through each row, grabbing the value at [row, columnNum]
      for (int i = 0; i < rowCount; i++)
      {
          col[i] = theBoard[i, columnNum];
      }

      return col;
    }

    /*
      Return the contents of the square indicated by squareNum.
      Squares are numbered like so:

       0  1  2
       3  4  5
       6  7  8

      Parameters
        squareNum:  int, from 0 - 8.

      Returns:
        Array of ints for the square, indexed the same as the squares are
          on the board.  EG:
            0  1  2
            3  4  5
            6  7  8
        Index 0 of the return array is the top-left most value in the square.
        Index 4 is the center most value in the square, etc.
        0 indicates empty, 1-9 represent values for Sudoku puzzle.
    */
    public int[] getSquare(int squareNum)
    {
      int[] square = new int[squareLength] {0, 0, 0,  0, 0, 0,  0, 0, 0};
      int squareIndex = 0;
      int rowStart = 0;
      int rowEnd = 3;
      int colStart = 0;
      int colEnd = 3;

      if ((squareNum < 0) || (squareNum >= numSquares)) {
        Console.WriteLine(
          "Board::getSquare(squareNum = {0:D}) - invalid squareNum, must be between 0 and {1:D}",
          squareNum, numSquares - 1);
        return square;
      }

      Console.WriteLine("Board.getSquare({0:D})", squareNum);
      switch (squareNum) {
        case 0:
          rowStart = 0; rowEnd = 3;
          colStart = 0; colEnd = 3;
          break;
        case 1:
          rowStart = 0; rowEnd = 3;
          colStart = 3; colEnd = 6;
          break;
        case 2:
          rowStart = 0; rowEnd = 3;
          colStart = 6; colEnd = 9;
          break;
          
        case 3:
          rowStart = 3; rowEnd = 6;
          colStart = 0; colEnd = 3;
          break;
        case 4:
          rowStart = 3; rowEnd = 6;
          colStart = 3; colEnd = 6;
          break;
        case 5:
          rowStart = 3; rowEnd = 6;
          colStart = 6; colEnd = 9;
          break;

        case 6:
          rowStart = 6; rowEnd = 9;
          colStart = 0; colEnd = 3;
          break;
        case 7:
          rowStart = 6; rowEnd = 9;
          colStart = 3; colEnd = 6;
          break;
        case 8:
          rowStart = 6; rowEnd = 9;
          colStart = 6; colEnd = 9;
          break;

        default:
          
          Console.WriteLine("ERROR  Board.getSquare({0:D}) -- DEFAULT CASE --", squareNum);
          return square;
      }

      Console.WriteLine("Board.getSquare({0:D}), rs: {1:D}, re: {2:D}, cs: {3:D}, ce: {4:D}",
                        squareNum, rowStart, rowEnd, colStart, colEnd);
      
      for (int rowNum = rowStart; rowNum < rowEnd; rowNum ++) {
        for (int colNum = colStart; colNum < colEnd; colNum ++) {
          square[squareIndex] = theBoard[rowNum, colNum];
          squareIndex ++;
        }
      }
      
      return square;
    }
}