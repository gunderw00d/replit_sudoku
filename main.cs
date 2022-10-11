using System;

internal class Program {

  public static void testBoard() {
    
    Board bb = new Board(true);
    bb.printBoard();

    Console.WriteLine();
    
    bb.printSquare(0);
    bb.printSquare(1);
    bb.printSquare(2);
    
    bb.printSquare(3);
    bb.printSquare(4);
    bb.printSquare(5);
    
    bb.printSquare(6);
    bb.printSquare(7);
    bb.printSquare(8);

    Console.WriteLine();
    
    bb.printRow(0);
    bb.printRow(1);
    bb.printRow(2);
    
    bb.printRow(3);
    bb.printRow(4);
    bb.printRow(5);
    
    bb.printRow(6);
    bb.printRow(7);
    bb.printRow(8);

    Console.WriteLine();
    
    bb.printColumn(0);
    bb.printColumn(1);
    bb.printColumn(2);
    
    bb.printColumn(3);
    bb.printColumn(4);
    bb.printColumn(5);
    
    bb.printColumn(6);
    bb.printColumn(7);
    bb.printColumn(8);
  }

  public static void testBoardReadFile() {
    bool useTestMode = false;
    Board bb = new Board(useTestMode);

    bb.readFile("Boards/board1_start.txt");
    bb.printBoard();

    Console.WriteLine();
    bb.readFile("Boards/evil1_start.txt");
    bb.printBoard();
  }

  public static bool IsBoardSolved(Board theBoard) {
    bool retValue = false;

    // TODO - write this function

    return retValue;
  }
  
  public static void Main (string[] args) {
    //testBoard();
    //testBoardReadFile();

    Board theBoard = new Board(/*testMode=*/false);
    theBoard.readFile("Boards/board1_solved.txt");

    theBoard.printBoard();

    bool isSolved = IsBoardSolved(theBoard);

    if (isSolved == true) {
      Console.WriteLine("The Board is Solved!");
    } else {
      Console.WriteLine("The Board is NOT solved!");
    }
  }
}
