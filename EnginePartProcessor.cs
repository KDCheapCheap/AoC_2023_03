using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023_03
{
    public class EnginePartProcessor
    {
        public int CalculateEngineParts(char[,] processedInput, int rows, int columns)
        {
            int answer = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    char workingChar = processedInput[i, j];

                    if (!char.IsDigit(workingChar))
                    {
                        continue;
                    }
                    bool isEnginePart = false;

                    isEnginePart = ProcessNumberInGrid([i, j], rows, columns, processedInput, out int partNumber, out int newLoopPos);
                    
                    //Move the loop to the end of the processed number
                    j = newLoopPos;

                    if (isEnginePart)
                    {
                        Console.WriteLine($"Found Engine Part: {partNumber}");
                        answer += partNumber;
                    }
                }
            }

            return answer;
        }

        public bool ProcessNumberInGrid(int[] position, int rows, int columns, char[,] processedInput, out int partNumber, out int newLoopPos)
        {
            bool returnValue = false;
            newLoopPos = position[1];

            StringBuilder numberBuilder = new StringBuilder();

            for (int k = position[1]; k < columns; k++)
            {
                if (!char.IsDigit(processedInput[position[0], k]))
                {
                    break;
                }

                numberBuilder.Append(processedInput[position[0], k]);

                // Change j to progress from the last number.
                if (k + 1 < columns)
                {
                    newLoopPos = k + 1;
                }
                else
                {
                    newLoopPos = columns;
                }

                // Check if engine part.
                if (CheckAdjacentCharForSymbol([position[0], k], rows, columns, processedInput))
                {
                    returnValue = true;
                }
            }

            partNumber = int.Parse(numberBuilder.ToString());
            return returnValue;
        }

        public char[,] ProcessInput(string filename)
        {
            string filePath = $"Data\\{filename}.txt";

            string[] lines = File.ReadAllLines(filePath);

            ValidateInput(lines);

            char[,] output = new char[lines.Length, lines[0].Length];

            // Loop through the lines and assign the values of the output array.
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    output[i, j] = lines[i][j];
                }
            }

            PrintGrid(output);

            return output;
        }

        public bool CheckAdjacentCharForSymbol(int[] position, int rows, int columns, char[,] processedInput)
        {
            // Define the directions for adjacency (top, bottom, left, right)
            int[,] directions = {
            { -1, 0 }, // Up
            { 1, 0 },  // Down
            { 0, -1 }, // Left
            { 0, 1 }, // Right
            { -1, -1 }, // Top-left diagonal
            { -1,  1 }, // Top-right diagonal
            {  1, -1 }, // Bottom-left diagonal
            {  1,  1 }  // Bottom-right diagonal
        };

            int directionsRows = directions.GetLength(0);

            // Check each adjacent position
            for (int i = 0; i < directionsRows; i++)
            {
                int newRow = position[0] + directions[i, 0];
                int newCol = position[1] + directions[i, 1];

                // Ensure the position is in bounds
                if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < columns)
                {
                    char adjacentChar = processedInput[newRow, newCol];
                    if (!(adjacentChar == '.' || char.IsDigit(adjacentChar)))
                    {
                        return true; // Found an invalid adjacent character
                    }
                }
            }

            return false;
        }

        public void PrintGrid(char[,] output)
        {
            Console.WriteLine("Number Grid:");

            Console.WriteLine();

            for (int i = 0; i < output.GetLength(0); i++)
            {
                for (int j = 0; j < output.GetLength(1); j++)
                {
                    Console.Write(output[i, j]);
                }

                Console.WriteLine();
            }
        }

        public void ValidateInput(string[] lines)
        {
            int targetLength = lines[0].Length;

            foreach (string line in lines)
            {
                if (line.Length != targetLength)
                {
                    throw new InvalidDataException("Input file does not have equal line length.");
                }
            }
        }
    }
}
