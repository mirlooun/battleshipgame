﻿using System;
using System.Threading;
using Battleship.Domain;
using Helpers;

namespace Battleship.ConsoleUi
{
    public class BattleshipUi : BaseUi
    {
        public static void DrawBoard(ECellState[,] board)
        {
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

        public static void DrawBoards(ECellState[,] currentEnemyBoard, ECellState[,] currentPlayer)
        {
            Console.Clear();
            
            Console.ForegroundColor = ConsoleColor.Blue;
            var width = currentEnemyBoard.GetUpperBound(0) + 1; //x-axis
            var height = currentEnemyBoard.GetUpperBound(1) + 1; //y-axis
            for (var colIndex = 0; colIndex < width; colIndex++)
            {
                if (colIndex == 0) Console.Write("    ");
                if (colIndex + 1 < 10) Console.Write($"  {(char) (65 + colIndex) + 1}  ");
                else if (colIndex + 1 < 100 && colIndex + 1 > 9) Console.Write($" {(char) (65 + colIndex) + 1}  ");
                else Console.Write($"  {(char) (65 + colIndex) + 1}  ");
            }

            Console.WriteLine();

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
                Console.Write($"{rowIndex + 1} ");

                for (var colIndex = 0; colIndex < width; colIndex++)
                {
                    Console.Write("| ");
                    var cellState = CellString(currentEnemyBoard[colIndex, rowIndex], false);
                    if (cellState.Equals("*")) Console.ForegroundColor = ConsoleColor.Gray;
                    else if (cellState.Equals("S")) Console.ForegroundColor = ConsoleColor.Green;
                    else if (cellState.Equals("X")) Console.ForegroundColor = ConsoleColor.Red;
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
            Console.WriteLine($"Player {playerName} move");
            Console.WriteLine();
        }

        public static void ShowEnemyBoardMessage(string playerName)
        {
            Console.WriteLine($"Player {playerName} board");
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
    }
}
