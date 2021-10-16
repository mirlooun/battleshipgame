using System;
using System.Threading;
using Battleship.Domain;

namespace Battleship.ConsoleUi
{
    public static class BattleshipUi
    {
        public static void DrawBoard(ECellState[,] board)
        {
            Console.Title = "Battleship";
            
            var width = board.GetUpperBound(0) + 1; //x-axis
            var height = board.GetUpperBound(1) + 1; //y-axis
            
            Console.ForegroundColor = ConsoleColor.White;
            for (var colIndex = 0; colIndex < width; colIndex++)
            {
                var c = (char)(65 + colIndex);
                if (colIndex == 0) Console.Write("    ");
                if (colIndex + 1 < 10) Console.Write($"  {c}  ");
                else if (colIndex + 1 < 100 && colIndex + 1 > 9) Console.Write($"  {c}  ");
                else Console.Write($"  {c}  ");
            }

            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Blue;
            for (var colIndex = 0; colIndex < width; colIndex++)
            {
                if (colIndex == 0) Console.Write("    ");
                Console.Write("+---+");
            }

            Console.WriteLine();

            for (var rowIndex = 0; rowIndex < height; rowIndex++)
            {
                if (rowIndex + 1 < 10) Console.Write("  ");
                else if (rowIndex + 1 > 9 && rowIndex + 1 < 100) Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.White;   
                Console.Write($"{rowIndex + 1} ");
                Console.ForegroundColor = ConsoleColor.Blue;  
                for (var colIndex = 0; colIndex < width; colIndex++)
                {
                    Console.Write("| ");
                    var cellState = CellString(board[colIndex, rowIndex], false);
                    if (cellState.Equals("*")) Console.ForegroundColor = ConsoleColor.Gray;
                    else if (cellState.Equals("X")) Console.ForegroundColor = ConsoleColor.White;
                    else if (cellState.Equals("@")) Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(cellState);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(" |");
                }
                Console.WriteLine();
                for (var colIndex = 0; colIndex < width; colIndex++)
                {
                    if (colIndex == 0) Console.Write("    ");
                    Console.Write("+---+");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.ResetColor();
        }

        public static void DrawSingleBoard(ECellState[,] presentationalBoard)
        {
            var width = presentationalBoard.GetUpperBound(0) + 1; //x-axis
            var height = presentationalBoard.GetUpperBound(1) + 1; //y-axis
            
            Console.ForegroundColor = ConsoleColor.White;
            for (var colIndex = 0; colIndex < width; colIndex++)
            {
                var c = (char)(65 + colIndex);
                if (colIndex == 0) Console.Write("    ");
                if (colIndex + 1 < 10) Console.Write($"  {c}  ");
                else if (colIndex + 1 < 100 && colIndex + 1 > 9) Console.Write($"  {c}  ");
                else Console.Write($"  {c}  ");
            }

            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.Blue;
            for (var colIndex = 0; colIndex < width; colIndex++)
            {
                if (colIndex == 0) Console.Write("    ");
                Console.Write("+---+");
            }

            Console.WriteLine();
            
            for (var rowIndex = 0; rowIndex < height; rowIndex++)
            {
                if (rowIndex + 1 < 10) Console.Write("  ");
                else if (rowIndex + 1 > 9 && rowIndex + 1 < 100) Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.White;   
                Console.Write($"{rowIndex + 1} ");
                Console.ForegroundColor = ConsoleColor.Blue;  
                for (var colIndex = 0; colIndex < width; colIndex++)
                {
                    Console.Write("| ");

                    var cellState = CellString(presentationalBoard[colIndex, rowIndex], true);
                    if (cellState.Equals("*")) Console.ForegroundColor = ConsoleColor.Gray;
                    else if (cellState.Equals("S")) Console.ForegroundColor = ConsoleColor.Green;
                    else if (cellState.Equals("X") || cellState.Equals("@")) Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(cellState);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(" |");
                }
                Console.WriteLine();
                for (var colIndex = 0; colIndex < width; colIndex++)
                {
                    if (colIndex == 0) Console.Write("    ");
                    Console.Write("+---+");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.ResetColor();
        }
        
        public static void DrawSingleBoard(ECellState[,] presentationalBoard, Boat boat)
        {
            
            var width = presentationalBoard.GetUpperBound(0) + 1; //x-axis
            var height = presentationalBoard.GetUpperBound(1) + 1; //y-axis
            
            Console.ForegroundColor = ConsoleColor.White;
            for (var colIndex = 0; colIndex < width; colIndex++)
            {
                var c = (char)(65 + colIndex);
                if (colIndex == 0) Console.Write("    ");
                if (colIndex + 1 < 10) Console.Write($"  {c}  ");
                else if (colIndex + 1 < 100 && colIndex + 1 > 9) Console.Write($" {c}  ");
                else Console.Write($"{c}  ");
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            for (var colIndex = 0; colIndex < width; colIndex++)
            {
                if (colIndex == 0) Console.Write("    ");
                Console.Write("+---+");
            }

            Console.WriteLine();

            for (var rowIndex = 0; rowIndex < height; rowIndex++)
            {
                if (rowIndex + 1 < 10) Console.Write("  ");
                else if (rowIndex + 1 > 9 && rowIndex + 1 < 100) Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{rowIndex + 1} ");
                Console.ForegroundColor = ConsoleColor.Blue;

                for (var colIndex = 0; colIndex < width; colIndex++)
                {
                    Console.Write("| ");
                    if (boat.IsLocatedHere(colIndex, rowIndex))
                    {
                        var cellState = "S";
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(cellState);
                        Console.ForegroundColor = ConsoleColor.Blue; 
                    }
                    else
                    {
                        var cellState = CellString(presentationalBoard[colIndex, rowIndex], true);
                        if (cellState.Equals("*")) Console.ForegroundColor = ConsoleColor.Gray;
                        else if (cellState.Equals("S")) Console.ForegroundColor = ConsoleColor.Green;
                        else if (cellState.Equals("X")) Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(cellState);
                        Console.ForegroundColor = ConsoleColor.Blue; 
                    }
                    
                    Console.Write(" |");
                }
                Console.WriteLine();
                for (var colIndex = 0; colIndex < width; colIndex++)
                {
                    if (colIndex == 0) Console.Write("    ");
                    Console.Write("+---+");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.ResetColor();
        }
        private static string CellString(ECellState cellState, bool displayShips)
        {
            switch (cellState)
            {
                case ECellState.Empty: return " ";
                case ECellState.Ship: return displayShips ? "S" : " ";
                case ECellState.Miss: return "X";
                case ECellState.Hit: return "@";
                default: return "-";
            }
        }

        public static void ShowCurrentPlayerName(string playerName)
        {
            Console.Write(" Player ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{playerName}");
            Console.ResetColor();
            Console.Write(" move");
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void ShowEnemyBoardMessage(string playerName)
        {
            Console.Write(" Player ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{playerName}");
            Console.ResetColor();
            Console.Write(" board");
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void ShowHitResponseMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Wait();
        }

        public static void ShowNextMoveByMessage(string playerName)
        {
            Console.Clear();
            Console.WriteLine($"Next move by {playerName}");
            Wait();
        }
        
        private static void Wait()
        {
            Thread.Sleep(2000);
        }
    }
}
